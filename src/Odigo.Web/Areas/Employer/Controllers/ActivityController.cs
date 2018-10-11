using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Web.Areas.Employer.Models;

namespace Odigo.Web.Areas.Employer.Controllers
{
    public class ActivityController : Controller
    {
        private ActivityViewModel _viewModel;
        private RequestController _requestController;

        public ActivityController(RequestController requestController, ActivityViewModel viewModel)
        {
            _requestController = requestController;
            _viewModel = viewModel;
        }

        public ActionResult Page(long? epid, long? tid, int pid)
        {
            TempData["EmployerActivityPageId"] = pid;
            
            if (TempData["EmployerActivityPageId"] != null)
            {
                _viewModel.PageId = (int)TempData["EmployerActivityPageId"];
            }
            else
            {
                _viewModel.PageId = 0;
            }

            _viewModel.TeacherId = tid.GetValueOrDefault();
            _viewModel.EmployerId = epid.GetValueOrDefault();

            TempData["ActivityViewModel"] = _viewModel;
            TempData["EmployerActivityPageId"] = _viewModel.PageId;
            return View(_viewModel);
        }

        [HttpGet]
        public ActionResult GetView(int pid)
        {
            _viewModel.PageId = pid;
            ActionResult pView = null;

            TempData["EmployerActivityPageId"] = pid;
            ActivityViewModel viewModel = (ActivityViewModel)TempData["ActivityViewModel"];

            switch (pid)
            {
                case 1:
                    {
                        pView = PartialView("~/Areas/Employer/Views/Activity/_DesiredTime.cshtml", viewModel);
                        break;
                    }
                case 2:
                    {
                        pView = PartialView("~/Areas/Employer/Views/Activity/_FindTeacher.cshtml", viewModel);
                        break;
                    }
                case 3:
                    {
                        ActionResult _regVie = _requestController.CostImplication(viewModel.TeacherId, viewModel.EmployerId);
                        viewModel.RequestViewModel = _requestController._viewModel;
                        pView = PartialView("~/Areas/Employer/Views/Request/CostImplication.cshtml", viewModel);
                        break;
                    }
                default:
                    {
                        pView = null;
                        break;
                    }
            }

            TempData["ActivityViewModel"] = viewModel;
            return pView;
        }




    }
}