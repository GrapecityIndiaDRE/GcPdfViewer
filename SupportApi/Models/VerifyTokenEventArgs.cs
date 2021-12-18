using System;

#if WEB_FORMS
using System.Web.Http.Controllers;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

namespace SupportApi.Models
{

    /// <summary>
    /// This event is fired when a client accesses any Support Api method.
    /// </summary>
    public class VerifyTokenEventArgs : EventArgs
    {

#if WEB_FORMS
        public VerifyTokenEventArgs(HttpControllerContext controllerContext, string token, string actionName): base()
#else
        public VerifyTokenEventArgs(ControllerContext controllerContext, string token, string actionName) : base()
#endif        
        {
            ControllerContext = controllerContext;
            Token = token;
            ActionName = actionName;
            Reject = false;
        }

#if WEB_FORMS
        public HttpControllerContext ControllerContext { get; set; }
#else
        public ControllerContext ControllerContext { get; set; }
#endif


        /// <summary>
        /// Gets the token string passed from the client.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the name of the SupportApi method that the client is accessing.
        /// </summary>
        public string ActionName { get; }

        /// <summary>
        /// Set the Reject property to true if verification of the token string failed.
        /// </summary>
        public bool Reject { get; set; }

        

    }
}
