using GrapeCity.Documents.Pdf;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;

using _Float = System.Single;
using _Size = System.Drawing.SizeF;

using SupportApi.Models;
using GrapeCity.Documents.Pdf.Annotations;
using GrapeCity.Documents.Pdf.AcroForms;
using SupportApi.Collaboration;
using SupportApi.Controllers;
using System.Diagnostics;
using SupportApi.Connection;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using GrapeCity.Documents.Pdf.Security;

namespace SupportApi.Utils
{
    /// <summary>
    /// PDF document loader.
    /// </summary>
    public class GcPdfDocumentLoader : IDisposable
    {
        #region ** fields

        private static object lockDocumentLoad = new object();
        private _Float _resolution;
        
        private GcPdfDocument _doc;
        private SignatureInfo _signatureInfo;
        private SupportApiSettings _settings;
        private DocumentOptions _docOptions;
        private readonly SharedAccessMode _accessMode;
        private string _documentId;

        #endregion

        #region ** constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GcPdfDocumentLoader(SupportApiSettings settings, DocumentOptions docOptions, SharedAccessMode accessMode, _Float resolution = 96)
        {
            _settings = settings;
            _docOptions = docOptions;
            _resolution = resolution;
            Url = "";
            ClientId = docOptions.clientId;
            _accessMode = accessMode;
        }

        #endregion

        #region ** properties

        /// <summary>
        /// Gets currently opened document.
        /// </summary>
        public GcPdfDocument Document
        {
            get
            {
                return _doc;
            }
        }

        public Stream Stream { get; private set; }

        public string FileName
        {
            get
            {
                string fileName = "";
                if (Info != null)
                    fileName = Info.fileName;
                return fileName;
            }
        }


        public string ClientId { get; private set; }

        public OpenDocumentInfo Info { get; private set; }

        public string Url { get; }

        public int Rotate { get; set; } = 0;

        public _Float Scale { get; set; } = 1.0f;

        public bool RenderInteractiveForms { get; internal set; }

        /// <summary>
        /// Document modifications.
        /// </summary>
        public DocumentModifications DocumentModifications { get; private set; }

        public string DocumentId
        { 
            get {
                return _documentId;
            }
            set {
                Debug.Assert(string.IsNullOrEmpty(value) || string.IsNullOrEmpty(_documentId) || _documentId.Equals(value), "Error. DocumentId cannot be changed.");
                if (!string.IsNullOrEmpty(value))
                {
                    var clientConnection = ClientConnection.GetByClientId(value);
                    if (clientConnection != null)
                    {
                        clientConnection.DocumentId = value;
                    }
                }
                _documentId = value;
            } 
        }

        #endregion

        #region ** methods

        public void ApplyDocumentModifications(DocumentModifications modifications)
        {
            DocumentModifications = modifications;
            _applyModifications();
        }

        public OpenDocumentInfo Open(Stream data, string knownDocumentId = null)
        {
            lock (lockDocumentLoad)
            {
                if (Stream != null)
                {
                    Stream.Dispose();
                    Stream = null;
                    _doc = null;
                }
                Stream = data;
                return _OpenInitialDocState(knownDocumentId);
            }
        }

        public void Reset()
        {
            DocumentModifications = null;
            _signatureInfo = null;
        }

        public void Sign(SignatureInfo signatureInfo)
        {
            _signatureInfo = signatureInfo;
        }

        public void Save(MemoryStream s)
        {
            _applyModifications();
            if (_signatureInfo != null)
            {
                _saveWithSignature(s);
            }
            else
            {
                _doc.Save(s);
            }
            s.Seek(0, SeekOrigin.Begin);
        }

        public void SaveWithoutModifications(MemoryStream s)
        {
            _OpenInitialDocState(DocumentId);// Reset previous changes.
            _doc.Save(s);
            s.Seek(0, SeekOrigin.Begin);
        }

        public bool VerifySignature(string fieldName)
        {
            var signatureField = _doc.AcroForm.Fields.Where(f => f.Name == fieldName).FirstOrDefault() as SignatureField;
            if (signatureField == null || signatureField.Value == null)
            {
                return false;
            }
            return signatureField.Value.VerifySignatureValue();
        }

