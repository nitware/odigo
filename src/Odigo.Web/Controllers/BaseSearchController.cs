using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Odigo.Entities;
using Odigo.Model.Model;
using Odigo.Utility.Interfaces;
using Odigo.Business.Interfaces;
using Odigo.Web.Models;

namespace Odigo.Web.Controllers
{
    public abstract class BaseSearchController : BaseController
    {
        protected readonly IRepository _da;
        protected readonly ILogger _logger;
        protected readonly ITeacherFinder _teacherFinder;

        protected BaseSearchViewModel _searchViewModel;

        public BaseSearchController(IRepository da, ILogger logger, ITeacherFinder teacherFinder)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (teacherFinder == null)
            {
                throw new ArgumentNullException("teacherFinder");
            }

            _da = da;
            _logger = logger;
            _teacherFinder = teacherFinder;
        }

        public void PopulateAllDropDowns(BaseSearchViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    //_searchViewModel = new BaseSearchViewModel();
                    InitializeDropDowns();

                    //ViewBag.States = _searchViewModel.StateSelectList;
                    //ViewBag.StudentCategories = _searchViewModel.StudentCategorySelectList;
                    //ViewBag.Qualifications = _searchViewModel.QualificationSelectList;
                    //ViewBag.TeacherTypes = _searchViewModel.TeacherTypeSelectList;
                }
                else
                {
                    _searchViewModel = viewModel;

                    ViewBag.States = new SelectList(_searchViewModel.StateSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.State.Id);
                    ViewBag.Qualifications = new SelectList(_searchViewModel.QualificationSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.Qualification.Id);
                    ViewBag.StudentCategories = new SelectList(_searchViewModel.StudentCategorySelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.StudentCategory.Id);
                    ViewBag.TeacherTypes = new SelectList(_searchViewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.TeacherType.Id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void InitializeDropDowns()
        {
            try
            {
                if (_searchViewModel == null)
                {
                    _searchViewModel = new BaseSearchViewModel();
                }

                _searchViewModel.States = _da.GetAll<State, STATE>();
                _searchViewModel.Qualifications = _da.GetModelsBy<QualificationCategory, QUALIFICATION_CATEGORY>(q => q.Qualification_Category_Id >= 3);
                _searchViewModel.TeacherTypes = _da.GetAll<TeacherType, TEACHER_TYPE>();
                _searchViewModel.StudentCategories = _da.GetAll<StudentCategory, STUDENT_CATEGORY>();

                _searchViewModel.StateSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.States, false, "-- Select Location --");
                _searchViewModel.QualificationSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.Qualifications, false, "-- Select Qualification --");
                _searchViewModel.TeacherTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.TeacherTypes, false, "-- Select Work Mode --");
                _searchViewModel.StudentCategorySelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.StudentCategories, false, "-- Select Child Category --");

                ViewBag.States = _searchViewModel.StateSelectList;
                ViewBag.StudentCategories = _searchViewModel.StudentCategorySelectList;
                ViewBag.Qualifications = _searchViewModel.QualificationSelectList;
                ViewBag.TeacherTypes = _searchViewModel.TeacherTypeSelectList;

                _searchViewModel.DropdownDataLoaded = true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public async Task PopulateAllDropDowns(BaseSearchViewModel viewModel)
        //{
        //    try
        //    {
        //        if (viewModel == null)
        //        {
        //            _searchViewModel = new BaseSearchViewModel();
        //            await InitializeDropDowns();

        //            ViewBag.States = _searchViewModel.StateSelectList;
        //            ViewBag.StudentCategories = _searchViewModel.StudentCategorySelectList;
        //            ViewBag.Qualifications = _searchViewModel.QualificationSelectList;
        //            ViewBag.TeacherTypes = _searchViewModel.TeacherTypeSelectList;
        //        }
        //        else
        //        {
        //            _searchViewModel = viewModel;

        //            ViewBag.States = new SelectList(_searchViewModel.StateSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.State.Id);
        //            ViewBag.Qualifications = new SelectList(_searchViewModel.QualificationSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.Qualification.Id);
        //            ViewBag.StudentCategories = new SelectList(_searchViewModel.StudentCategorySelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.StudentCategory.Id);
        //            ViewBag.TeacherTypes = new SelectList(_searchViewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _searchViewModel.TeacherType.Id);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private async Task InitializeDropDowns()
        //{
        //    try
        //    {
        //        //_viewModel.States = _da.GetAll<State, STATE>();
        //        //_viewModel.Qualifications = _da.GetAll<Qualification, QUALIFICATION>();
        //        //_viewModel.TeacherTypes = _da.GetAll<TeacherType, TEACHER_TYPE>();
        //        //_viewModel.StudentCategories = _da.GetAll<StudentCategory, STUDENT_CATEGORY>();

