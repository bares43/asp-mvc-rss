using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace rssreader.Models.Entities
{
    public class Feed :IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }

        public List<Item> Items { get; set; }

        public DateTime? LastUpdate { get; set; }

    }
}