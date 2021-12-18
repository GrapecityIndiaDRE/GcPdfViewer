namespace SupportApi.Models
{
    /// <summary>
    /// Client side options and properties which was passed from viewer.
    /// </summary>
    public class DocumentOptions
    {

        /// <summary>
        /// Required. Unique viewer identifier.
        /// </summary>
        public string clientId;

        /// <summary>
        /// Optional. The password used to open the document.
        /// </summary>
        public string password;

        /// <summary>
        ///  URL which was used to open document.
        /// </summary>
        public string fileUrl;

        /// <summary>
        /// File name which was used to open document.
        /// </summary>
        public string fileName;

        /// <summary>
        /// The friendly file name option specified on client (see GcPdfViewer's friendlyFileName option).
        /// </summary>
        public string friendlyFileName;

        /**
        * Arbitrary data associated with the viewer.
        * This data is sent to the server when the document is saved.
        **/
        public object userData;

        /// <summary>
        /// Current user name. When specified this value will be used as UserName to mark annotation's state 
        /// and as author name for sticky note's replies.
        /// </summary>
        public string userName;



    }
}
