using GrapeCity.Documents.Pdf;
using SupportApi.Collaboration;
using SupportApi.Localization;
using SupportApi.Models;
using SupportApi.Resources.Stamps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SupportApi.Models
{
    public class SupportApiSettings
    {
        /// <summary>
        /// Contains additional options using during redact action.
        /// </summary>
        public RedactOptions RedactOptions { get; set; }

        /// <summary>
        /// Gets or sets a certificate used to generate a digital signature
        /// </summary>
        public X509Certificate2 Certificate { get; set; }

        public CollaborationSettings Collaboration { get; private set; } = new CollaborationSettings();

        /// <summary>
        /// Gets or sets a list of all available user names. These names will be shown in the Share Document dialog box.
        /// </summary>
        public List<string> AvailableUsers { get; private set; } = new List<string>();

        public ErrorMessages ErrorMessages { get; private set; } = Localizer.GetErrorMessages();

        /// <summary>
        /// Stamp images storage.
        /// </summary>
        public IStampImagesStorage StampImagesStorage { get; set; }


        /// <summary>
        /// This event is fired when a client accesses any SupportApi method.
        /// Use this event in order to verify authentication token passed from client.
        /// </summary>
        public event VerifyTokenEventHandler VerifyToken
        {
            // Add the input delegate to the collection.
            add
            {
                verifyTokenEventDelegates.AddHandler(verifyTokenEventKey, value);
            }
            // Remove the input delegate from the collection.
            remove
            {
                verifyTokenEventDelegates.RemoveHandler(verifyTokenEventKey, value);
            }
        }

        internal void OnVerifyToken(VerifyTokenEventArgs e)
        {
            VerifyTokenEventHandler eventDelegate = (VerifyTokenEventHandler)verifyTokenEventDelegates[verifyTokenEventKey];
            if (eventDelegate != null)
            {
                eventDelegate(this, e);
            }
        }

        static readonly object verifyTokenEventKey = new object();
        protected EventHandlerList verifyTokenEventDelegates = new EventHandlerList();
    }

    public delegate void VerifyTokenEventHandler(Object sender, VerifyTokenEventArgs e);
}
