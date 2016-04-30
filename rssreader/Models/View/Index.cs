using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using rssreader.Models.Entities;

namespace rssreader.Models.View
{
    public class Index
    {
        public IList<Feed> Feeds { get; set; }

        public IList<Item> Items { get; set; }

        public int? CurrentFeedId { get; set; }

        [DisplayName("Datum od")]
        [DataType(DataType.Date)]
        public DateTime? FilterFrom { get; set; }

        [DisplayName("Datum do")]
        [DataType(DataType.Date)]
        public DateTime? FilterTo { get; set; }

        public JavascriptHelper JavascriptHelper { get; set; }
    }
}