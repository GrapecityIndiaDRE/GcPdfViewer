using System.Threading.Tasks;

namespace SupportApi.Collaboration.Storages
{
    /// <summary>
    /// Use the ICollaborationStorage interface to implement any custom shared documents storage type.
    /// </summary>
    public interface ICollaborationStorage
    {

        /// <summary>
        /// Method for reading data from storage. You must return null if the data for the given key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<byte[]> ReadData(string key);

        /// <summary>
        /// Method of writing data to storage. Please note, when the data is null, you must delete the old data for the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        Task WriteData(string key, byte[] data);
    }
}