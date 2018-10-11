using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Web.Controllers;
using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Utility.Interfaces;
using System.Threading.Tasks;
using Odigo.Web.Models;

namespace Odigo.Web.Areas.Common.Controllers
{
    public class SearchController : BaseSearchController
    {
        public SearchController(IRepository da, ILogger logger, ITeacherFinder teacherFinderService) :base(da, logger, teacherFinderService)
        {
           
        }

        public ActionResult Result()
        {
            List<Model.Model.Teacher> teachers = null;
            BaseSearchViewModel viewModel = null;

            try
            {
                teachers = (List<Model.Model.Teacher>)TempData["Teachers"];
                viewModel = (BaseSearchViewModel)TempData["BaseSearchViewModel"];

                base.PopulateAllDropDowns(viewModel);
                _searchViewModel.Teachers = teachers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            TempData["Teachers"] = _searchViewModel.Teachers;
            TempData["BaseSearchViewModel"] = _searchViewModel;
            return View(_searchViewModel);
        }

        









    }


}