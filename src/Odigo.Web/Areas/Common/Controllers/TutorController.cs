using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Business.Interfaces;
using Odigo.Web.Controllers;
using Odigo.Web.Areas.Teacher.Models;
using Odigo.Entities;
using Odigo.Model.Model;
using Odigo.Utility.Interfaces;
using Odigo.Utility;
using Odigo.Web.Models;
using System.Threading.Tasks;

namespace Odigo.Web.Areas.Common.Controllers
{
    public class TutorController : BaseController
    {
        private readonly IRepository _da;
        private readonly ILogger _logger;

        private TeacherSubscriptionViewModel _viewModel;

        public TutorController(IRepository da, ILogger logger)
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

        public ActionResult Details(long tid)
        {
           
            //var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            
            try
            {
                //tid = 47;

                if (_viewModel == null)
                {
                    _viewModel = new TeacherSubscriptionViewModel();
                }
                
                Model.Model.Teacher teacher = _da.GetModelBy<Model.Model.Teacher, TEACHER>(x => x.Person_Id == tid);
                if ((teacher != null && teacher.Person.Id > 0) && _viewModel.PersonAlreadyExist == false)
                {
                    _viewModel.Teacher = teacher;

                    _viewModel.Periods = _da.GetAll<Period, PERIOD>();
                    _viewModel.WeekDays = _da.GetAll<WeekDay, WEEK_DAY>();
                    _viewModel.StudentCategories = _da.GetAll<StudentCategory, STUDENT_CATEGORY>();

                    List<TeacherOLevelResultDetail> firstSittingOLevelResultDetails = null;
                    List<TeacherOLevelResultDetail> secondSittingOLevelResultDetails = null;
                    Referee referee = _da.GetModelBy<Referee, TEACHER_REFEREE>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    LoginDetail loginDetail = _da.GetModelBy<LoginDetail, PERSON_LOGIN>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    TeacherOLevelResult firstSittingOLevelResult = _da.GetModelBy<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 1);
                    TeacherOLevelResult secondSittingOLevelResult = _da.GetModelBy<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 2);
                    List<TeacherEducationalQualification> teacherEducationalQualifications = _da.GetModelsBy<TeacherEducationalQualification, TEACHER_EDUCATIONAL_QUALIFICATION>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    List<TeacherEmploymentHistory> teacherEmploymentHistories = _da.GetModelsBy<TeacherEmploymentHistory, TEACHER_EMPLOYMENT_HISTORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    List<TeacherAward> teacherAwards = _da.GetModelsBy<TeacherAward, TEACHER_AWARD>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    List<TeacherStudentCategory> teacherStudentCategories = _da.GetModelsBy<TeacherStudentCategory, TEACHER_STUDENT_CATEGORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    List<TeacherAvailability> teacherAvailabilities = _da.GetModelsBy<TeacherAvailability, TEACHER_AVAILABILITY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);




                    //_viewModel.Periods = await _da.GetAllAsync<Period, PERIOD>();
                    //_viewModel.WeekDays = await _da.GetAllAsync<WeekDay, WEEK_DAY>();
                    //_viewModel.StudentCategories = await _da.GetAllAsync<StudentCategory, STUDENT_CATEGORY>();

                    //List<TeacherOLevelResultDetail> firstSittingOLevelResultDetails = null;
                    //List<TeacherOLevelResultDetail> secondSittingOLevelResultDetails = null;
                    //Referee referee = await _da.GetModelByAsync<Referee, TEACHER_REFEREE>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //LoginDetail loginDetail = await _da.GetModelByAsync<LoginDetail, PERSON_LOGIN>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //TeacherOLevelResult firstSittingOLevelResult = await _da.GetModelByAsync<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 1);
                    //TeacherOLevelResult secondSittingOLevelResult = await _da.GetModelByAsync<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 2);
                    //List<TeacherEducationalQualification> teacherEducationalQualifications = await _da.GetModelsByAsync<TeacherEducationalQualification, TEACHER_EDUCATIONAL_QUALIFICATION>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //List<TeacherEmploymentHistory> teacherEmploymentHistories = await _da.GetModelsByAsync<TeacherEmploymentHistory, TEACHER_EMPLOYMENT_HISTORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //List<TeacherAward> teacherAwards = await _da.GetModelsByAsync<TeacherAward, TEACHER_AWARD>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //List<TeacherStudentCategory> teacherStudentCategories = await _da.GetModelsByAsync<TeacherStudentCategory, TEACHER_STUDENT_CATEGORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    //List<TeacherAvailability> teacherAvailabilities = await _da.GetModelsByAsync<TeacherAvailability, TEACHER_AVAILABILITY>(x => x.Person_Id == _viewModel.Teacher.Person.Id);





                    
                    if (referee != null)
                    {
                        _viewModel.Referee = referee;
                    }
                    if (loginDetail != null)
                    {
                        _viewModel.Teacher.LoginDetail = loginDetail;
                    }
                    else
                    {
                        _viewModel.Teacher.LoginDetail = new LoginDetail();
                        _viewModel.Teacher.LoginDetail.SecurityQuestion = new SecurityQuestion();
                        _viewModel.Teacher.LoginDetail.Role = new Role();
                    }


