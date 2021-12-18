using SupportApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApi.Connection
{

    /// <summary>
    /// Client message.
    /// </summary>
    public class ClientMessage : Message
    {
        public string clientId { get; set; }

        public ClientMessageOrRequestType type { get; set; }

    }

    public enum ClientMessageOrRequestType
    {
        // Messages:
        Start = 1,
        Stop = 2,
        ShareDocument = 10,
        UnshareDocument = 11,
        Modification = 20,
        Reconnect = 30,
        // Request messages:
        UserAccessList = 100,
        SharedDocumentsList = 101,
        AllUsersList = 102,
        OpenSharedDocument = 103,
        StartSharedMode = 104,
        StopSharedMode = 105,
    }

}
