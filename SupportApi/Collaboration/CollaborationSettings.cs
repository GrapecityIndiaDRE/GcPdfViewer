using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApi.Collaboration
{
    /// <summary>
    /// Collaboration mode options.
    /// </summary>
    public class CollaborationSettings
    {

        /// <summary>
        /// Shared storage options.
        /// </summary>
        public SharedStorageSettings Storage { get; private set; } = new SharedStorageSettings();

        /// <summary>
        /// Time interval to wait before disposing client connections.
        /// Default time interval is 30 minutes.
        /// </summary>
        public TimeSpan WaitForReconnectTime { get; internal set; } = new TimeSpan(0, 30, 0);
    }
}