                    if (_viewModel.Periods != null && _viewModel.Periods.Count > 0)
                    {
                        _viewModel.WeekDays.Insert(0, new WeekDay());
                    }
                    if (_viewModel.WeekDays != null && _viewModel.WeekDays.Count > 0)
                    {
                        _viewModel.Periods.Insert(0, new Period());
                    }
                    _viewModel.TeacherStudentCategories = UiUtility.InitializeStudentCategory(_viewModel.StudentCategories);
                    _viewModel.TeacherAvailabilities = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);




                    if (teacherEducationalQualifications != null && teacherEducationalQualifications.Count > 0)
                    {
                        if (_viewModel.TeacherEducationalQualifications != null && _viewModel.TeacherEducationalQualifications.Count > 0)
                        {
                            foreach (TeacherEducationalQualification educationalQualification in teacherEducationalQualifications)
                            {
                                foreach (TeacherEducationalQualification qualification in _viewModel.TeacherEducationalQualifications)
                                {
                                    if (qualification.SchoolType.Id == educationalQualification.SchoolType.Id && qualification.Id <= 0)
                                    {
                                        qualification.Id = educationalQualification.Id;
                                        qualification.Person = educationalQualification.Person;
                                        qualification.SchoolType = educationalQualification.SchoolType;
                                        qualification.School = educationalQualification.School;
                                        qualification.YearOfAdmission = educationalQualification.YearOfAdmission;
                                        qualification.YearOfGraduation = educationalQualification.YearOfGraduation;
                                        qualification.Qualification = educationalQualification.Qualification;

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (firstSittingOLevelResult != null && firstSittingOLevelResult.Id > 0)
                    {
                        firstSittingOLevelResultDetails = _da.GetModelsBy<TeacherOLevelResultDetail, TEACHER_O_LEVEL_RESULT_DETAIL>(x => x.Teacher_O_Level_Result_Id == firstSittingOLevelResult.Id);
                    }
                    if (secondSittingOLevelResult != null && secondSittingOLevelResult.Id > 0)
                    {
                        secondSittingOLevelResultDetails = _da.GetModelsBy<TeacherOLevelResultDetail, TEACHER_O_LEVEL_RESULT_DETAIL>(x => x.Teacher_O_Level_Result_Id == secondSittingOLevelResult.Id);
                    }

                    if (teacherEmploymentHistories != null && teacherEmploymentHistories.Count > 0)
                    {
                        if (_viewModel.TeacherEmploymentHistories != null && _viewModel.TeacherEmploymentHistories.Count > 0)
                        {
                            for (int i = 0; i < teacherEmploymentHistories.Count; i++)
                            {
                                _viewModel.TeacherEmploymentHistories[i].Id = teacherEmploymentHistories[i].Id;
                                _viewModel.TeacherEmploymentHistories[i].Person = teacherEmploymentHistories[i].Person;
                                _viewModel.TeacherEmploymentHistories[i].Employer = teacherEmploymentHistories[i].Employer;
                                _viewModel.TeacherEmploymentHistories[i].LastPositionHeld = teacherEmploymentHistories[i].LastPositionHeld;
                                _viewModel.TeacherEmploymentHistories[i].StartYear = teacherEmploymentHistories[i].StartYear;
                                _viewModel.TeacherEmploymentHistories[i].EndYear = teacherEmploymentHistories[i].EndYear;
                            }
                        }
                    }

                    if (teacherAwards != null && teacherAwards.Count > 0)
                    {
                        if (_viewModel.TeacherAwards != null && _viewModel.TeacherAwards.Count > 0)
                        {
                            for (int i = 0; i < teacherAwards.Count; i++)
                            {
                                _viewModel.TeacherAwards[i].Id = teacherAwards[i].Id;
                                _viewModel.TeacherAwards[i].Person = teacherAwards[i].Person;
                                _viewModel.TeacherAwards[i].AwardBody = teacherAwards[i].AwardBody;
                                _viewModel.TeacherAwards[i].AwardName = teacherAwards[i].AwardName;
                                _viewModel.TeacherAwards[i].YearAwarded = teacherAwards[i].YearAwarded;
                            }
                        }
                    }

                    if (teacherStudentCategories != null && teacherStudentCategories.Count > 0)
                    {
                        if (_viewModel.TeacherStudentCategories != null && _viewModel.TeacherStudentCategories.Count > 0)
                        {
                            foreach (TeacherStudentCategory teacherStudentCategory in _viewModel.TeacherStudentCategories)
                            {
                                List<TeacherStudentCategory> studentCategories = teacherStudentCategories.Where(x => x.StudentCategory.Id == teacherStudentCategory.StudentCategory.Id).ToList();
                                if (studentCategories != null && studentCategories.Count > 0)
                                {
                                    teacherStudentCategory.IsSelected = true;
                                    teacherStudentCategory.Person = _viewModel.Teacher.Person;
                                }
                            }
                        }
                    }

                    if (teacherAvailabilities != null && teacherAvailabilities.Count > 0)
                    {
                        if (_viewModel.TeacherAvailabilities != null && _viewModel.TeacherAvailabilities.Count > 0)
                        {
                            foreach (TeacherAvailability teacherAvailability in _viewModel.TeacherAvailabilities)
                            {
                                List<TeacherAvailability> availabilities = teacherAvailabilities.Where(x => x.WeekDay.Id == teacherAvailability.WeekDay.Id && x.Period.Id == teacherAvailability.Period.Id).ToList();
                                if (availabilities != null && availabilities.Count > 0)
                                {
                                    teacherAvailability.IsAvailable = true;
                                    teacherAvailability.Person = _viewModel.Teacher.Person;
                                }
                            }
                        }
                    }

                    SetTeacherFirstSittingOLevelResult(firstSittingOLevelResult, firstSittingOLevelResultDetails);
                    SetTeacherSecondSittingOLevelResult(secondSittingOLevelResult, secondSittingOLevelResultDetails);
                }

                SetPassportUrl(_viewModel.Teacher.ImageFileUrl);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //stopWatch.Stop();
            //var executionTime = stopWatch.ElapsedMilliseconds;
            //ViewBag.Time = executionTime;

            return View(_viewModel);
        }

        private void SetPassportUrl(string passportUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passportUrl))
                {
                    _viewModel.Teacher.ImageFileUrl = FileManager.DEFAULT_AVATAR;
                }
                else
                {
                    _viewModel.Teacher.ImageFileUrl = passportUrl;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetTeacherFirstSittingOLevelResult(TeacherOLevelResult oLevelResult, List<TeacherOLevelResultDetail> oLevelResultDetails)
        {
            try
            {
                if (oLevelResult != null && oLevelResult.Id > 0)
                {
                    _viewModel.FirstSittingOLevelResult = new TeacherOLevelResult();
                    if (oLevelResult.Type != null)
                    {
                        _viewModel.FirstSittingOLevelResult.Type = oLevelResult.Type;
                    }
                    else
                    {
                        _viewModel.FirstSittingOLevelResult.Type = new OLevelType();
                    }

                    _viewModel.FirstSittingOLevelResult.ExamNumber = oLevelResult.ExamNumber;
                    _viewModel.FirstSittingOLevelResult.ExamYear = oLevelResult.ExamYear;

                    if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
                    {
                        for (int i = 0; i < oLevelResultDetails.Count; i++)
                        {
                            _viewModel.FirstSittingOLevelResultDetails[i].Subject = oLevelResultDetails[i].Subject;
                            _viewModel.FirstSittingOLevelResultDetails[i].Grade = oLevelResultDetails[i].Grade;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetTeacherSecondSittingOLevelResult(TeacherOLevelResult oLevelResult, List<TeacherOLevelResultDetail> oLevelResultDetails)
        {
            try
            {
                if (oLevelResult != null && oLevelResult.Id > 0)
                {
                    _viewModel.SecondSittingOLevelResult = new TeacherOLevelResult();
                    if (oLevelResult.Type != null)
                    {
                        _viewModel.SecondSittingOLevelResult.Type = oLevelResult.Type;
                    }
                    else
                    {
                        _viewModel.SecondSittingOLevelResult.Type = new OLevelType();
                    }

                    _viewModel.SecondSittingOLevelResult.ExamNumber = oLevelResult.ExamNumber;
                    _viewModel.SecondSittingOLevelResult.ExamYear = oLevelResult.ExamYear;

                    if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
                    {
                        for (int i = 0; i < oLevelResultDetails.Count; i++)
                        {
                            if (oLevelResultDetails[i].Subject != null)
                            {
                                _viewModel.SecondSittingOLevelResultDetails[i].Subject = oLevelResultDetails[i].Subject;
                            }
                            else
                            {
                                _viewModel.SecondSittingOLevelResultDetails[i].Subject = new OLevelSubject();
                            }
                            if (oLevelResultDetails[i].Grade != null)
                            {
                                _viewModel.SecondSittingOLevelResultDetails[i].Grade = oLevelResultDetails[i].Grade;
                            }
                            else
                            {
                                _viewModel.SecondSittingOLevelResultDetails[i].Grade = new OLevelGrade();
                            }
                        }
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