        #endregion

        #region ** IDisposable

        protected virtual void Dispose(bool disposing)
        {
            _doc = null;
            if (Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ** private and internal implementation

        public ConcurrentDictionary<string, byte[]> AttachedFiles { get; set; } = new ConcurrentDictionary<string, byte[]>();

        private void _applyModifications()
        {            
            _OpenInitialDocState(DocumentId);// Reset previous changes.
            List<DataParser> allClientAnnotations = null;
            if (DocumentModifications != null)
            {
                if (DocumentModifications.rotation.HasValue)
                {
                    Rotate = DocumentModifications.rotation.Value;
                }
                if (DocumentModifications.renderInteractiveForms.HasValue)
                {
                    RenderInteractiveForms = DocumentModifications.renderInteractiveForms.Value;
                }
                // Apply document structure changes:                
                var structureChanges = DocumentModifications.structureChanges;
                if (structureChanges != null)
                {
                    bool add = false;
                    int pageIndex = -1;
                    int operationIndex = 0;
                    int checkNumPages = -1;

                    foreach (var change in structureChanges)
                    {
                        add = change.add;
                        pageIndex = change.pageIndex;
                        checkNumPages = change.checkNumPages;
                        pageIndex = Math.Min(pageIndex, checkNumPages); // DOC-2299
                        if (checkNumPages > 5 && !_doc.IsLicensed)
                        {
                            throw new NotLicensedException(GcPdfViewerController.Settings.ErrorMessages.GcPdfNotLicensedLimit);
                        }
                        try
                        {
                            if (checkNumPages != _doc.Pages.Count)
                            {
                                // The most likely reason for this is using an unlicensed GcPdfDocument, which limits the number of pages.
                                // To ignore this error and return whatever pages have been added, comment out the next line:
                                throw new Exception(GcPdfViewerController.Settings.ErrorMessages.NumPagesCheckMismatch);
                            }
                            Page page = null;
                            if (!change.modOnly.HasValue || !change.modOnly.Value)
                            {
                                if (change.add)
                                {
                                    if (pageIndex >= _doc.Pages.Count)
                                        page = _doc.Pages.Add();
                                    else
                                        page = _doc.Pages.Insert(pageIndex);
                                }
                                else
                                {
                                    _doc.Pages.RemoveAt(pageIndex);
                                }
                            }
                            else
                            {
                                page = _doc.Pages[pageIndex];
                            }
                            var pageModification = change.mod;
                            if (pageModification != null)
                            {
                                if(page != null)
                                {
                                    if(!string.IsNullOrEmpty(pageModification.tabs))
                                    {
                                        page.AnnotationsTabsOrder = DataParser.ParseAnnotationsTabsOrder(pageModification.tabs);
                                    }
                                    if (pageModification.width.HasValue)
                                    {
                                        page.Size = new _Size(pageModification.width.Value, page.Size.Height);
                                    }
                                    if (pageModification.height.HasValue)
                                    {
                                        page.Size = new _Size(page.Size.Width, pageModification.height.Value);
                                    }
                                    if (pageModification.rotate.HasValue)
                                    {
                                        page.Rotate = pageModification.rotate.Value;
                                    }
                                }
                            }
                            operationIndex++;
                        }
                        catch (Exception)
                        {
                            throw new Exception(GcPdfViewerController.Settings.ErrorMessages.InvalidDocumentStructureChanges);
                            // Check: {operationIndex} {pageIndex} {add} {checkNumPages} {_doc.Pages.Count}. {ex.Message}"
                        }
                    }

                }
                // Apply annotation modifications
                if (DocumentModifications.annotationsData != null)
                {
                    List<KeyValuePair<PopupAnnotation, string>> popupAnnotations = new List<KeyValuePair<PopupAnnotation, string>>();
                    List<KeyValuePair<MarkupAnnotation, string>> referencedAnnotations = new List<KeyValuePair<MarkupAnnotation, string>>();
                    var redactsToApply = new List<RedactAnnotation>();
                    var convertAnnotationsToContent = new List<AnnotationBase>();
                    // Update existing annotations:
                    DataParser.CollectAnnotationsData(out List<DataParser> annotationsToUpdate, out List<DataParser> annotationsToCreate,
                        out List<KeyValuePair<int, PdfObjID>> annotationToRemove, out allClientAnnotations, DocumentModifications, _doc, Info, AttachedFiles);
                    foreach (var dataParser in annotationsToUpdate)
                    {
                        PdfObjID objID = dataParser.pdfObjID;
                        AnnotationTypeCode annotationType = dataParser.annotationType;
                        try
                        {
                            if (dataParser.IsWidget)
                            {
                                Field field = _FindFieldByObjId(_doc.AcroForm.Fields, objID);
                                if (field == null)
                                {
                                    if (dataParser.IsRadioButton)
                                        field = _doc.AcroForm.Fields.Where(a => a.Widgets.Any(w => w.ParsedObjID == objID)).FirstOrDefault();
                                    if (field == null)
                                        continue;
                                }
                                if (!IsFieldNamesEqual(dataParser.FieldName, field))
                                {
                                    // DOC-2025
                                    var fieldWidget = field.Widgets.Where(fw => fw.ParsedObjID == objID).FirstOrDefault();
                                    if (fieldWidget != null)
                                    {                                        
                                        if(field.Widgets.Count <= 1)
                                        {
                                            _RemoveField(field);
                                        }
                                        else
                                        {
                                            _RemoveAnnotation(fieldWidget);
                                        }
                                        annotationsToCreate.Add(dataParser);
                                    }
                                    continue;
                                }
                                AcroFormFactory.UpdateField(annotationType, field, dataParser);
                                AddField(field, dataParser);
                                if (dataParser.ConvertToContent)
                                {
                                    convertAnnotationsToContent.AddRange(field.Widgets);
                                }
                            }
                            else
                            {
                                AnnotationBase annotation = dataParser.page.Annotations.Where(a => a.ParsedObjID == objID).FirstOrDefault();
                                if (annotation == null)
                                {
                                    //throw new Exception($"Annotation with id {objID} not found.");
                                    continue;
                                }
                                AnnotationFactory.UpdateAnnotation(annotationType, annotation, dataParser);
                                AddAnnotation(annotation, dataParser.page.Annotations, dataParser);
                                if (annotationType == AnnotationTypeCode.REDACT && dataParser.ParseBool("redactApplied"))
                                {
                                    redactsToApply.Add(annotation as RedactAnnotation);
                                }
                                else if (dataParser.ConvertToContent)
                                {
                                    convertAnnotationsToContent.Add(annotation);
                                }
                                if (annotationType == AnnotationTypeCode.POPUP)
                                {
                                    popupAnnotations.Add(new KeyValuePair<PopupAnnotation, string>((PopupAnnotation)annotation, dataParser.parentId));
                                }
                                if ((annotation is MarkupAnnotation) && !string.IsNullOrEmpty(dataParser.referenceAnnotationId))
                                {
                                    referencedAnnotations.Add(new KeyValuePair<MarkupAnnotation, string>((MarkupAnnotation)annotation, dataParser.referenceAnnotationId));
                                }
                            }


                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    // Remove annotations:
                    foreach (var removedAnnotation in annotationToRemove)
                    {
                        try
                        {
                            Page page = _doc.Pages[removedAnnotation.Key];
                            PdfObjID objID = removedAnnotation.Value;

                            var field = _doc.AcroForm.Fields.Where(a => a.ParsedObjID == objID).FirstOrDefault();
                            if (field != null)
                            {
                                _RemoveField(field);
                            }
                            else
                            {
                                var annotation = page.Annotations.Where(a => a.ParsedObjID == objID).FirstOrDefault();
                                if (annotation != null)
                                {
                                    _RemoveAnnotation(annotation);

                                }

                            }

                            

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    // Add new annotations:
                    foreach (var dataParser in annotationsToCreate)
                    {
                        try
                        {
                            var page = dataParser.page;
                            if (dataParser.IsWidget)
                            {
                                if (dataParser.IsRadioButton)
                                {
                                    string fieldName = dataParser.FieldName;
                                    var radioButtonFieldWithSameName = _doc.AcroForm.Fields.Where(a => a.Name == fieldName).FirstOrDefault();
                                    if (radioButtonFieldWithSameName != null)
                                    {
                                        var widget = new WidgetAnnotation(dataParser.page, dataParser.rect);                                        
                                        radioButtonFieldWithSameName.Widgets.Add(widget);
                                        AcroFormFactory.UpdateFieldWidget(radioButtonFieldWithSameName, widget, dataParser);
                                        if (dataParser.ConvertToContent)
                                            convertAnnotationsToContent.Add(widget);
                                        continue;
                                    }
                                }
                                Field field = AcroFormFactory.CreateField(dataParser, page);
                                if (field != null)
                                {                                    
                                    AddField(field, dataParser);
                                    if (dataParser.ConvertToContent)
                                        convertAnnotationsToContent.AddRange(field.Widgets);
                                }                                
                            }
                            else
                            {
                                AnnotationBase annotation = AnnotationFactory.CreateAnnotation(dataParser, page);
                                if (annotation != null)
                                {
                                    AddAnnotation(annotation, page.Annotations, dataParser);
                                    if (dataParser.annotationType == AnnotationTypeCode.REDACT && dataParser.ParseBool("redactApplied"))
                                    {
                                        redactsToApply.Add(annotation as RedactAnnotation);                                        
                                    }
                                    else if (dataParser.ConvertToContent)
                                    {
                                        convertAnnotationsToContent.Add(annotation);
                                    }
                                    if (dataParser.annotationType == AnnotationTypeCode.POPUP)
                                    {
                                        popupAnnotations.Add(new KeyValuePair<PopupAnnotation, string>((PopupAnnotation)annotation, dataParser.parentId));
                                    }
                                }
                                if ((annotation is MarkupAnnotation) && !string.IsNullOrEmpty(dataParser.referenceAnnotationId))
                                {
                                    referencedAnnotations.Add(new KeyValuePair<MarkupAnnotation, string>((MarkupAnnotation)annotation, dataParser.referenceAnnotationId));
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    // Apply all redacts at once  (DOC-2254):
                    if (redactsToApply.Count > 0)
                        _doc.Redact(redactsToApply, _settings.RedactOptions);
                    AnnotationFactory.ConvertAnnotationsToContent(convertAnnotationsToContent);
                    

                    // Associate popup annotations with their destinations:
                    foreach (var keyValue in popupAnnotations)
                    {
                        try
                        {
                            var popupAnnotation = keyValue.Key;
                            var parentId = keyValue.Value;
                            var parentObjId = DataParser.IdToPdfObjID(parentId);
                            MarkupAnnotation popupParent = popupAnnotation.Page.Annotations.Where(a => a.ParsedObjID == parentObjId).FirstOrDefault() as MarkupAnnotation;
                            if (popupParent == null)
                            {
                                var annotationParser = allClientAnnotations.Where(p => p.id == parentId).FirstOrDefault();
                                if (annotationParser != null)
                                    popupParent = annotationParser.Annotation as MarkupAnnotation;
                            }
                            if (popupParent != null)
                                popupParent.Popup = popupAnnotation;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    // Set text annotation references:
                    foreach (var keyValue in referencedAnnotations)
                    {
                        try
                        {
                            var referenceAnnotation = keyValue.Key;
                            var referenceAnnotationId = keyValue.Value;
                            var referenceAnnotationObjId = DataParser.IdToPdfObjID(referenceAnnotationId);
                            MarkupAnnotation ownerAnnotation = referenceAnnotation.Page.Annotations.Where(a => a.ParsedObjID == referenceAnnotationObjId).FirstOrDefault() as MarkupAnnotation;
                            if (ownerAnnotation == null)
                            {
                                var annotationParser = allClientAnnotations.Where(p => p.id == referenceAnnotationId).FirstOrDefault();
                                if (annotationParser != null)
                                    ownerAnnotation = annotationParser.Annotation as MarkupAnnotation;
                            }
                            if (ownerAnnotation != null)
                            {
                                referenceAnnotation.ReferenceAnnotation = ownerAnnotation;
                                var reviewOrReplyAnnotation = referenceAnnotation as TextAnnotation;
                                if(reviewOrReplyAnnotation != null)  
                                {
                                    if (reviewOrReplyAnnotation.StateModel == "Review")
                                    {
                                        // Review annotation
                                        reviewOrReplyAnnotation.Subject = ownerAnnotation.Subject;
                                        reviewOrReplyAnnotation.ReferenceType = AnnotationReferenceType.Reply;
                                        DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.Hidden);
                                        DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.Print);
                                        DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.NoZoom);
                                        DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.NoRotate);
                                        reviewOrReplyAnnotation.CreationDate = new PdfDateTime(DateTime.Now);
                                        reviewOrReplyAnnotation.Rect = ownerAnnotation.Rect;
                                        if (string.IsNullOrEmpty(reviewOrReplyAnnotation.Name))
                                        {
                                            reviewOrReplyAnnotation.Name = Guid.NewGuid().ToString();
                                        }
                                        if (reviewOrReplyAnnotation.StateModel == "Review")
                                        {
                                            reviewOrReplyAnnotation.Contents = $"{reviewOrReplyAnnotation.State} set by {reviewOrReplyAnnotation.UserName}";
                                            reviewOrReplyAnnotation.RichText = $"<?xml version=\"1.0\"?><body xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:xfa=\"http://www.xfa.org/schema/xfa-data/1.0/\" xfa:APIVersion=\"Acrobat:20.6.0\" xfa:spec=\"2.0.2\" ><p>{reviewOrReplyAnnotation.Contents}</p></body>";
                                        }
                                    }
                                    else if(reviewOrReplyAnnotation.ReferenceType == AnnotationReferenceType.Reply)
                                    {
                                        if (string.IsNullOrEmpty(reviewOrReplyAnnotation.StateModel))
                                        {
                                            // Reply annotation:
                                            reviewOrReplyAnnotation.CreationDate = new PdfDateTime(DateTime.Now);
                                            DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.Print);
                                            DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.NoZoom);
                                            DataParser.SetFlag(reviewOrReplyAnnotation, AnnotationFlags.NoRotate);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                }
                // Import form data
                if (DocumentModifications.formData != null && DocumentModifications.formData.Count > 0)
                {
                    try
                    {
                        List<KeyValuePair<string, IList<string>>> fieldValues = new List<KeyValuePair<string, IList<string>>>();
                        foreach (var pageIndex in DocumentModifications.formData.Keys)
                        {
                            if (DocumentModifications.formData.TryGetValue(pageIndex, out Dictionary<string, string[]> pageData))
                            {
                                foreach (var kv in pageData)
                                {
                                    fieldValues.Add(new KeyValuePair<string, IList<string>>(kv.Key, new List<string>(kv.Value)));
                                }
                            }
                        }
                        _doc.ImportFormDataFromCollection(fieldValues.ToArray());
                    }
                    catch (Exception)
                    {
                        //
                    }
                }
            }

            var annotationsOrderTable = DocumentModifications.annotationsOrderTable;
            int rotate = Rotate;
            var pages = _doc.Pages;
            List<int> pageRotations = new List<int>();
            for (var i = 0; i < pages.Count; i++)
            {
                var page = pages.Where(p => p.Index == i).First();
                var totalRotate = page.Rotate + rotate;
                if (totalRotate > 360)
                    totalRotate = totalRotate - 360;
                pageRotations.Add(totalRotate);
            }
            foreach (var page in pages)
            {
                page.Rotate = pageRotations[page.Index];
                if (annotationsOrderTable != null && annotationsOrderTable.ContainsKey(page.Index))
                {
                    _SortPageAnnotations(page, new List<string>(annotationsOrderTable[page.Index]), allClientAnnotations);
                }
            }

        }

        private bool IsFieldNamesEqual(string fieldName, Field field)
        {
            if (fieldName == field.Name)
                return true;
            string compoundFieldName = "";
            while (field != null)
            {
                if (string.IsNullOrEmpty(compoundFieldName))
                    compoundFieldName = field.Name;
                else
                    compoundFieldName = $"{field.Name}.{compoundFieldName}";
                field = field.Parent;
            }
            return fieldName == compoundFieldName;
        }

        private Field _FindFieldByObjId(FieldCollection fields, PdfObjID objID)
        {
            if (fields == null)
                return null;
            foreach (var field in fields)
            {
                if (field.ParsedObjID.Equals(objID))
                    return field;
                var child = _FindFieldByObjId(field.Children, objID);
                if(child  != null)
                    return child;
            }
            return null;
        }

        private Field _FindFieldByName(FieldCollection fields, string fieldName)
        {
            if (fields == null)
                return null;
            foreach (var field in fields)
            {
                if (!string.IsNullOrEmpty(field.Name) && field.Name.Equals(fieldName))
                    return field;
                var child = _FindFieldByName(field.Children, fieldName);
                if (child != null)
                    return child;
            }
            return null;
        }

        private string _FindClientId(AnnotationBase annotation, List<DataParser> allClientAnnotations)
        {
            if (annotation == null)
                return "";
            string clientId = annotation.ParsedObjID.IsEmpty() ? _FindAnnotationId(annotation, allClientAnnotations) : DataParser.PdfObjIDtoId(annotation.ParsedObjID);
            if (string.IsNullOrEmpty(clientId) && annotation is WidgetAnnotation)
            {
                var dataParser = allClientAnnotations.Where(a => a.IsWidget == true && a.Widget != null && a.Widget == annotation).FirstOrDefault();
                if (dataParser == null)
                {
                    var field = (annotation as WidgetAnnotation).Field;
                    dataParser = allClientAnnotations.Where(a => a.IsWidget == true && a.Field == field).FirstOrDefault();
                }
                if (dataParser != null)
                {
                    clientId = dataParser.id;
                }
            }
            return clientId;
        }

        private void _SortPageAnnotations(Page page, List<string> annotationsOrder, List<DataParser> allClientAnnotations)
        {
            AnnotationCollection annotations = page.Annotations;
            if (annotations == null || annotationsOrder == null || allClientAnnotations == null)
                return;
            try
            {
                annotations.Sort(new Comparison<AnnotationBase>(delegate (AnnotationBase x, AnnotationBase y)
                {
                    if (x == null && y == null)
                        return 0;
                    if (x == null)
                        return -1;
                    if (y == null)
                        return 1;
                    try
                    {
                        string xID = _FindClientId(x, allClientAnnotations);
                        string yID = _FindClientId(y, allClientAnnotations);
                        int xIndex = annotationsOrder.IndexOf(xID);
                        int yIndex = annotationsOrder.IndexOf(yID);
                        if (xIndex == -1 || yIndex == -1)
                        {
                            if (x is WidgetAnnotation widget1 && y is WidgetAnnotation widget2)
                            {
                                var xName = widget1.Field?.Name ?? "";
                                var yName = widget2.Field?.Name ?? "";
                                return xName.CompareTo(yName);
                            }
                            // annotation without id (-1) should go to the end of the list (fix DOC-3358)
                            return yIndex.CompareTo(xIndex);
                        }
                        int res = xIndex.CompareTo(yIndex);
                        return res;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                    
                }));
            }
            catch (Exception)
            {
#if DEBUG          
                Console.Error.WriteLine("[Debug] [SupportApi] Exception in _SortPageAnnotations");
#endif
            }
        }
       
        private string _FindAnnotationId(AnnotationBase annotation, IEnumerable<DataParser> allClientAnnotations)
        {
            if (allClientAnnotations == null) 
                return "";
            var fieldParser = allClientAnnotations.Where(p => p.Field != null && p.Field.Equals(annotation)).FirstOrDefault();
            return fieldParser != null ? fieldParser.id : "";
        }

        private void AddField(Field field, DataParser dataParser)
        {
            try
            {
                FieldCollection owner = field.Owner;
                string uniqueFieldName = field.Name;
                // Remove original field from collection. (DOC-2001)
                _RemoveField(field);
                dataParser.Field = field;
                // Then search for duplicate names. (DOC-2001)
                int duplicateCount = 0;                
                Field duplicateField = _FindFieldByName(_doc.AcroForm.Fields, uniqueFieldName);
                while (duplicateField != null)
                {
                    if(duplicateField.Widgets.Count == 0 || duplicateField.Widgets[0].Page == null)
                    {
                        // Fix for DOC-1991: replace field without page.
                        _RemoveField(duplicateField);
                        break;
                    }
                    uniqueFieldName = $"{field.Name}_{++duplicateCount}";
                    duplicateField = _FindFieldByName(_doc.AcroForm.Fields, uniqueFieldName);
                }
                field.Name = uniqueFieldName;

                if(owner == null)
                    owner = _doc.AcroForm.Fields;
                owner.Add(field);
                foreach (var widget in field.Widgets)
                    widget.Page = dataParser.page;
            } 
            catch (Exception)
            {
                throw;
            }
        }

        private void AddAnnotation(AnnotationBase annotation, AnnotationCollection collection, DataParser dataParser)
        {
            _RemoveAnnotation(annotation);
            dataParser.Annotation = annotation;
            collection.Add(annotation);
            annotation.Page = dataParser.page;
        }

        private void _RemoveAnnotation(AnnotationBase annotation)
        {
            foreach (var p in _doc.Pages)
            {
                if (p.Annotations.Contains(annotation))
                {
                    p.Annotations.Remove(annotation);
                    break;
                }
            }
        }

        private void _RemoveField(Field field)
        {
            foreach (var widget in field.Widgets.ToArray())
            {
                _RemoveAnnotation(widget);
            }
            var owner = field.Owner;
            if (owner == null)
                owner = _doc.AcroForm.Fields;
            owner.Remove(field);
        }

        private void _saveWithSignature(Stream stream)
        {
            if (_signatureInfo != null)
            {

                var props = new SignatureProperties();
                props.ContactInfo = _signatureInfo.ContactInfo;
                props.Location = _signatureInfo.Location;
                props.SignerName = _signatureInfo.SignerName;
                props.SigningDateTime = DateTime.Now;
                props.Reason = _signatureInfo.Reason;
                props.TimeStamp = _signatureInfo.TimeStamp;
                props.SignatureField = _signatureInfo.SignatureField;
                
                props.SignatureBuilder = new Pkcs7SignatureBuilder()
                {
                    CertificateChain = new X509Certificate2[] { _settings.Certificate },
                    HashAlgorithm = _signatureInfo.HashAlgorithm,
                    Format = _signatureInfo.SignatureFormat,
                    
                };
                _doc.Sign(props, stream);
            }
        }

        private OpenDocumentInfo _OpenInitialDocState(string knownDocumentId)
        {
            try
            {
                if (_doc != null)
                {
                    _doc.Clear();
                    _doc = null;
                }
                string password = null;
                if (_docOptions != null)
                    password = _docOptions.password;
                var d = new GcPdfDocument();
                Stream.Seek(0, SeekOrigin.Begin);
                d.Load(Stream, password);
                _doc = d;
                _Size defaultViewPortSize = d.Pages.Count > 0 ? d.Pages[0].GetRenderSize(this._resolution, this._resolution) : new _Size(0f, 0f);

                string fileName = _docOptions?.fileName ?? string.Empty;                
                if (_doc.Metadata != null)
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = $"{_doc.Metadata.Title ?? ""}";
                    }
                }
                string documentId = knownDocumentId;
                if (string.IsNullOrEmpty(documentId))
                {
                    // Auto-generate document id:
                    documentId = $"generated_id_{Guid.NewGuid()}".Replace("-", "");
                    //documentId = _doc.Metadata.Identifier; // do not use fingerprint
                }


                if (string.IsNullOrEmpty(fileName))
                    fileName = documentId;
                DocumentId = documentId;
                Info = new OpenDocumentInfo(documentId, fileName, _accessMode, d.DocumentInfo, d.Pages.Count, ClientId, defaultViewPortSize, _docOptions);
                return Info;
            } 
            catch(Exception ex)
            {
                GcPdfViewerController.SetLastError(ex.Message);
                throw;
            }
        }

        #endregion

    }
}
