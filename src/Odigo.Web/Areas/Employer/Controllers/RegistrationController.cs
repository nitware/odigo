using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Business.Interfaces;
using Odigo.Utility.Interfaces;
using Odigo.Web.Controllers;
using Odigo.Notification.Interfaces;
using Odigo.Model.Model;
using Odigo.Notification;
using Odigo.Web.Areas.Employer.Models;
using Odigo.Entities;
using Odigo.Web.Models;
using System.Linq.Expressions;

namespace Odigo.Web.Areas.Employer.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository _da;
        private readonly IRegistrationFactory _registrationFactory;
        

        public EmployerRegistrationViewModel _viewModel;

        //public RegistrationController(IRepository da, IRegistrationFactory registrationFactory, ILogger logger, INotificationProvider<Email, bool> emailEngine)

        public RegistrationController(IRepository da, IRegistrationFactory registrationFactory, ILogger logger)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (registrationFactory == null)
            {
                throw new ArgumentNullException("registrationFactory");
            }

            _da = da;
            _logger = logger;
            _registrationFactory = registrationFactory;
        }

        public ActionResult SchoolForm()
        {
            return View();
        }

        //public ActionResult SchoolForm()
        //{
        //    try
        //    {
        //        Email email = new Email();
        //        email.Addressee = "Odigo International School Lekki";
        //        email.MailBody = "Testing Odigo mail for the first time. What do you think?";
        //        email.MailDate = DateTime.Now;
        //        email.MailTitle = "TESTING";
        //        email.MailTo = "egenti.daniel@gmail.com";
        //        email.NameFrom = "Odigo Team, Nitware Solutions";
        //        email.Salutation = "Dear John";
        //        email.Subject = "Subject";

        //        string message = null;
        //        bool sent = _emailEngine.Send(email);
        //        if (sent)
        //        {
        //            message = "Message has been sent successfully";
        //        }
        //        else
        //        {
        //            message = "Message sending operation failed! Please try again";
        //        }

        //        SetMessage(message, Message.Category.Information);
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        SetMessage(ex.Message, Message.Category.Error);
        //    }

        //    return View();
        //}

        //[HttpPost]
        public ActionResult UpdateStudentCategory(List<int> ids)
        {
            EmployerRegistrationViewModel viewModel = (EmployerRegistrationViewModel)TempData["EmployerRegistrationViewModel"];

            try
            {
                SetStudentCategory(ids, viewModel);
                SetEmployerStudentCategory(ids, viewModel);
                SetSelectedEmployerStudentCategory(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            SaveUiState(viewModel);
            return PartialView("_DesiredTime", viewModel);
        }
        private void SetStudentCategory(List<int> ids, EmployerRegistrationViewModel viewModel)
        {
            try
            {
                if (viewModel.StudentCategories != null && viewModel.StudentCategories.Count > 0)
                {
                    foreach (StudentCategory studentCategory in viewModel.StudentCategories)
                    {
                        foreach (int id in ids)
                        {
                            if (id == studentCategory.Id)
                            {
                                studentCategory.IsSelected =  true;
                                continue;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }
        private void SetEmployerStudentCategory(List<int> ids, EmployerRegistrationViewModel viewModel)
        {
            try
            {
                if (ids != null && ids.Count > 0)
                {
                    List<EmployerStudentCategory> employerStudentCategories = null;
                    if (viewModel.SelectedEmployerStudentCategories == null || viewModel.SelectedEmployerStudentCategories.Count <= 0)
                    {
                        List<EmployerStudentCategory> defaultEmployerStudentCategories = viewModel.EmployerStudentCategories;
                        if (defaultEmployerStudentCategories != null && defaultEmployerStudentCategories.Count > 0)
                        {
                            if (viewModel.StudentCategories != null && viewModel.StudentCategories.Count > 0)
                            {
                                employerStudentCategories = new List<EmployerStudentCategory>();
                                foreach (int id in ids)
                                {
                                    EmployerStudentCategory employerStudentcategory = defaultEmployerStudentCategories.Where(x => x.StudentCategory.Id == id).SingleOrDefault();
                                    employerStudentCategories.Add(employerStudentcategory);
                                }

                                viewModel.SelectedEmployerStudentCategories = employerStudentCategories;
                            }
                        }
                    }
                    else
                    {
                        List<EmployerStudentCategory> currentSelectedEmployerStudentCategories = new List<EmployerStudentCategory>();
                        List<EmployerStudentCategory> previouslySelectedEmployerStudentCategories = viewModel.SelectedEmployerStudentCategories;
                        List<EmployerStudentCategory> loadedSelectedEmployerStudentCategories = viewModel.LoadedEmployerStudentCategories;
                       
                        foreach (int id in ids)
                        {
                            EmployerStudentCategory defaultEmployerStudentCategory = viewModel.EmployerStudentCategories.Where(x => x.StudentCategory.Id == id).SingleOrDefault();
                            EmployerStudentCategory previousEmployerStudentCategory = previouslySelectedEmployerStudentCategories.Where(x => x.StudentCategory.Id == id).SingleOrDefault();
                            
                            if (previousEmployerStudentCategory != null)
                            {
                                currentSelectedEmployerStudentCategories.Add(previousEmployerStudentCategory);
                            }
                            else
                            {
                                EmployerStudentCategory loadedEmployerStudentCategory = loadedSelectedEmployerStudentCategories.Where(x => x.StudentCategory.Id == id).SingleOrDefault();
                                if (loadedEmployerStudentCategory != null)
                                {
                                    currentSelectedEmployerStudentCategories.Add(loadedEmployerStudentCategory);
                                }
                                else
                                {
                                    currentSelectedEmployerStudentCategories.Add(defaultEmployerStudentCategory);
                                }
                            }
                        }

                        viewModel.SelectedEmployerStudentCategories = currentSelectedEmployerStudentCategories;
                    }
                }
                else
                {
                    viewModel.SelectedEmployerStudentCategories = new List<EmployerStudentCategory>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }

        private void SetSelectedEmployerStudentCategory(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.SelectedEmployerStudentCategories != null && viewModel.SelectedEmployerStudentCategories.Count > 0)
                {
                    int i = 0;
                    foreach (EmployerStudentCategory employerStudentCategory in viewModel.SelectedEmployerStudentCategories)
                    {
                        if (employerStudentCategory.TeacherType != null && employerStudentCategory.StudentCategory != null)
                        {
                            employerStudentCategory.StudentCategory = viewModel.StudentCategories.Where(x => x.Id == employerStudentCategory.StudentCategory.Id).SingleOrDefault();
                            ViewData["TeacherTypes" + i] = new SelectList(viewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employerStudentCategory.TeacherType.Id);
                            ViewData["NoOfStudents" + i] = new SelectList(viewModel.NoOfStudentSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employerStudentCategory.NoOfStudent);
                        }
                        else
                        {
                            employerStudentCategory.StudentCategory = new StudentCategory();
                            ViewData["TeacherTypes" + i] = new SelectList(viewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                            ViewData["NoOfStudents" + i] = new SelectList(viewModel.NoOfStudentSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }

        public ActionResult Form(long? eid, int? ptid)
        {
            EmployerRegistrationViewModel viewModel = null;
            if (!eid.HasValue)
            {
                viewModel = (EmployerRegistrationViewModel)TempData["EmployerRegistrationViewModel"];
            }

            try
            {
                PopulateAllDropDowns(viewModel);

                if (viewModel != null)
                {
                    _viewModel = viewModel;
                }

                //_viewModel.Employer.Person.Type = (PersonType)TempData["PersonType"];

                PersonType personType = (PersonType)TempData["PersonType"];
                if (personType != null && personType.Id > 0)
                {
                    _viewModel.Employer.Person.Type = personType;
                }
               
                //ptid = 3;
                //eid = 89;

                //_viewModel.Employer.Person.Type.Id = ptid.GetValueOrDefault();
                Model.Model.Employer employer = _da.GetModelBy<Model.Model.Employer, EMPLOYER>(x => x.Person_Id == eid.GetValueOrDefault());
                if ((employer != null && employer.Person.Id > 0) && _viewModel.PersonAlreadyExist == false)
                {
                    _viewModel.Employer = employer;
                    _viewModel.PersonAlreadyExist = true;
                   
                    LoginDetail loginDetail = _da.GetModelBy<LoginDetail, PERSON_LOGIN>(x => x.Person_Id == _viewModel.Employer.Person.Id);
                    List<EmployerStudentCategory> employerStudentCategories = _da.GetModelsBy<EmployerStudentCategory, EMPLOYER_STUDENT_CATEGORY>(x => x.Person_Id == _viewModel.Employer.Person.Id).ToList();
                                      
                    if (loginDetail != null)
                    {
                        _viewModel.Employer.LoginDetail = loginDetail;
                    }
                    else
                    {
                        _viewModel.Employer.LoginDetail = new LoginDetail();
                        _viewModel.Employer.LoginDetail.SecurityQuestion = new SecurityQuestion();
                        _viewModel.Employer.LoginDetail.Role = new Role();
                    }

                    List<int> ids = null;
                    if (employerStudentCategories != null && employerStudentCategories.Count > 0)
                    {
                        foreach (EmployerStudentCategory employerStudentCategory in employerStudentCategories)
                        {
                            List<EmployerDesiredTime> employerDesiredTimes = _da.GetModelsBy<EmployerDesiredTime, EMPLOYER_DESIRED_TIME>(x => x.Employer_Student_Category_Id == employerStudentCategory.Id);
                            EmployerStudentCategory defaultEmployerStudentCategory = _viewModel.EmployerStudentCategories.Where(x => x.StudentCategory.Id == employerStudentCategory.StudentCategory.Id).SingleOrDefault();

                            if (defaultEmployerStudentCategory != null)
                            {
                                employerStudentCategory.DesiredTimes = defaultEmployerStudentCategory.DesiredTimes;

                                if (employerStudentCategory.DesiredTimes != null && employerStudentCategory.DesiredTimes.Count > 0)
                                {
                                    foreach (EmployerDesiredTime employerDesiredTime in employerStudentCategory.DesiredTimes)
                                    {
                                        EmployerDesiredTime desiredTime = employerDesiredTimes.Where(x => x.WeekDay.Id == employerDesiredTime.WeekDay.Id && x.Period.Id == employerDesiredTime.Period.Id).SingleOrDefault();
                                        if (desiredTime != null)
                                        {
                                            employerDesiredTime.IsAvailable = true;
                                        }
                                    }
                                }
                            }
                        }

                        _viewModel.LoadedEmployerStudentCategories = employerStudentCategories;
                        _viewModel.SelectedEmployerStudentCategories = employerStudentCategories;
                        ids = employerStudentCategories.Select(x => x.StudentCategory.Id).ToList();
                    }

                    SetStudentCategory(ids, _viewModel);
                    SetEmployerStudentCategory(ids, _viewModel);
                    SetSelectedEmployerStudentCategory(_viewModel);

                    SetLgaIfExist(_viewModel.Employer.Person.State, _viewModel.Employer.Person.Lga);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
                        
            SaveUiState(_viewModel);
            TempData["PersonType"] = _viewModel.Employer.Person.Type;
            return View(_viewModel);
        }

        [HttpPost]
        public ActionResult Form(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                UpdateDropdownSelectList(viewModel);

                ModelState.Remove("Employer.Website");
                ModelState.Remove("Employer.Person.Country.Id");
                ModelState.Remove("Employer.Person.IsBlacklisted");
                ModelState.Remove("Employer.Person.Type.Id");
                ModelState.Remove("Employer.Name");
                ModelState.Remove("Employer.Sex.Id");

                if (ModelState.IsValid)
                {
                    if (IncompleteUserInput(viewModel))
                    {
                        SaveUiState(viewModel);
                        return View(viewModel);
                    }

                    SaveUiState(viewModel);
                    return RedirectToAction("FormPreview", "Registration");

                    //return Json(new { isDone = true, message = "Registration has been saved successfully" }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            SaveUiState(viewModel);
            return View(viewModel);

            //return Json(new { isDone = true, message = "Registration saved operation failed! Please try again" }, "text/html", JsonRequestBehavior.AllowGet);

        }

        private bool IncompleteUserInput(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                if (viewModel.SelectedEmployerStudentCategories == null || viewModel.SelectedEmployerStudentCategories.Count <= 0)
                {
                    SetMessage("No Student Category selected! Please select at least one student category", ApplicationMessage.Category.Error);
                    return true;
                }
                else
                {
                    for (int i = 0; i < viewModel.SelectedEmployerStudentCategories.Count; i++)
                    {
                        bool noEntry = viewModel.SelectedEmployerStudentCategories[i].NoOfStudent <= 0 || viewModel.SelectedEmployerStudentCategories[i].TeacherType.Id <= 0 ;
                       //bool filled = viewModel.SelectedEmployerStudentCategories[i].NoOfStudent > 0 && viewModel.SelectedEmployerStudentCategories[i].TeacherType.Id > 0;
                        if (noEntry == true)
                        {
                            SetMessage("Incomplete entry found on row " + (i + 1) + " for " + viewModel.SelectedEmployerStudentCategories[i].StudentCategory.Name + " category of Desired Time! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }

                        if (viewModel.SelectedEmployerStudentCategories[i].DesiredTimes != null && viewModel.SelectedEmployerStudentCategories[i].DesiredTimes.Count > 0)
                        {
                            List<EmployerDesiredTime> desiredTimes = viewModel.SelectedEmployerStudentCategories[i].DesiredTimes.Where(x => x.IsAvailable == true).ToList();
                            if (desiredTimes == null || desiredTimes.Count <= 0)
                            {
                                SetMessage("No Desired Time specified for " + viewModel.SelectedEmployerStudentCategories[i].StudentCategory.Name + "! Please modify", ApplicationMessage.Category.Error);
                                return true;
                            }
                        }
                        else
                        {
                            SetMessage("Desired Time not set for " + viewModel.SelectedEmployerStudentCategories[i].StudentCategory.Name + "! category Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }

                        //if (noEntry == false && filled == false)
                        //{
                        //    SetMessage("Incomplete entry found on row " + (i + 1) + " of Employment history! Please modify", Message.Category.Error);
                        //    return true;
                        //}

                        //if (viewModel.TeacherEmploymentHistories[i].StartYear > viewModel.TeacherEmploymentHistories[i].EndYear)
                        //{
                        //    SetMessage("Start Year greater than End Year on row " + (i + 1) + " of Employment History section! Please modify", Message.Category.Error);
                        //    return true;
                        //}
                    }
                }


                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult FormPreview()
        {
            EmployerRegistrationViewModel viewModel = (EmployerRegistrationViewModel)TempData["EmployerRegistrationViewModel"];

            try
            {
                if (viewModel != null)
                {
                    if (viewModel.Employer != null && viewModel.Employer.Person != null)
                    {
                        if (viewModel.Employer != null && viewModel.Employer.Sex != null)
                        {
                            viewModel.Employer.Sex = viewModel.Sexs.Where(x => x.Id == viewModel.Employer.Sex.Id).SingleOrDefault();
                        }

                        viewModel.Employer.Person.State = viewModel.States.Where(x => x.Id == viewModel.Employer.Person.State.Id).SingleOrDefault();
                        viewModel.Employer.Person.Lga = _da.GetModelBy<Lga, LGA>(x => x.Lga_Id == viewModel.Employer.Person.Lga.Id);
                        viewModel.Employer.LoginDetail.SecurityQuestion = viewModel.SecurityQuestions.Where(x => x.Id == viewModel.Employer.LoginDetail.SecurityQuestion.Id).SingleOrDefault();
                    }

                    if (viewModel.SelectedEmployerStudentCategories != null && viewModel.SelectedEmployerStudentCategories.Count > 0)
                    {
                        foreach (EmployerStudentCategory employerStudentCategory in viewModel.SelectedEmployerStudentCategories)
                        {
                            employerStudentCategory.Person = viewModel.Employer.Person;
                            employerStudentCategory.TeacherType = viewModel.TeacherTypes.Where(x => x.Id == employerStudentCategory.TeacherType.Id).SingleOrDefault();

                            if (employerStudentCategory.StudentCategory != null && employerStudentCategory.StudentCategory.Id > 0)
                            {
                                employerStudentCategory.StudentCategory = viewModel.StudentCategories.Where(x => x.Id == employerStudentCategory.StudentCategory.Id).SingleOrDefault();
                            }
                            else
                            {
                                employerStudentCategory.StudentCategory = new StudentCategory() { Id = 0 };
                            }
                        }
                    }
                }
                else
                {

                }

                viewModel.LoadedEmployerStudentCategories = viewModel.SelectedEmployerStudentCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            SaveUiState(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult FormPreview(EmployerRegistrationViewModel vm)
        {
            JsonResult json = null;
            EmployerRegistrationViewModel viewModel = (EmployerRegistrationViewModel)TempData["EmployerRegistrationViewModel"];

            try
            {
                //var form = Request.Form;

                Model.Model.Employer employer = new Model.Model.Employer();
                employer.Person = viewModel.Employer.Person;
                employer.Person.Type = viewModel.Employer.Person.Type;
                employer.Website = viewModel.Employer.Website;
                employer.Name = viewModel.Employer.Name;

                if (viewModel.Employer != null && viewModel.Employer.Sex != null && viewModel.Employer.Sex.Id > 0)
                {
                    employer.Sex = viewModel.Employer.Sex;
                }

                List<EmployerStudentCategory> employerStudentCategories = vm.SelectedEmployerStudentCategories;
                if (employerStudentCategories != null && employerStudentCategories.Count > 0)
                {
                    foreach (EmployerStudentCategory employerStudentCategory in employerStudentCategories)
                    {
                        if (employerStudentCategory.DesiredTimes == null || employerStudentCategory.DesiredTimes.Count <= 0)
                        {
                            throw new Exception("Desired Times not set! Please cantact your system administrator");
                        }

                        List<EmployerDesiredTime> employerDesiredTimes = employerStudentCategory.DesiredTimes.Where(x => x.IsAvailable == true).ToList();
                        employerStudentCategory.DesiredTimes = employerDesiredTimes;
                    }
                }

                employer.StudentCategories = employerStudentCategories;

                IRegistration registrationService = null;
                if (viewModel.PersonAlreadyExist)
                {
                    registrationService = _registrationFactory.Create(Subscription.EditEmployer);
                }
                else
                {
                    employer.Person.IsBlacklisted = false;
                    employer.Person.DateEntered = DateTime.Now;
                    employer.Person.Country = new Country() { Id = 1 }; //temp

                    employer.LoginDetail = viewModel.Employer.LoginDetail;
                    employer.LoginDetail.Person = viewModel.Employer.Person;
                    employer.LoginDetail.Role = new Role() { Id = 1 };
                    employer.LoginDetail.IsFirstLogon = false;
                    employer.LoginDetail.IsActivated = true;
                    employer.LoginDetail.IsLocked = false;

                    Payment payment = new Payment();
                    payment.Mode = new PaymentMode() { Id = 1 };
                    payment.DateEntered = DateTime.Now;
                    payment.Person = employer.Person;
                    payment.Paid = false;
                    
                    List<Payment> payments = new List<Payment>();
                    payments.Add(payment);

                    employer.Payments = payments;
                    registrationService = _registrationFactory.Create(Subscription.SubmitEmployer);
                }

                Model.Model.Employer newEmployer = registrationService.Save(employer);
                if (newEmployer != null && newEmployer.Person != null && newEmployer.Person.Id > 0)
                {
                    viewModel.Employer.Person.Id = newEmployer.Person.Id;
                    viewModel.Employer.PaymentSlip = newEmployer.PaymentSlip;

                    SaveUiState(viewModel);
                    TempData["PaymentSlip"] = viewModel.Employer.PaymentSlip;

                    json = Json(new { isDone = true, message = "Registration has been saved successfully" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    json = Json(new { isDone = false, message = "Registration save operation failed! Please try again" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                json = Json(new { isDone = false, message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }

            SaveUiState(viewModel);

            return json;
        }

        private void InitializeDropDowns()
        {
            try
            {
                _viewModel.Sexs = _da.GetAll<Sex, SEX>();
                _viewModel.States = _da.GetAll<State, STATE>();
                _viewModel.SecurityQuestions = _da.GetAll<SecurityQuestion, SECURITY_QUESTION>();
                _viewModel.TeacherTypes = _da.GetAll<TeacherType, TEACHER_TYPE>();
                _viewModel.NoOfStudents = DropdownUtility.CreateNumberListFrom(1, 5);

                _viewModel.SexSelectList = DropdownUtility.PopulateModelSelectList<Sex, SEX>(_da, false);
                _viewModel.StateSelectList = DropdownUtility.PopulateModelSelectList<State, STATE>(_da, false);
                _viewModel.CountrySelectList = DropdownUtility.PopulateModelSelectList<Country, COUNTRY>(_da, false);
                _viewModel.SecurityQuestionSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.SecurityQuestions, "Id", "Question");
                _viewModel.TeacherTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.TeacherTypes, true);
                _viewModel.NoOfStudentSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.NoOfStudents, true);

                _viewModel.Periods = _da.GetAll<Period, PERIOD>();
                _viewModel.WeekDays = _da.GetAll<WeekDay, WEEK_DAY>();
                _viewModel.StudentCategories = _da.GetAll<StudentCategory, STUDENT_CATEGORY>();

                if (_viewModel.Periods != null && _viewModel.Periods.Count > 0)
                {
                    _viewModel.WeekDays.Insert(0, new WeekDay());
                }
                if (_viewModel.WeekDays != null && _viewModel.WeekDays.Count > 0)
                {
                    _viewModel.Periods.Insert(0, new Period());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }

        private void UpdateDropdownSelectList(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                EmployerRegistrationViewModel storedViewModel = (EmployerRegistrationViewModel)TempData["EmployerRegistrationViewModel"];
                if (storedViewModel != null)
                {
                    viewModel.SexSelectList = storedViewModel.SexSelectList;
                    viewModel.StateSelectList = storedViewModel.StateSelectList;
                    viewModel.CountrySelectList = storedViewModel.CountrySelectList;
                    viewModel.SecurityQuestionSelectList = storedViewModel.SecurityQuestionSelectList;
                    viewModel.TeacherTypeSelectList = storedViewModel.TeacherTypeSelectList;
                    viewModel.NoOfStudentSelectList = storedViewModel.NoOfStudentSelectList;
                    
                    viewModel.Sexs = storedViewModel.Sexs;
                    viewModel.States = storedViewModel.States;
                    viewModel.Periods = storedViewModel.Periods;
                    viewModel.WeekDays = storedViewModel.WeekDays;
                    viewModel.SecurityQuestions = storedViewModel.SecurityQuestions;
                    viewModel.TeacherTypes = storedViewModel.TeacherTypes;
                    viewModel.NoOfStudents = storedViewModel.NoOfStudents;
                    viewModel.EmployerStudentCategories = storedViewModel.EmployerStudentCategories;
                    viewModel.LoadedEmployerStudentCategories = storedViewModel.LoadedEmployerStudentCategories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }

        private void PopulateAllDropDowns(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    _viewModel = new EmployerRegistrationViewModel();

                    InitializeDropDowns();
                    _viewModel.EmployerStudentCategories = UiUtility.InitializeStudentCategory(_viewModel.EmployerStudentCategories, _viewModel.WeekDays, _viewModel.Periods);

                    ViewBag.Lgas = new SelectList(new List<Lga>(), DropdownUtility.ID, DropdownUtility.NAME);
                    ViewBag.SecurityQuestions = _viewModel.SecurityQuestionSelectList;
                    ViewBag.Countries = _viewModel.CountrySelectList;
                    ViewBag.States = _viewModel.StateSelectList;
                    ViewBag.Sexs = _viewModel.SexSelectList;
                }
                else
                {
                    _viewModel = viewModel;
                    if (_viewModel.Employer != null && _viewModel.Employer.Sex != null)
                    {
                        ViewBag.Sexs = new SelectList(_viewModel.SexSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Employer.Sex.Id);
                    }
                    else
                    {
                        ViewBag.Sexs = new SelectList(_viewModel.SexSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                    }

                    ViewBag.States = new SelectList(_viewModel.StateSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Employer.Person.State.Id);
                    ViewBag.Countries = new SelectList(_viewModel.CountrySelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Employer.Person.Country.Id);

                    if (_viewModel.Employer != null && _viewModel.Employer.LoginDetail != null && _viewModel.Employer.LoginDetail.SecurityQuestion != null)
                    {
                        ViewBag.SecurityQuestions = new SelectList(_viewModel.SecurityQuestionSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Employer.LoginDetail.SecurityQuestion.Id);
                    }
                    else
                    {
                        ViewBag.SecurityQuestions = new SelectList(_viewModel.SecurityQuestionSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                    }

                    if (_viewModel.EmployerStudentCategories == null || _viewModel.EmployerStudentCategories.Count <= 0)
                    {
                        viewModel.EmployerStudentCategories = UiUtility.InitializeStudentCategory(_viewModel.EmployerStudentCategories, _viewModel.WeekDays, _viewModel.Periods);
                    }

                    SetLgaIfExist(_viewModel.Employer.Person.State, _viewModel.Employer.Person.Lga);
                }

                SetSelectedEmployerStudentCategory(_viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveUiState(EmployerRegistrationViewModel viewModel)
        {
            try
            {
                PopulateAllDropDowns(viewModel);
                TempData["EmployerRegistrationViewModel"] = viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }
        }

        //private void SetSelectedEmployerStudentCategory(EmployerRegistrationViewModel viewModel)
        //{
        //    try
        //    {
        //        if (viewModel != null && viewModel.EmployerStudentCategories != null && viewModel.EmployerStudentCategories.Count > 0)
        //        {
        //            int i = 0;
        //            foreach (EmployerStudentCategory employerStudentCategory in viewModel.EmployerStudentCategories)
        //            {
        //                if (employerStudentCategory.TeacherType != null && employerStudentCategory.StudentCategory != null)
        //                {
        //                    employerStudentCategory.StudentCategory = viewModel.StudentCategories.Where(x => x.Id == employerStudentCategory.StudentCategory.Id).SingleOrDefault();
        //                    ViewData["TeacherTypes" + i] = new SelectList(viewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employerStudentCategory.TeacherType.Id);
        //                    ViewData["NoOfStudents" + i] = new SelectList(viewModel.NoOfStudentSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employerStudentCategory.NoOfStudent);
        //                }
        //                else
        //                {
        //                    employerStudentCategory.StudentCategory = new StudentCategory();
        //                    ViewData["TeacherTypes" + i] = new SelectList(viewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
        //                    ViewData["NoOfStudents" + i] = new SelectList(viewModel.NoOfStudentSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
        //                }

        //                i++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        SetMessage(ex.Message, Message.Category.Error);
        //    }
        //}

        public JsonResult GetLocalGovernmentsByState(string id)
        {
            try
            {
                Expression<Func<LGA, bool>> selector = lg => lg.State_Id == id;
                List<Lga> lgas = _da.GetModelsBy<Lga, LGA>(selector);

                return Json(new SelectList(lgas, DropdownUtility.ID, DropdownUtility.NAME), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SetLgaIfExist(State state, Lga lga)
        {
            try
            {
                if (state != null && !string.IsNullOrEmpty(state.Id))
                {
                    List<Lga> lgas = _da.GetModelsBy<Lga, LGA>(l => l.State_Id == state.Id);
                    if (lga != null && lga.Id > 0)
                    {
                        ViewBag.Lgas = new SelectList(lgas, DropdownUtility.ID, DropdownUtility.NAME, lga.Id);
                    }
                    else
                    {
                        ViewBag.Lgas = DropdownUtility.PopulateModelSelectListHelper(lgas, false);
                        //ViewBag.Lgas = new SelectList(lgas, DropdownUtility.ID, DropdownUtility.NAME);
                    }
                }
                else
                {
                    ViewBag.Lgas = new SelectList(new List<Lga>(), DropdownUtility.ID, DropdownUtility.NAME);
                    if (_viewModel.StateSelectList != null && _viewModel.StateSelectList.Count > 0)
                    {
                        ViewBag.States = new SelectList(_viewModel.StateSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, "");
                    }
                    else
                    {
                        ViewBag.States = new SelectList(new List<State>(), DropdownUtility.VALUE, DropdownUtility.NAME);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }







    }
}