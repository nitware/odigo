using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Web.Models;
using Odigo.Model.Model;
using Odigo.Business.Interfaces;
using Odigo.Utility.Interfaces;
using Odigo.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Odigo.Web.Controllers
{
    public class HomeController : BaseSearchController
    {
        private readonly INews _newsService;
        private readonly IQuickLinker _quickLinkService;

        private HomeViewModel _viewModel;

        public HomeController(HomeViewModel viewModel, IRepository da, ILogger logger, IQuickLinker quickLinkService, INews newsService, ITeacherFinder teacherFinder) : base(da, logger, teacherFinder)
        {
            if (newsService == null)
            {
                throw new ArgumentNullException("newsService");
            }
            if (quickLinkService == null)
            {
                throw new ArgumentNullException("quickLinkService");
            }

            _newsService = newsService;
            _quickLinkService = quickLinkService;
            _viewModel = viewModel;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

        public ActionResult Index()
        //public async Task<ActionResult> Index()
        {
            const int TEN = 10;
            const int FIVE = 5;
            HomeViewModel viewModel = (HomeViewModel)TempData["HomeViewModel"];

            TempData["imageUrl"] = null;
            TempData["PersonType"] = null;
            TempData["PaymentSlip"] = null;
            TempData["TeacherSubscriptionViewModel"] = null;
            TempData["EmployerRegistrationViewModel"] = null;
            TempData["PaymentViewModel"] = null;
            TempData["SearchViewModel"] = null;
            TempData["Teacher"] = null;

            try
            {
                //_viewModel = viewModel == null ? new HomeViewModel() : viewModel;

                //await Task.Factory.StartNew(() =>
                //{
                //    PopulateAllDropDowns(_viewModel.BaseSearchViewModel);

                //    _viewModel.BaseSearchViewModel = _searchViewModel;
                //    _viewModel.News = _newsService.GetTop(FIVE);
                //    _viewModel.QuickLinks = _quickLinkService.GetTop(FIVE);
                //    _viewModel.BaseSearchViewModel.Teachers = _teacherFinder.GetTopBy(TEN, t => t.Verification_Status_Id == 3);
                //});



                //PopulateAllDropDowns(_viewModel.BaseSearchViewModel);

                //_viewModel.BaseSearchViewModel = _searchViewModel;
                //_viewModel.News = _newsService.GetTop(FIVE);
                //_viewModel.QuickLinks = _quickLinkService.GetTop(FIVE);
                //_viewModel.BaseSearchViewModel.Teachers = _teacherFinder.GetTopBy(TEN, t => t.Verification_Status_Id == 3);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            TempData["HomeViewModel"] = _viewModel;
            return View(_viewModel);
        }

        public ActionResult GetLatestVerifiedTeachers()
        {
            const int TEN = 10;
            //HomeViewModel viewModel = (HomeViewModel)TempData["HomeViewModel"];

            try
            {
                //if (viewModel == null)
                //{
                //    viewModel = new HomeViewModel();
                //}

                //_viewModel = viewModel == null ? new HomeViewModel() : viewModel;
                if (_viewModel.BaseSearchViewModel == null)
                {
                    _viewModel.BaseSearchViewModel = new BaseSearchViewModel();
                }

                _viewModel.BaseSearchViewModel.Teachers = _teacherFinder.GetTopBy(TEN, t => t.Verification_Status_Id == 3);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //TempData["HomeViewModel"] = viewModel;
            return PartialView("_Teachers", _viewModel.BaseSearchViewModel.Teachers);
        }

        public ActionResult PopulateSearchDropdowns()
        {
            //HomeViewModel viewModel = (HomeViewModel)TempData["HomeViewModel"];

            try
            {
                //if (viewModel == null)
                //{
                //    viewModel = new HomeViewModel();
                //}


                //_searchViewModel.States = _da.GetAll<State, STATE>();
                //_searchViewModel.Qualifications = _da.GetModelsBy<QualificationCategory, QUALIFICATION_CATEGORY>(q => q.Qualification_Category_Id >= 3);
                //_searchViewModel.TeacherTypes = _da.GetAll<TeacherType, TEACHER_TYPE>();
                //_searchViewModel.StudentCategories



                //_viewModel = viewModel == null ? new HomeViewModel() : viewModel;
                if (_viewModel.BaseSearchViewModel != null)
                {
                    if (!_viewModel.BaseSearchViewModel.DropdownDataLoaded)
                    {
                        base.InitializeDropDowns();
                        _viewModel.BaseSearchViewModel = _searchViewModel;
                    }
                }
               

                PopulateAllDropDowns(_viewModel.BaseSearchViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //TempData["HomeViewModel"] = _viewModel;
            return PartialView("_SearchPanel", _viewModel.BaseSearchViewModel);
        }

        public ActionResult GetLatestNews()
        {
            const int FIVE = 5;
            //HomeViewModel viewModel = (HomeViewModel)TempData["HomeViewModel"];

            try
            {
                //if (viewModel == null)
                //{
                //    viewModel = new HomeViewModel();
                //}

                //_viewModel = viewModel == null ? new HomeViewModel() : viewModel;
                _viewModel.News = _newsService.GetTop(FIVE);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //TempData["HomeViewModel"] = _viewModel;
            return PartialView("_NewsPanel", _viewModel.News);
        }

        public ActionResult GetTopQuickLinks()
        {
            const int FIVE = 5;
            //HomeViewModel viewModel = (HomeViewModel)TempData["HomeViewModel"];

            try
            {
                //if (viewModel == null)
                //{
                //    viewModel = new HomeViewModel();
                //}

                _viewModel.QuickLinks = _quickLinkService.GetTop(FIVE);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //TempData["HomeViewModel"] = viewModel;
            return PartialView("_QuickLinksPanel", _viewModel.QuickLinks);
        }

        [HttpGet]
        public JsonResult GetTeacherBy(BaseSearchViewModel viewModel)
        {
            JsonResult json = null;
            List<Teacher> teachers = null;

            try
            {
                if (viewModel != null && viewModel != null)
                {
                    teachers = FindTeacherByHelper(viewModel.TeacherType, viewModel.StudentCategory, viewModel.Qualification, viewModel.State);

                    json = Json(new { isSuccessful = true, message = teachers.Count + " teachers found!" }, "text/html", JsonRequestBehavior.AllowGet);
                    TempData["Teachers"] = teachers;
                }
                else
                {
                    json = Json(new { isSuccessful = false, message = "No teacher was found! Please change your search criteria" }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                json = Json(new { isSuccessful = false, message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }

            return json;
        }



        //[HttpGet]
        //public JsonResult GetTeacherBy(HomeViewModel viewModel)
        //{
        //    JsonResult json = null;
        //    List<Teacher> teachers = null;

        //    try
        //    {
        //        //StudentCategory studentCategory = new StudentCategory() { Id = scid };
        //        //List<Teacher> teachers = _teacherFinder.GetBy(studentCategory);

        //        if (viewModel != null && viewModel.BaseSearchViewModel != null)
        //        {
        //            teachers = FindTeacherByHelper(viewModel.BaseSearchViewModel.TeacherType, viewModel.BaseSearchViewModel.StudentCategory, viewModel.BaseSearchViewModel.Qualification, viewModel.BaseSearchViewModel.State);

        //            json = Json(new { isSuccessful = true, message = teachers.Count + " teachers found!" }, "text/html", JsonRequestBehavior.AllowGet);
        //            TempData["Teachers"] = teachers;
        //        }
        //        else
        //        {
        //            json = Json(new { isSuccessful = false, message = "No teacher was found! Please change your search criteria" }, "text/html", JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        json = Json(new { isSuccessful = false, message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
        //    }

        //    return json;
        //}




    }
}