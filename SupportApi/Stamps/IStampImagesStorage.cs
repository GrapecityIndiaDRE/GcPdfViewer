using SupportApi.Models;
using System.Collections.Generic;
using System.IO;

namespace SupportApi.Resources.Stamps
{

    /// <summary>
    /// Use the IStampImagesStorage interface to provide your own set of predefined image stamps.
    /// </summary>
    public interface IStampImagesStorage
    {
        /// <summary>
        /// Property with predefined stamp categories.
        /// </summary>
        List<StampsCategory> StampCategories { get; }

        /// <summary>
        /// Gets the readable stream of the stamp image.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        Stream GetStampStream(string categoryId, string imageName);

    }
}
