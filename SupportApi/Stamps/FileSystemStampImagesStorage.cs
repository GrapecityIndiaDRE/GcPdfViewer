using SupportApi.Localization;
using SupportApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupportApi.Resources.Stamps
{

    /// <summary>
    /// FileSystem-based stamp images storage.
    /// </summary>
    public class FileSystemStampImagesStorage : IStampImagesStorage
    {

        #region ** fields
        private DirectoryInfo _StampsDirectory;
        private List<StampsCategory> _StampCategories;
        #endregion

        #region ** constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stampsPath">Image directory path.</param>
        /// <param name="searchPattern">Optional. Image files search pattern. The default is "*.png;*.jpg;*.gif".</param>
        /// <param name="categoryName">Optional. Stamps category name. The default is "Stamps".</param>
        public FileSystemStampImagesStorage(string stampsPath, string searchPatterns = "*.png;*.jpg;*.jpeg;*.gif", string categoryName = null)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                categoryName = Localizer.Get("FileSystemStampImagesStorage.Stamps", "Stamps");
            }
            
            _StampsDirectory = new DirectoryInfo(stampsPath);
            _StampCategories = new List<StampsCategory>();
            
            if (_StampsDirectory.Exists)
            {
                var searchPatternsArr = searchPatterns.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                List<string> allImageFiles = new List<string>();
                foreach(var searchPattern in searchPatternsArr)
                {
                    var imageFiles = _StampsDirectory.GetFiles(searchPattern).Select(f => f.Name).ToArray();
                    allImageFiles.AddRange(imageFiles);
                }
                if (allImageFiles.Count > 0)
                {
                    float? dpi = null;
                    try
                    {
                        dpi = GrapeCity.Documents.Drawing.Image.FromFile(Path.Combine(_StampsDirectory.FullName, allImageFiles[0])).VerticalResolution;
                    } 
                    catch (Exception)
                    {
#if DEBUG
                        throw;
#endif
                    }
                    var stampsCategory = new StampsCategory()
                    {
                        Id = _StampsDirectory.Name,
                        IsDynamic = false,
                        Name = categoryName,
                        StampImages = allImageFiles.ToArray(),
                        dpi = dpi
                    };
                    _StampCategories.Add(stampsCategory);
                }
            }
        }
#endregion

#region ** IStampImagesStorage interface implementation

        public List<StampsCategory> StampCategories
        { 
            get
            {
                return _StampCategories;
            }
        }

        public Stream GetStampStream(string categoryId, string imageName)
        {
            string filePath = Path.Combine(_StampsDirectory.FullName, imageName);
            if (!File.Exists(filePath))
                return null;
            return File.OpenRead(filePath);
        }

#endregion
    }
}
