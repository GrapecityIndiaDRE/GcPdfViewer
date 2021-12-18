using GrapeCity.Documents.Pdf;
using SupportApi.Collaboration;

namespace SupportApi.Models
{

    /// <summary>
    /// Information about PDF document.
    /// An instance of this class is passed to the appropriate client viewer when the document is opened.
    /// </summary>
    public class OpenDocumentInfo
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="fileName"></param>
        /// <param name="accessMode"></param>
        /// <param name="documentInfo"></param>
        /// <param name="pagesCount"></param>
        /// <param name="clientId"></param>
        /// <param name="defaultViewPortSize"></param>
        /// <param name="docOptions"></param>
        public OpenDocumentInfo(string documentId, string fileName, SharedAccessMode accessMode, DocumentInfo documentInfo, int pagesCount, string clientId, System.Drawing.SizeF defaultViewPortSize, DocumentOptions docOptions)
        {
            this.documentId = documentId;
            this.fileName = fileName;
            this.accessMode = accessMode;
            this.clientId = clientId;
            info = new DocumentInfoWrapper(documentInfo);
            this.pagesCount = pagesCount;
            this.defaultViewPortSize = new { w = defaultViewPortSize.Width, h = defaultViewPortSize.Height };
            documentOptions = docOptions;
        }


        /// <summary>
        /// Unique client(viewer application) session identifier.
        /// </summary>
        public string clientId { get; set; }

        /// <summary>
        /// User access mode.
        /// </summary>
        public SharedAccessMode accessMode { get; set; }

        /// <summary>
        /// Unique document identifier. Used to share document between clients.
        /// </summary>
        public string documentId { get; set; }

        public string fileName { get; set; }

        /// <summary>
        /// The friendly file name specified on the client (see GcPdfViewer's friendlyFileName option).
        /// </summary>
        public string friendlyFileName { get; set; }

        /// <summary>
        /// Total pages count.
        /// </summary>
        public int pagesCount { get; set; }

        /// <summary>
        /// Default page's view port size.
        /// </summary>
        public dynamic defaultViewPortSize { get; set; }

        /// <summary>
        /// Document options passed from the client viewer.
        /// </summary>
        public DocumentOptions documentOptions { get; set; }

        /// <summary>
        /// Contains information about the document. This information includes 
        /// the document author, title, keywords, etc.
        /// </summary>
        public DocumentInfoWrapper info { get; set; }

    }

    public class DocumentInfoWrapper
    {

        public string keywords { get; set; }

        public string author { get; set; }

        public string creator { get; set; }

        public string subject { get; set; }

        public string title { get; set; }

        public string producer { get; set; }

        public string creationDate { get; set; }

        public string modifyDate { get; set; }

        public DocumentInfoWrapper(DocumentInfo documentInfo)
        {
            if (documentInfo != null)
            {
                keywords = documentInfo.Keywords;
                author = documentInfo.Author;
                creator = documentInfo.Creator;
                subject = documentInfo.Subject;
                title = documentInfo.Title;
                producer = documentInfo.Producer;
                // Note, PdfDateTime.DateTimeValue can throw exception for incorrect date, 
                // so we use string value instead:
                creationDate = documentInfo.CreationDate.HasValue ? documentInfo.CreationDate.Value.ToString() : "";
                modifyDate = documentInfo.ModifyDate.HasValue ? documentInfo.ModifyDate.Value.ToString() : "";
            }
        }
    }
}
