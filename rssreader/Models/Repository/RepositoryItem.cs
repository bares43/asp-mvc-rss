using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using rssreader.Models.Entities;

namespace rssreader.Models.Repository
{
    public class RepositoryItem : Repository<Item>
    {
        public RepositoryItem(RssContext context) : base(context)
        {
        }

        public IList<Item> GetByFilter(int? idFeed = null, DateTime? from = null, DateTime? to = null)
        {
            var items = DbSet.Include(i => i.Feed);

            if (idFeed.HasValue)
            {
                items = items.Where(i => i.Feed.Id == idFeed.Value);
            }

            if (from.HasValue)
            {
                var fromLinq = from.Value.Date;
                items = items.Where(i => i.Created >= fromLinq);
            }

            if (to.HasValue)
            {
                // .Date nastavi cas na 00:00:00, tzn pokud chceme zahrnout i posledni den, je treba se o den posunout dopredu
                var toLinq = to.Value.AddDays(1).Date;
                items = items.Where(i => i.Created <= toLinq);
            }
            
            return items.OrderByDescending(i => i.Created).ToList();
        } 
        
    }
}