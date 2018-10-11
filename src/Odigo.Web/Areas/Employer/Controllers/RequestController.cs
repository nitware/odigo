using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Business.Interfaces;
using Odigo.Web.Areas.Employer.Models;
using Odigo.Model.Model;
using Odigo.Entities;
using Odigo.Web.Models;
using Odigo.Web.Controllers;
using Odigo.Utility.Interfaces;

namespace Odigo.Web.Areas.Employer.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IRepository _da;
        private readonly ILogger _logger;
        private readonly IService _serviceCharge;
        private readonly ITeacherFinder _teacherFinder;
        private readonly IRequestService _requestService;

        public RequestViewModel _viewModel;

        public RequestController(IRepository da, IRequestService requestService, ILogger logger, ITeacherFinder teacherFinder, IService serviceCharge)
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
            if (serviceCharge == null)
            {
                throw new ArgumentNullException("serviceCharge");
            }
            if (requestService == null)
            {
                throw new ArgumentNullException("requestService");
            }

            _da = da;
            _logger = logger;
            _teacherFinder = teacherFinder;
            _serviceCharge = serviceCharge;
            _requestService = requestService;
        }

        public ActionResult CostImplication(long tid, long epid)
        {
            try
            {
                _viewModel = new Models.RequestViewModel();

                int highestQualificationId = 0;
                _viewModel.Periods = _da.GetAll<Period, PERIOD>();
                _viewModel.WeekDays = _da.GetAll<WeekDay, WEEK_DAY>();
                _viewModel.EmployerStudentCategories = _da.GetModelsBy<EmployerStudentCategory, EMPLOYER_STUDENT_CATEGORY>(esc => esc.Person_Id == epid);
                _viewModel.TeacherStudentCategories = _da.GetModelsBy<TeacherStudentCategory, TEACHER_STUDENT_CATEGORY>(tsc => tsc.Person_Id == tid);

                List<TeacherEducationalQualification> educationalQualifications = _da.GetModelsBy<TeacherEducationalQualification, TEACHER_EDUCATIONAL_QUALIFICATION>(x => x.Person_Id == tid);
                if (educationalQualifications != null && educationalQualifications.Count > 0)
                {
                    highestQualificationId = educationalQualifications.Max(eq => eq.Qualification.Category.Id);
                }

                if (_viewModel.Periods != null && _viewModel.Periods.Count > 0)
                {
                    _viewModel.WeekDays.Insert(0, new WeekDay());
                }
                if (_viewModel.WeekDays != null && _viewModel.WeekDays.Count > 0)
                {
                    _viewModel.Periods.Insert(0, new Period());
                }

                Person person = new Person() { Id = tid };
                List<TeacherAvailability> teacherAvailabilities = _da.GetModelsBy<TeacherAvailability, TEACHER_AVAILABILITY>(ta => ta.Person_Id == tid);
                _viewModel.Teacher = _teacherFinder.GetBy(person);

                _viewModel.RequestCostImplications = new List<RequestForEmploymentCostImplication>();
                if (_viewModel.EmployerStudentCategories != null && _viewModel.EmployerStudentCategories.Count > 0)
                {
                    _viewModel.Employer = _viewModel.EmployerStudentCategories[0].Person;

                    foreach (EmployerStudentCategory employerStudentCategory in _viewModel.EmployerStudentCategories)
                    {
                        RequestForEmploymentCostImplication requestCostImplication = new RequestForEmploymentCostImplication();
                        TeachingCost teachingCost = _da.GetModelBy<TeachingCost, TEACHING_COST>(tc => tc.Qualification_Category_Id == highestQualificationId && tc.Student_Category_Id == employerStudentCategory.StudentCategory.Id);
                        TeacherStudentCategory teacherStudentCategory = _viewModel.TeacherStudentCategories.Where(sc => sc.StudentCategory.Id == employerStudentCategory.StudentCategory.Id).SingleOrDefault();

                        if (teacherStudentCategory != null && teacherStudentCategory.Id > 0)
                        {
                            List<TeacherAvailability> teacherPeriodsAvailable = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);
                            requestCostImplication.EmployerStudentCategory = employerStudentCategory;

                            List<RequestForEmploymentTeacherAvailability> requestForEmploymentTeacherAvailabilities = new List<RequestForEmploymentTeacherAvailability>();
                            if (teacherPeriodsAvailable != null && teacherPeriodsAvailable.Count > 0)
                            {
                                decimal totalCost = 0;
                                foreach (TeacherAvailability teacherAvailability in teacherPeriodsAvailable)
                                {
                                    TeacherAvailability availability = teacherAvailabilities.Where(x => x.WeekDay.Id == teacherAvailability.WeekDay.Id && x.Period.Id == teacherAvailability.Period.Id).SingleOrDefault();
                                    RequestForEmploymentTeacherAvailability requestForEmploymentTeacherAvailability = new RequestForEmploymentTeacherAvailability();
                                    

                                    if (availability != null && availability.Id > 0)
                                    {
                                        requestForEmploymentTeacherAvailability.TeacherAvailability = availability;
                                        requestForEmploymentTeacherAvailability.TeacherAvailability.IsAvailable = true;

                                        if (teachingCost != null)
                                        {
                                            totalCost += teachingCost.Amount;
                                            requestForEmploymentTeacherAvailability.TeachingCost = teachingCost;
                                        }
                                    }
                                    else
                                    {
                                        requestForEmploymentTeacherAvailability.TeacherAvailability = new TeacherAvailability();
                                        requestForEmploymentTeacherAvailability.TeacherAvailability.IsAvailable = false;
                                    }

                                    requestForEmploymentTeacherAvailabilities.Add(requestForEmploymentTeacherAvailability);
                                }
                                
                                //decimal totalCost = requestForEmploymentTeacherAvailabilities.Sum(x => x.TeachingCost.Amount);
                                requestCostImplication.MonthlyPay = 4 * totalCost * employerStudentCategory.NoOfStudent;
                                requestCostImplication.TeacherAvailabilities = requestForEmploymentTeacherAvailabilities;
                            }
                        }
                        else
                        {
                            requestCostImplication.EmployerStudentCategory = employerStudentCategory;
                            requestCostImplication.TeacherAvailabilities = null;

                            //requestCostImplication.TeacherPeriodsAvailable = null;
                        }

                        _viewModel.RequestCostImplications.Add(requestCostImplication);
                    }

                    //foreach (TeacherStudentCategory teacherStudentCategory in _viewModel.TeacherStudentCategories)
                    //{
                    //    TeachingCost teachingCost = _da.GetModelBy<TeachingCost, TEACHING_COST>(tc => tc.Qualification_Category_Id == highestQualificationId && tc.Student_Category_Id == teacherStudentCategory.StudentCategory.Id);
                    //    EmployerStudentCategory employerStudentCategory = _viewModel.EmployerStudentCategories.Where(sc => sc.StudentCategory.Id == teacherStudentCategory.StudentCategory.Id).SingleOrDefault();
                    //    if (employerStudentCategory == null || employerStudentCategory.Id <= 0)
                    //    {
                    //        RequestCostImplication costImplication = new RequestCostImplication();
                    //        costImplication.TeacherAvailabilities = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);
                    //        costImplication.StudentCategory = teacherStudentCategory.StudentCategory;
                    //        costImplication.TeachingCost = teachingCost;
                    //        costImplication.NoOfStudent = 0;

                    //        if (costImplication.TeacherAvailabilities != null && costImplication.TeacherAvailabilities.Count > 0)
                    //        {
                    //            foreach (TeacherAvailability availability in costImplication.TeacherAvailabilities)
                    //            {
                    //                List<TeacherAvailability> availabilities = teacherAvailabilities.Where(x => x.WeekDay.Id == availability.WeekDay.Id && x.Period.Id == availability.Period.Id).ToList();
                    //                if (availabilities != null && availabilities.Count > 0)
                    //                {
                    //                    availability.IsAvailable = true;
                    //                    if (teachingCost != null)
                    //                    {
                    //                        availability.Cost = teachingCost.Amount;
                    //                    }
                    //                }
                    //            }
                    //        }

                    //        _viewModel.RequestCostImplications.Add(costImplication);
                    //    }
                    //}

                    _viewModel.ServiceCharge = _serviceCharge.GetRequestCostBy(new QualificationCategory() { Id = highestQualificationId });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            TempData["RequestViewModel"] = _viewModel;
            return View(_viewModel);
        }

        [HttpPost]
        public JsonResult Send(RequestViewModel viewModel)
        {
            JsonResult json = null;

            try
            {
                _viewModel = (RequestViewModel)TempData["RequestViewModel"];

                if (_viewModel != null)
                {
                    Request request = new Request();
                    request.FromPerson = _viewModel.Employer;
                    request.ToPerson = _viewModel.Teacher.Person;
                    request.RequestMessage = _da.GetModelBy<Message, MESSAGE>(x => x.Message_Id == 1); //new Message() { Id = 1 };
                    request.ServiceCharge = _viewModel.ServiceCharge;
                    request.Status = new RequestStatus() { Id = (int)RequestStatus.List.Pending };
                    request.Date = DateTime.Now;
                    
                    List< RequestForEmploymentCostImplication> requestForEmploymentCostImplications = _viewModel.RequestCostImplications.Where(x => x.MonthlyPay > 0 && x.EmployerStudentCategory.Id > 0 && x.EmployerStudentCategory.NoOfStudent > 0).ToList();
                    if (requestForEmploymentCostImplications != null && requestForEmploymentCostImplications.Count > 0)
                    {
                        foreach(RequestForEmploymentCostImplication requestForEmploymentCostImplication in requestForEmploymentCostImplications)
                        {
                            requestForEmploymentCostImplication.TeacherAvailabilities = requestForEmploymentCostImplication.TeacherAvailabilities.Where(x => x.TeacherAvailability.IsAvailable == true).ToList();
                        }
                    }

                    request.ForEmploymentCostImplications = requestForEmploymentCostImplications;

                    PaymentSlip paymentSlip = _requestService.Send(request);
                    if (paymentSlip == null || paymentSlip.Payment == null || paymentSlip.Payment.Id <= 0)
                    {
                        json = Json(new { isValid = false, message = "Sending of request failed! Please try again" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        TempData["PaymentViewModel"] = null;
                        TempData["PaymentSlip"] = paymentSlip;
                        json = Json(new { isValid = true, message = "Request has been successfully sent, and email and SMS has also been sent to the recipient" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    json = Json(new { isValid = false, message = "Error occurred! Empty input variables detected! Please contact your system administrator" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                json = Json(new { isValid = false, message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }

            TempData["RequestViewModel"] = _viewModel;
            return json;
        }



    }


}