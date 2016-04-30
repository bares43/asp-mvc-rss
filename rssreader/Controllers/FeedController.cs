using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using rssreader.Exceptions;
using rssreader.Models;
using Feed = rssreader.Models.Entities.Feed;

namespace rssreader.Controllers
{
    public class FeedController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly Models.Services.Feed _feedService = new Models.Services.Feed();

        // GET: Feed
        public ActionResult Index(int? id = null, string filterFrom = "", string filterTo = "")
        {
            DateTime? fromDate = null, toDate = null;
            if (!string.IsNullOrEmpty(filterFrom))
            {
                DateTime from;
                if (DateTime.TryParse(filterFrom, out from))
                {
                    fromDate = from;
                }

            }

            if (!string.IsNullOrEmpty(filterTo))
            {
                DateTime to;
                if (DateTime.TryParse(filterTo, out to))
                {
                    toDate = to;
                }
            }

            if (toDate.HasValue && fromDate.HasValue && toDate.Value < fromDate.Value)
            {
                DateTime x;
                x = toDate.Value;
                toDate = fromDate;
                fromDate = x;
            }
            
            var indexView = _feedService.CreateIndexView(id, fromDate, toDate);
            ModelState.Clear();
            if (Request.IsAjaxRequest())
            {
                return PartialView(indexView);
            }
            return View(indexView);
        }
        

        public ActionResult AddForm()
        {
            return View(new Feed());
        }

        [HttpPost]
        public ActionResult Add(Feed formFeed)
        {
            var javascriptHelper = new JavascriptHelper();

            try
            {
                if (_unitOfWork.RepositoryFeed.GetByUrl(formFeed.Url) != null)
                {
                    javascriptHelper.AddWarning("Tento feed už existuje.");
                }
                else
                {
                    var feed = _feedService.CreateFeed(formFeed.Url);
                    _feedService.UpdateFeed(feed);

                    javascriptHelper.AddSuccess("Feed byl přidán.");
                }

            }
            catch (Exception e) when(e is BadFeed || e is FileNotFoundException || e is XmlException)
            {
                javascriptHelper.AddWarning(
                    "Tento feed se nepodařilo načíst. Zkontrolujte prosím, zda jste zadali správnou URL.");
            }
            catch (Exception)
            {
                javascriptHelper.AddWarning("Bohužel došlo k chybě :(");
            }

            var indexView = _feedService.CreateIndexView();
            indexView.JavascriptHelper = javascriptHelper;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", indexView);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(List<int> id)
        {
            var javascriptHelper = new JavascriptHelper();

            foreach (var item in id)
            {
                _feedService.DeleteFeed(item);
            }

            if (id.Count > 1)
            {
                javascriptHelper.AddSuccess($"{id.Count} feedů bylo smazáno.");
            }
            // uz vime ze jich neni vice nez 1 a any bude trochu rychlejsi nez count
            else if (id.Any())
            {
                javascriptHelper.AddSuccess("Feed byl smazán.");
            }

            var indexView = _feedService.CreateIndexView();
            indexView.JavascriptHelper = javascriptHelper;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", indexView);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteAll()
        {
            var javascriptHelper = new JavascriptHelper();

            try
            {
                var feeds = _unitOfWork.RepositoryFeed.GetAll();
                foreach (var feed in feeds)
                {
                    _feedService.DeleteFeed(feed.Id);
                }

                javascriptHelper.AddSuccess("Všechny feedy byly smazány");
            }
            catch (Exception)
            {
                javascriptHelper.AddWarning("Bohužel došlo k chybě :(");
            }

            var indexView = _feedService.CreateIndexView();
            indexView.JavascriptHelper = javascriptHelper;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", indexView);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult RefreshFeed(int? id = null)
        {
            var javascriptHelper = new JavascriptHelper();

            IList<Feed> feeds = new List<Feed>();

            if (id.HasValue)
            {
                var feed = _unitOfWork.RepositoryFeed.Get(id.Value);
                if (feed != null)
                {
                    feeds = new List<Feed> {feed};
                }
            }
            else
            {
                feeds = _unitOfWork.RepositoryFeed.GetAll();
            }

            try
            {
                foreach (var feed in feeds)
                {
                    _feedService.UpdateFeed(feed);
                }

                if (feeds.Count > 1)
                {
                    javascriptHelper.AddSuccess($"{feeds.Count} feedů bylo aktualizováno");
                }
                // uz vime ze jich neni vice nez 1 a any bude trochu rychlejsi nez count
                else if(feeds.Any())
                {
                    javascriptHelper.AddSuccess($"Feed {feeds.First().Name} byl aktualizován.");
                }

            }
            catch (Exception e) when (e is BadFeed || e is FileNotFoundException || e is XmlException)
            {
                javascriptHelper.AddWarning(
                    "Nedaří se obnovit feedy, vypadá to, že jejich obsah nejde správně přečíst :(");
            }
            catch (Exception)
            {
                javascriptHelper.AddWarning("Bohužel došlo k chybě :(");
            }


            var indexView = _feedService.CreateIndexView();
            indexView.JavascriptHelper = javascriptHelper;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", indexView);
            }
            return RedirectToAction("Index");
        }
    }
}