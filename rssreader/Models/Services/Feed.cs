using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rssreader.Models.Entities;
using rssreader.Models.View;

namespace rssreader.Models.Services
{
    public class Feed
    {

        XML xmlService = new XML();
        UnitOfWork unitOfWork = new UnitOfWork();

        public Entities.Feed CreateFeed(string url)
        {

            var feed = new Entities.Feed();

            var xml = xmlService.Load(url);

            feed = xmlService.ParseFeed(xml, feed);
            feed.Url = url;

            unitOfWork.RepositoryFeed.Add(feed);
            unitOfWork.Save();

            return feed;
        }

        public Entities.Feed UpdateFeed(Entities.Feed feed)
        {
            var xml = xmlService.Load(feed.Url);

            var items = xmlService.ParseItems(xml, feed);
            feed.LastUpdate = DateTime.Now;
            unitOfWork.RepositoryFeed.Update(feed);

            foreach (var item in items)
            {
                unitOfWork.RepositoryItem.Add(item);
            }

            unitOfWork.Save();

            return feed;
        }

        public void DeleteFeed(int id)
        {
            var items = unitOfWork.RepositoryItem.GetByFilter(id).Select(i => i.Id).ToList();

            unitOfWork.RepositoryItem.DeleteAll(items);

            unitOfWork.RepositoryFeed.Delete(id);
            unitOfWork.Save();
        }

        public Index CreateIndexView(int? idFeed = null, DateTime? from = null, DateTime? to = null)
        {
            return new Index
            {
                Feeds = unitOfWork.RepositoryFeed.GetAll(),
                Items =
                    idFeed.HasValue
                        ? unitOfWork.RepositoryItem.GetByFilter(idFeed, from, to)
                        : unitOfWork.RepositoryItem.GetByFilter(null, from, to),
                CurrentFeedId = idFeed,
                FilterFrom = from?.Date,
                FilterTo = to?.Date
            };

        }

    }
}