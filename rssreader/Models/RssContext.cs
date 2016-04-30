using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using rssreader.Models.Entities;

namespace rssreader.Models
{
    public class RssContext : DbContext
    {
        public DbSet<Feed> Feeds { get; set; } 
        public DbSet<Item> Items { get; set; }

        public RssContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
    }

}