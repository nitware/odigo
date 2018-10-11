using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Web.Controllers;
using Odigo.Utility.Interfaces;
using Odigo.Business.Interfaces;
using Odigo.Entities;
using Odigo.Model.Model;
using Odigo.Web.Models;
using Odigo.Web.Areas.Common.Models;
using System.Threading.Tasks;

namespace Odigo.Web.Areas.Common.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository _da;
        private ServiceCostViewModel _viewModel;

        public ServiceController(IRepository da, ILogger logger)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _da = da;
            _logger = logger;
        }

        public JsonResult CreatePersonTypeModel(int ptid)
        {
            JsonResult json = null;

            try
            {
                TempData["PersonType"] = new PersonType() { Id = ptid };
                json = Json(new { created = true, message = "Person Type Model was successfully " }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
                json = Json(new { created = false, message = "Person Type Model creation failed!" }, "text/html", JsonRequestBehavior.AllowGet);
            }

            return json;
        }

        public async Task<ActionResult> Cost(string ptid)
        {
            ServiceCostViewModel viewModel = (ServiceCostViewModel)TempData["TeachingCostViewModel"];

            try
            {
                if (viewModel == null)
                {
                    _viewModel = new ServiceCostViewModel();
                }
                else
                {
                    _viewModel = viewModel;
                }

                if (!string.IsNullOrWhiteSpace(ptid))
                {
                    if (_viewModel.Subscribers == null || _viewModel.Subscribers.Count <= 0)
                    {
                        _viewModel.Subscribers = _da.GetModelsBy<PersonType, PERSON_TYPE>(p => p.Person_Type_Id != 1);
                    }

                    List<PersonType> personTypes = null;
                    if (_viewModel.Subscribers != null && _viewModel.Subscribers.Count > 0)
                    {
                        personTypes = _viewModel.Subscribers.Where(u => ptid.Contains(u.Id.ToString())).ToList();
                        if (personTypes != null && personTypes.Count > 0)
                        {
                            _viewModel.PersonTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(personTypes, false, "-- Select Your Category --");
                            if (personTypes.Count == 1)
                            {
                                ViewBag.PersonTypes = new SelectList(_viewModel.PersonTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, personTypes[0].Id);
                            }
                            else
                            {
                                ViewBag.PersonTypes = _viewModel.PersonTypeSelectList;
                            }
                        }
                        else
                        {
                            _viewModel.PersonTypeSelectList = new List<SelectListItem>();
                        }
                    }
                    else
                    {
                        _viewModel.PersonTypeSelectList = new List<SelectListItem>();
                    }
                }
                
                await LoadPersonType();
                await InitializeTeachingCostHelper();
                await InitializeTeacherAvailabilityHelper();

                _viewModel.ServiceCharges = await _da.GetAllAsync<ServiceCharge, SERVICE_CHARGE>();
                _viewModel.TeacherAvailabilities = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);
                _viewModel.TeachingCosts = UiUtility.InitializeTeachingCost(_viewModel.StudentCategories, _viewModel.QualificationCategories, _viewModel.ExistingTeachingCosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            TempData["TeachingCostViewModel"] = _viewModel;
            return View(_viewModel);
        }

        private async Task LoadPersonType()
        {
            try
            {
                if (_viewModel.PersonTypes == null || _viewModel.PersonTypes.Count <= 0)
                {
                    _viewModel.PersonTypes = await _da.GetAllAsync<PersonType, PERSON_TYPE>();
                    if (_viewModel.PersonTypes != null && _viewModel.PersonTypes.Count > 0)
                    {
                        _viewModel.PersonTypes = _viewModel.PersonTypes.Where(x => x.Id != 1).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task InitializeTeacherAvailabilityHelper()
        {
            try
            {
                if (_viewModel.Periods == null || _viewModel.Periods.Count <= 0)
                {
                    _viewModel.Periods = await _da.GetAllAsync<Period, PERIOD>();
                    if (_viewModel.Periods != null && _viewModel.Periods.Count > 0)
                    {
                        _viewModel.Periods.Insert(0, new Period());
                    }
                }
                if (_viewModel.WeekDays == null || _viewModel.WeekDays.Count <= 0)
                {
                    _viewModel.WeekDays = await _da.GetAllAsync<WeekDay, WEEK_DAY>();
                    if (_viewModel.WeekDays != null && _viewModel.WeekDays.Count > 0)
                    {
                        _viewModel.WeekDays.Insert(0, new WeekDay());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private void InitializeTeacherAvailability(TeachingCostViewModel viewModel)
        //{
        //    try
        //    {
        //        InitializeTeacherAvailabilityHelper(viewModel);

        //        viewModel.TeacherAvailabilities = new List<TeacherAvailability>();
        //        for (int j = 0; j < viewModel.Periods.Count; j++)
        //        {
        //            for (int i = 0; i < viewModel.WeekDays.Count; i++)
        //            {
        //                if (i == 0 || j == 0)
        //                {
        //                    continue;
        //                }

        //                TeacherAvailability teacherAvailability = new TeacherAvailability();
        //                teacherAvailability.Period = viewModel.Periods[j];
        //                teacherAvailability.WeekDay = viewModel.WeekDays[i];
        //                //teacherAvailability.Person = viewModel.Person;
        //                teacherAvailability.IsAvailable = false;

        //                viewModel.TeacherAvailabilities.Add(teacherAvailability);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private void InitializeTeachingCost()
        //{
        //    try
        //    {
        //        InitializeTeachingCostHelper();

        //        _viewModel.TeachingCosts = new List<TeachingCost>();
        //        List<TeachingCost> teachingCosts = _da.GetAll<TeachingCost, TEACHING_COST>();

        //        for (int i = 0; i < _viewModel.QualificationCategories.Count; i++)
        //        {
        //            for (int j = 0; j < _viewModel.StudentCategories.Count; j++)
        //            {

        //                if (i == 0 || j == 0)
        //                {
        //                    continue;
        //                }

        //                TeachingCost teachingCost = teachingCosts.Where(t => t.StudentCategory.Id == _viewModel.StudentCategories[j].Id && t.QualificationCategory.Id == _viewModel.QualificationCategories[i].Id).SingleOrDefault();

        //                _viewModel.TeachingCosts.Add(teachingCost);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private async Task InitializeTeachingCostHelper()
        {
            try
            {
                if (_viewModel.StudentCategories == null || _viewModel.StudentCategories.Count <= 0)
                {
                    _viewModel.StudentCategories = await _da.GetAllAsync<StudentCategory, STUDENT_CATEGORY>();
                    if (_viewModel.StudentCategories != null && _viewModel.StudentCategories.Count > 0)
                    {
                        _viewModel.StudentCategories.Insert(0, new StudentCategory());
                    }
                }
                if (_viewModel.QualificationCategories == null || _viewModel.QualificationCategories.Count <= 0)
                {
                    _viewModel.QualificationCategories = _da.GetModelsBy<QualificationCategory, QUALIFICATION_CATEGORY>(x => x.Qualification_Category_Id > 2);
                    if (_viewModel.QualificationCategories != null && _viewModel.QualificationCategories.Count > 0)
                    {
                        _viewModel.QualificationCategories.Insert(0, new QualificationCategory());
                    }
                }
                if (_viewModel.ExistingTeachingCosts == null || _viewModel.ExistingTeachingCosts.Count <= 0)
                {
                    _viewModel.ExistingTeachingCosts = await _da.GetAllAsync<TeachingCost, TEACHING_COST>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }







    }
}