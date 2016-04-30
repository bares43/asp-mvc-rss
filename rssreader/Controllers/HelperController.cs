using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rssreader.Models;

namespace rssreader.Controllers
{
    public class HelperController : Controller
    {
        // GET: Helper
        public ActionResult Javascript(JavascriptHelper helper)
        {
            return View(helper);
        }
    }
}