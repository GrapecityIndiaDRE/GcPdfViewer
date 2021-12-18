using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApi.Models
{
    /// <summary>
    /// Image stamp category.
    /// </summary>
    public class StampsCategory
    {

        /// <summary>
        /// Category identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Category display name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Stamp images.
        /// </summary>
        public string[] StampImages { get; set; }

        /// <summary>
        /// Indicates whether the category contains dynamic stamps.
        /// Dynamic stamp images are updated for each new stamp instance.
        /// </summary>
        public bool IsDynamic { get; set; } = false;

        /// <summary>
        /// Images resolution.
        /// </summary>
        public float? dpi { get; set; }
    }

}
