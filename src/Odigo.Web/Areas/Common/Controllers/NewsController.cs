using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Odigo.Web.Areas.Common.Controllers
{
    public class NewsController : Controller
    {
        // GET: Common/News
        public ActionResult Index()
        {
            return View();
        }
    }
}