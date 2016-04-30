using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rssreader.Models.Entities;

namespace rssreader.Models.Repository
{
    public class RepositoryFeed : Repository<Feed>
    {
        public RepositoryFeed(RssContext context) : base(context)
        {
        }

        public Feed GetByUrl(string url)
        {
            return DbSet.FirstOrDefault(f => f.Url.Equals(url));
        }
    }
}