using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using rssreader.Exceptions;
using rssreader.Models.Entities;

namespace rssreader.Models.Services
{
    public class XML
    {
        public IEnumerable<XElement> Load(string url)
        {
            var xelement = XElement.Load(url);
            return xelement.Elements();
        }

        public Entities.Feed ParseFeed(IEnumerable<XElement> xml, Entities.Feed feed)
        {
            if (xml.ElementAt(0) == null || xml.ElementAt(0).Element("title") == null)
            {
                throw new BadFeed();
            }

            feed.Name = xml.ElementAt(0).Element("title").Value;
            return feed;
        }

        public List<Item> ParseItems(IEnumerable<XElement> xml, Entities.Feed feed)
        {
            var items = new List<Item>();

            var xmlItems = xml.First().Elements()
                .Where(i => i.Name.LocalName.Equals("item") && (!feed.LastUpdate.HasValue || i.Element("pubDate") == null || (i.Element("pubDate") != null && DateTime.Parse(i.Element("pubDate").Value) > feed.LastUpdate.Value)))
                .ToList();

            foreach (var xmlItem in xmlItems)
            {

                if (xmlItem.Element("link") == null ||
                    xmlItem.Element("title") == null)
                {
                    throw new BadFeed();
                }

                var item = new Item
                {
                    Feed = feed,
                    Url = xmlItem.Element("link").Value,
                    Title = xmlItem.Element("title").Value,
                    Created = DateTime.Now,
                };

                if (xmlItem.Element("pubDate") != null)
                {
                    DateTime created;
                    if (DateTime.TryParse(xmlItem.Element("pubDate").Value, out created))
                    {
                        item.Created = created;
                    }
                }

                items.Add(item);

            }

            return items;
        }
    }
}