using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Odigo.Web.Areas.Teacher.Controllers
{
    public class VerificationController : Controller
    {
        // GET: Teacher/VerificationStatus
        public ActionResult Status()
        {
            return View();
        }
    }
}