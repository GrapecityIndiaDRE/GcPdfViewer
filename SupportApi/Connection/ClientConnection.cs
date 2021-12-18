using SupportApi.Controllers;
using SupportApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SupportApi.Connection
{
    public class ClientConnection : IDisposable
    {
        
        #region ** fields

        private static List<ClientConnection> _allConnections { get; set; } = new List<ClientConnection>();
        private bool _disposed;

        #endregion

        #region ** constructor

        public ClientConnection(string connectionId, string clientId, string userName)
        {
            ConnectionId = connectionId;
            ClientId = clientId;
            UserName = userName;
        }

        #endregion

        #region ** properties

        public string ClientId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        
        /// <summary>
        /// The document identifier associated with the connection. Can be null or empty if no document is open..
        /// </summary>
        public string DocumentId { get; set; } = string.Empty;

        public GcPdfDocumentLoader DocumentLoader
        {
            get
            {
                if (GcPdfViewerController.DocumentLoaders.TryGetValue(ClientId, out GcPdfDocumentLoader loader))
                {
                    return loader;
                }
                else
                {
                    throw new ClientConnectionException(GcPdfViewerController.Settings.ErrorMessages.DocumentLoaderNotFoundFatal);
                }
            }
        }

        #endregion

        #region ** factory methods

        /// <summary>
        /// Create and register client connection.
        /// </summary>
        /// <param name="connectionId">SignalR connectionId.</param>
        /// <param name="clientId">Client viewer id.</param>
        /// <param name="userName">Owner user name.</param>
        /// <returns></returns>
        public static ClientConnection CreateConnection(string connectionId, string clientId, string userName)
        {
            if (string.IsNullOrEmpty(connectionId))
                throw new ClientConnectionException(GcPdfViewerController.Settings.ErrorMessages.CreateConnectionMissingConnectionIdInternal);
            if (string.IsNullOrEmpty(clientId))
                throw new ClientConnectionException(GcPdfViewerController.Settings.ErrorMessages.CreateConnectionMissingClientIdInternal);
            lock (_allConnections)
            {
                var connection = _allConnections.Where(i => i.ClientId == clientId).FirstOrDefault();
                if (connection != null)
                {
                    connection.ConnectionId = connectionId;
                    connection.UserName = userName;
                }
                else
                {
                    connection = new ClientConnection(connectionId, clientId, userName);
                    _allConnections.Add(connection);
                }
                return connection;
            }
        }

        /// <summary>
        /// Unregister client connection.
        /// </summary>
        /// <param name="connectionId">SignalR connectionId</param>
        public static void DisposeConnection(string connectionId)
        {
            lock (_allConnections)
            {
                var connection = _allConnections.Where(i => i.ConnectionId == connectionId).FirstOrDefault();
                if (connection != null)
                {
                    _allConnections.Remove(connection);
                    connection.Dispose();                    
                }
            }
        }

        internal static void DisposeConnection(ClientConnection clientConnection)
        {
            if (clientConnection != null)
            {
                DisposeConnection(clientConnection.ConnectionId);
            }
        }

        #endregion

        #region ** methods

        /// <summary>
        /// Gets registered client connection.
        /// </summary>
        /// <param name="connectionId">SignalR connectionId</param>
        /// <returns></returns>
        public static ClientConnection Get(string connectionId)
        {
            lock (_allConnections)
            {
                return _allConnections.Where(i => i.ConnectionId == connectionId).FirstOrDefault();
            }
        }

        public static List<ClientConnection> GetByUserName(string userName)
        {
            lock (_allConnections)
            {
                return _allConnections.Where(i => i.UserName == userName).ToList();
            }
        }

        public static ClientConnection GetByClientId(string clientId)
        {
            lock (_allConnections)
            {
                return _allConnections.Where(i => i.ClientId == clientId).FirstOrDefault();
            }
        }

        public static List<ClientConnection> GetByDocumentId(string documentId)
        {
            lock (_allConnections)
            {
                return _allConnections.Where(i => i.DocumentId == documentId).ToList();
            }
        }

        #endregion

        #region ** IDisposable interface implementation

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                // Dispose managed state (managed objects).                
            }
            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }



        #endregion
    }
}