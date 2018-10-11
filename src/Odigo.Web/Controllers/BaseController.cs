using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Model.Model;

namespace Odigo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetMessage(string message, ApplicationMessage.Category messageType)
        {
            ApplicationMessage msg = new ApplicationMessage(message, (int)messageType);
            TempData["Message"] = msg;
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }




    }



}