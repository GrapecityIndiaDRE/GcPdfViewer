using Newtonsoft.Json;
using ProtoBuf;
using SupportApi.Collaboration;
using SupportApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SupportApi.Models
{

    /// <summary>
    /// Information about document modifications.
    /// </summary>
    public class DocumentModifications
    {

        /// <summary>
        /// Document rotation.
        /// </summary>
        public int? rotation { get; set; }

        /// <summary>
        /// Not used.
        /// </summary>
        public bool? renderInteractiveForms { get; set; }

        /// <summary>
        /// Acro-form data. Key - page index, value, form fields data.
        /// </summary>
        public Dictionary<string, Dictionary<string, string[]>> formData { get; set; }

        /// <summary>
        /// Annotations data.
        /// </summary>
        public ModificationsState annotationsData { get; set; }

        /// <summary>
        /// Information about document structure changes.
        /// </summary>
        public List<StructureChange> structureChanges { get; set; }

        /// <summary>
        /// Annotations order.
        /// </summary>
        public Dictionary<int, string[]> annotationsOrderTable { get; set; }

    }

    /// <summary>
    /// Removed annotation info.
    /// </summary>
    [ProtoContract]
    public class RemovedAnnotationInfo
    {
        [ProtoMember(1)]
        public int pageIndex;

        [ProtoMember(2)]
        public string annotationId;

    }

    /// <summary>
    /// Document structure modifications.
    /// </summary>
    public class StructureChange
    {
        /// <summary>
        /// Changed page index.
        /// </summary>
        [ProtoMember(1)]
        public int pageIndex { get; set; }
        /// <summary>
        /// true - page added, false - page removed
        /// </summary>
        [ProtoMember(2)]
        public bool add { get; set; }

        /// <summary>
        /// Pages count before change.
        /// </summary>
        [ProtoMember(3)]
        public int checkNumPages { get; set; }

        /// <summary>
        /// Apply page modification only, ignore add flag.
        /// </summary>
        [ProtoMember(4)]
        public bool? modOnly { get; set; }

        /// <summary>
        /// Page modification.
        /// </summary>
        [ProtoMember(5)]
        public PageModification mod { get; set; }

    }

    /// <summary>
    /// Page modification.
    /// </summary>
    [ProtoContract]
    public class PageModification
    {
        /// <summary>
        /// New page width.
        /// </summary>
        [ProtoMember(1)]
        public int? width;

        /// <summary>
        /// New page height.
        /// </summary>
        [ProtoMember(2)]
        public int? height;

        /// <summary>
        /// Page rotation.
        /// </summary>
        [ProtoMember(3)]
        public int? rotate;

        /// <summary>
        /// Page fields tab order.
        /// </summary>
        [ProtoMember(4)]
        public string tabs;
    }


    [ProtoContract]
    public class PdfInfo
    {
        /// <summary>
        /// Total number of pages the PDF contains.
        /// </summary>
        [ProtoMember(1)]
        public int numPages { get; set; }
        /// <summary>
        /// A (not guaranteed to be) unique ID to identify a PDF.
        /// </summary>
        [ProtoMember(2)]
        public string fingerprint { get; set; }

    }

    [ProtoContract]
    public class StructureChanges 
    {
        [ProtoMember(1)]
        public int[] resultStructure { get; set; }
        [ProtoMember(2)]
        public StructureChange[] structureChanges { get; set; }
        [ProtoMember(3)]
        public PdfInfo pdfInfo { get; set; }
        [ProtoMember(4)]
        public RemovedAnnotationInfo[] touchedAnnotations { get; set; }

    }



}