        //        //Task<List<State>> states = _da.GetAllAsync<State, STATE>();
        //        //Task<List<Qualification>> qualifications = _da.GetAllAsync<Qualification, QUALIFICATION>();
        //        //Task<List<TeacherType>> teacherTypes = _da.GetAllAsync<TeacherType, TEACHER_TYPE>();
        //        //Task<List<StudentCategory>> studentCategories = _da.GetAllAsync<StudentCategory, STUDENT_CATEGORY>();

        //        //await Task.WhenAll(states, qualifications, teacherTypes, studentCategories);

        //        //_viewModel.States = states.Result;
        //        //_viewModel.Qualifications = qualifications.Result;
        //        //_viewModel.TeacherTypes = teacherTypes.Result;
        //        //_viewModel.StudentCategories = studentCategories.Result;




        //        _searchViewModel.States = await _da.GetAllAsync<State, STATE>();
        //        _searchViewModel.Qualifications = await _da.GetAllAsync<QualificationCategory, QUALIFICATION_CATEGORY>();
        //        _searchViewModel.TeacherTypes = await _da.GetAllAsync<TeacherType, TEACHER_TYPE>();
        //        _searchViewModel.StudentCategories = await _da.GetAllAsync<StudentCategory, STUDENT_CATEGORY>();

        //        _searchViewModel.StateSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.States, false, "-- Select Location --");
        //        _searchViewModel.QualificationSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.Qualifications, false, "-- Select Qualification --");
        //        _searchViewModel.TeacherTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.TeacherTypes, false, "-- Select Work Mode --");
        //        _searchViewModel.StudentCategorySelectList = DropdownUtility.PopulateModelSelectListHelper(_searchViewModel.StudentCategories, false, "-- Select Child Category --");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private void UpdateDropdownSelectList(BaseSearchViewModel viewModel)
        {
            try
            {
                BaseSearchViewModel storedViewModel = (BaseSearchViewModel)TempData["HomeViewModel"];
                if (storedViewModel != null)
                {
                    viewModel.StateSelectList = storedViewModel.StateSelectList;
                    viewModel.QualificationSelectList = storedViewModel.QualificationSelectList;
                    viewModel.TeacherTypeSelectList = storedViewModel.TeacherTypeSelectList;
                    viewModel.StudentCategorySelectList = storedViewModel.StudentCategorySelectList;

                    viewModel.States = storedViewModel.States;
                    viewModel.Qualifications = storedViewModel.Qualifications;
                    viewModel.StudentCategories = storedViewModel.StudentCategories;
                    viewModel.TeacherTypes = storedViewModel.TeacherTypes;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult FindTeacherBy(BaseSearchViewModel viewModel)
        {
            List<Teacher> teachers = null;

            try
            {
                teachers = FindTeacherByHelper(viewModel.TeacherType, viewModel.StudentCategory, viewModel.Qualification, viewModel.State);
                TempData["Teachers"] = teachers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            return PartialView("_Teachers", teachers);
        }

        protected List<Teacher> FindTeacherByHelper(TeacherType teacherType, StudentCategory studentCategory, QualificationCategory qualification, State location)
        {
            List<Teacher> teachers = null;

            try
            {

                if ((teacherType != null && teacherType.Id > 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification == null || qualification.Id <= 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification == null || qualification.Id <= 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(studentCategory);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification != null && qualification.Id > 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(qualification);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification == null || qualification.Id <= 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(location);
                }



                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification == null || qualification.Id <= 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, studentCategory);
                }
                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification != null && qualification.Id > 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, qualification);
                }
                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification == null || qualification.Id <= 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, location);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification != null && qualification.Id > 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(studentCategory, qualification);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification == null || qualification.Id <= 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(studentCategory, location);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification != null && qualification.Id > 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(qualification, location);
                }



                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification != null && qualification.Id > 0) && (location == null || string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, studentCategory, qualification);
                }
                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification == null || qualification.Id <= 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, studentCategory, location);
                }
                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory == null || studentCategory.Id <= 0) && (qualification != null && qualification.Id > 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, qualification, location);
                }
                else if ((teacherType == null || teacherType.Id <= 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification != null && qualification.Id > 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(studentCategory, qualification, location);
                }


                else if ((teacherType != null && teacherType.Id > 0) && (studentCategory != null && studentCategory.Id > 0) && (qualification != null && qualification.Id > 0) && (location != null && !string.IsNullOrWhiteSpace(location.Id)))
                {
                    teachers = _teacherFinder.GetBy(teacherType, studentCategory, qualification, location);
                }

                return teachers;
            }
            catch (Exception)
            {
                throw;
            }
        }







    }
}