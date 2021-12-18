using System;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Pdf.AcroForms;
using GrapeCity.Documents.Pdf.Security;

namespace SupportApi.Models
{

    /// <summary>
    /// Information about document signature.
    /// </summary>
    public class SignatureInfo
    {
        /// <summary>
        /// Gets or sets the information provided by the signer to enable a recipient to contact the signer
        /// to verify the signature (for example, a phone number).
        /// </summary>
        public string ContactInfo { get; internal set; }

        /// <summary>
        /// Gets or sets the CPU host name or physical location of the signing.
        /// <para>
        /// By default this property is initalized with <see cref="Environment.MachineName"/>.
        /// </para>
        /// </summary>
        public string Location { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the person or authority signing the document.
        /// <para>
        /// NOTE: This value is used only if it is not possible to extract the name from the signature,
        /// for example from the certificate of the signer.
        /// </para>
        /// <para>
        /// By default this property is initialized with <see cref="Environment.UserName"/>.
        /// </para>
        /// </summary>
        public string SignerName { get; internal set; }

        /// <summary>
        /// Gets or sets the reason for the signing, such as "I agree...".
        /// </summary>
        public string Reason { get; internal set; }

        /// <summary>
        ///  Gets or sets the HASH algorithm used if GrapeCity.Documents.Pdf.Pkcs7SignatureBuilder.SignatureGenerator
        ///  is not specified. Note! If GrapeCity.Documents.Pdf.Pkcs7SignatureBuilder.Format
        ///  is GrapeCity.Documents.Pdf.Pkcs7SignatureBuilder.SignatureFormat.adbe_pkcs7_sha1
        ///  then this property is ignored and always SHA-1 used.
        /// </summary>
        public OID HashAlgorithm { get; internal set; } = OID.HashAlgorithms.SHA512;

        /// <summary>
        /// Gets or sets the signature format.
        /// </summary>
        public Pkcs7SignatureBuilder.SignatureFormat SignatureFormat { get; internal set; }

        /// <summary>
        /// Gets or sets the <see cref="Pdf.TimeStamp"/> object used to include
        /// a timestamp in the digital signature of a PDF.
        /// </summary>
        public TimeStamp TimeStamp { get; internal set; }

        /// <summary>
        /// Gets or sets an acroform field used to store a digital signature.
        /// </summary>
        public Field SignatureField { get; internal set; }
    }
}
