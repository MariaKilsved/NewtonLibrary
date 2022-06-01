using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class NewsAndEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? Updated { get; set; }
        public int? CategoryId { get; set; }
    }
}