using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApi.Connection
{

    /// <summary>
    /// Server message.
    /// </summary>
    public class ServerMessage : Message
    {

        public ServerMessage() : base()
        {

        }

        public ServerMessage(string correlationId) : base(correlationId)
        {

        }

        /// <summary>
        /// Message type.
        /// </summary>
        public int /* ServerMessageType */ type;
        
        public static readonly string EMPTY_CORRELATION_ID = "no-id";
    }

    /// <summary>
    /// Server message type.
    /// </summary>
    public enum ServerMessageType
    {
        /// <summary>
        /// Display information message to user.
        /// </summary>
        Information = 10,

        /// <summary>
        /// Display error message to user.
        /// </summary>
        Error = 11,

        /// <summary>
        /// Push shared document modifications.
        /// </summary>
        Modifications = 20,

        /// <summary>
        /// The message is used to send a list of shared documents when modified.
        /// </summary>
        SharedDocumentsListChanged = 45,

        // Replies to request messages:

        /// <summary>
        /// UserAccessList response.
        /// </summary>
        UserAccessListResponse = 100,

        /// <summary>
        /// SharedDocumentsList response.
        /// </summary>
        SharedDocumentsListResponse = 101,

        /// <summary>
        /// AllUsersList response.
        /// </summary>
        AllUsersListResponse = 102,

        /// <summary>
        /// OpenSharedDocument response.
        /// </summary>
        OpenSharedDocumentResponse = 103,

        /// <summary>
        /// StartSharedMode response.
        /// </summary>
        StartSharedModeResponse = 104,

        /// <summary>
        /// StopSharedMode response.
        /// </summary>
        StopSharedModeResponse = 105
    }

}
