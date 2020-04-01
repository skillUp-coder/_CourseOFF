using System;

namespace TestingSys.WebUI.Models
{
    /// <summary>
    /// Pagination properties.
    /// </summary>
    public class InfoOfPage
    {
        /// <summary>
        /// Quantity of goods.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Quantity of goods on one page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Current Page Number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total Page Counts.
        /// </summary>
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }
}