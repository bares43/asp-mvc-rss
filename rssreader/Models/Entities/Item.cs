using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rssreader.Models.Entities
{
    public class Item : IEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public DateTime Created { get; set; }

        public Feed Feed { get; set; }
    }
}