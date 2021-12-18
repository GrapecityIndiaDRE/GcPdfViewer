using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace SupportApi.Collaboration.Models
{

    /// <summary>
    /// Information about the access mode for the user, provided by the <see cref="UserName"/> property.
    /// </summary>
    [Serializable]
    public class UserAccess : ISerializable, IComparable
    {
        
        #region ** constructor

        /// <summary>
        /// Default UserAccess class constructor.
        /// </summary>
        public UserAccess()
        {
        }

        /// <summary>
        /// Constructor which accepts a user name and access mode.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accessMode"></param>
        public UserAccess(string userName, SharedAccessMode accessMode)
        {
            UserName = userName;
            AccessMode = accessMode;
        }

        #endregion

        #region ** properties

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Shared document access type.
        /// </summary>
        public SharedAccessMode AccessMode { get; set; }

        #endregion

        #region ** ISerializable interface implementation

        protected UserAccess(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            if (serializationInfo == null)
                throw new ArgumentNullException("serializationInfo");
            UserName = serializationInfo.GetString("userName");
            int intAccessMode = serializationInfo.GetInt32("accessMode");
            AccessMode = (SharedAccessMode)intAccessMode;
        }


        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo == null)
                throw new ArgumentNullException("serializationInfo");

            serializationInfo.AddValue("userName", UserName);
            serializationInfo.AddValue("accessMode", (int)AccessMode);
        }

        #endregion

        #region ** IComparable interface implementation

        public int CompareTo(object obj)
        {
            var userAccess = obj as UserAccess;
            if (userAccess == null)
                return -1;
            var result = UserName.CompareTo(userAccess.UserName);
            if(result == 0)
            {
                return AccessMode.CompareTo(userAccess.AccessMode);
            }
            else
            {
                return result;
            }
        }

        #endregion
    }
 
}
