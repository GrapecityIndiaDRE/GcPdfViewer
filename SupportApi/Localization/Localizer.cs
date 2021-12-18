using System.Globalization;
using System.IO;

namespace SupportApi.Localization
{
    /// <summary>
    /// SupportApi localization.
    /// </summary>
    public sealed class Localizer
    {

        /// <summary>
        /// SupportApi culture.        
        /// </summary>
        /// <example>
        /// SupportApi.Localization.Localizer.Culture = CultureInfo.GetCultureInfo("ja-JP");
        /// </example>
        public static CultureInfo Culture = CultureInfo.CurrentCulture;

        /// <summary>
        /// Gets localized error messages.
        /// </summary>
        /// <returns></returns>
        public static ErrorMessages GetErrorMessages()
        {            
            if (Culture.Name == "ja-JP")
                return new ErrorMessages_Ja();
            else
                return new ErrorMessages();
        }

        /// <summary>
        /// Get localized string.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string Get(string key, string defaultValue)
        {
            var s = StringsTable.ResourceManager.GetString(key, Culture);
            if (string.IsNullOrEmpty(s))
                return defaultValue;
            return s;
        }

        /// <summary>
        /// Get localized image resource.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte[] GetImageBytes(string key)
        {
            if (key == "void")
            {
                var voidImg = ImagesTable.ResourceManager.GetObject("_void", Culture) as byte[];
                if (voidImg != null)
                    return voidImg;
            }
            return ImagesTable.ResourceManager.GetObject(key, Culture) as byte[];
        }
    }
}
