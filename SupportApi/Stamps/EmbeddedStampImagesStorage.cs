using SupportApi.Localization;
using SupportApi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SupportApi.Resources.Stamps
{

    /// <summary>
    /// Internal stamp images storage.
    /// </summary>
    public class EmbeddedStampImagesStorage: IStampImagesStorage
    {

        private readonly string ext = ".png";

        private List<StampsCategory> _stampsCategories;

        #region ** IStampImagesStorage implementation

        public List<StampsCategory> StampCategories {
            get
            {
                if (_stampsCategories == null)
                {
                    _stampsCategories = new List<StampsCategory>();
                    _stampsCategories.Add(new StampsCategory()
                    {
                        Id = "Standard",
                        IsDynamic = false,
                        Name = Localizer.Get("EmbeddedStampImagesStorage.StandardBusiness", "Standard Business"),
                        StampImages = new string[] { "approved.png", "completed.png",
                        "confidential.png", "draft.png", "final.png", "forComment.png",  "forPublicRelease.png", "informationsOnly.png", "notApproved.png",
                        "notForPublicRelease.png", "preliminaryResults.png", "void.png"},
                        dpi = 144
                    });
                    _stampsCategories.Add(new StampsCategory()
                    {
                        Id = "Sign",
                        IsDynamic = false,
                        Name = Localizer.Get("EmbeddedStampImagesStorage.Sign", "Sign"),
                        StampImages = new string[] { "checkMark.png", "crossMark.png", "initialHere.png", "signHere.png", "witness.png" },
                        dpi = 144
                    });

                    /*
                    // Dynamic stamps sample:
                    // To use dynamic stamps, setup the GcImaging license key:
                    // GrapeCity.Documents.Imaging.GcBitmap.SetLicenseKey(key)
                    _stampsCategories.Add(new StampsCategory()
                    {
                        Id = "Dynamics",
                        IsDynamic = true,
                        Name = Localizer.Get("EmbeddedStampImagesStorage.Dynamics", "Dynamics"),
                        StampImages = new string[] { "received.png", "reviewed.png", "revised.png" },
                        dpi = 144
                    });
                    */
                }
                return _stampsCategories;
            }
        }

        public Stream GetStampStream(string categoryId, string imageName)
        {
            if (!imageName.EndsWith(ext))
                imageName += ext;
            var category = StampCategories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (category != null)
            {
                if (category.StampImages.Where(image => image == imageName).Count() > 0)
                {
                    Stream stream = null;
                    string imageKey = imageName.Replace(ext, "");
                    byte[] localizedImage = Localizer.GetImageBytes(imageName.Replace(ext, ""));
#if DEBUG
                    if(localizedImage == null)
                    {
                        Console.WriteLine("Missing image resource for key: " + imageKey);
                    }
#endif
                    if(localizedImage != null)
                    {
                        stream = new MemoryStream(localizedImage);
                        if (stream != null)
                        {
                            if (categoryId == "Dynamics")
                                stream = GetDynamicStampStream(stream, imageName);
                            stream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                    return stream;
                }
            }

            return null;
        }

        private Stream GetDynamicStampStream(Stream stream, string imageName)
        {
            try
            {
                using (var image = GrapeCity.Documents.Drawing.Image.FromStream(stream, false))
                {
                    var bitmap = image.ToGcBitmap();
                    using (var g = bitmap.CreateGraphics())
                    {
                        var tf = new GrapeCity.Documents.Text.TextFormat();
                        tf.FontSize = image.Height / 5;
                        tf.FontBold = true;
                        tf.ForeColor = Color.FromArgb(255, 25, 36, 105);
                        var marginBottom = 5;
                        var marginLeft = 20;
                        var p = new PointF(marginLeft, image.Height - image.Height / 3 - marginBottom);
                        g.DrawString(DateTime.Now.ToString(CultureInfo.InvariantCulture), tf, p);
                        var ms = new MemoryStream();
                        bitmap.SaveAsPng(ms);
                        stream.Dispose();
                        return ms;
                    }
                }
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#else
                return stream;
#endif

            }
        }

        #endregion


    }
}
