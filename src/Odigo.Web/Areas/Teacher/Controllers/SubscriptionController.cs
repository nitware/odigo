using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Entities;
using Odigo.Web.Models;
using Odigo.Model.Model;
using Odigo.Web.Controllers;
using Odigo.Business.Interfaces;
using Odigo.Web.Areas.Teacher.Models;
using System.Linq.Expressions;
using Odigo.Utility;
using Odigo.Utility.Interfaces;

namespace Odigo.Web.Areas.Teacher.Controllers
{
    //public class SubscriptionController : ImageUploadController

    public class SubscriptionController : BaseController
    {
        private const int START_YEAR = 1950;
        private const string JUNK_FOLDER_PATH = "Junk";
        private const string DESTIONATION_FOLDER_PATH = "Person";

        private readonly ILogger _logger;
        private readonly IRepository _da;
        private readonly ISubscriptionFactory _subscriptionFactory;
        private readonly IFileManager _fileManager;

        private TeacherSubscriptionViewModel _viewModel;
        
        public SubscriptionController(IRepository da, IFileManager fileManager, ISubscriptionFactory subscriptionFactory, ILogger logger)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (fileManager == null)
            {
                throw new ArgumentNullException("fileManager");
            }
            if (subscriptionFactory == null)
            {
                throw new ArgumentNullException("subscriptionFactory");
            }

            _da = da;
            _logger = logger;
            _fileManager = fileManager;
            _subscriptionFactory = subscriptionFactory;
        }
        
        [HttpPost]
        public JsonResult UploadFile(FormCollection form)
        {
            JsonResult result = null;

            try
            {
                string folderPath = "/Content/Junk";
                string id = form["Teacher.Person.Id"].ToString();
                string imageUrl = form["Teacher.ImageFileUrl"].ToString();
                HttpPostedFileBase file = Request.Files["File"];

                

                result = _fileManager.Upload(file, id, folderPath, imageUrl);
                if (result.Data is Dictionary<string, dynamic>)
                {
                    Dictionary<string, dynamic> values = (Dictionary<string, dynamic>)result.Data;
                    if (values != null && values.Count > 0)
                    {
                        TempData["imageUploaded"] = true;
                        TempData["imageUrl"] = values["imageUrl"].ToString();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                //TempData["imageUrl"] = null;
                return Json(new { isUploaded = false, message = ex.Message, imageUrl = "" }, "text/html", JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        public ActionResult Form(long? tid)
        {
            TeacherSubscriptionViewModel viewModel = null;
            if (!tid.HasValue)
            {
                viewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];
            }

            try
            {
                PopulateAllDropDowns(viewModel);

                if (viewModel != null)
                {
                    _viewModel = viewModel;
                }
                
                Model.Model.Teacher teacher = _da.GetModelBy<Model.Model.Teacher, TEACHER>(x => x.Person_Id == tid.GetValueOrDefault());
                if ((teacher != null && teacher.Person.Id > 0) && _viewModel.PersonAlreadyExist == false)
                {
                    _viewModel.Teacher = teacher;
                    _viewModel.PersonAlreadyExist = true;
                    TempData["imageUrl"] = _viewModel.Teacher.ImageFileUrl;

                    List<TeacherOLevelResultDetail> firstSittingOLevelResultDetails = null;
                    List<TeacherOLevelResultDetail> secondSittingOLevelResultDetails = null;
                    Referee referee = _da.GetModelBy<Referee, TEACHER_REFEREE>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    LoginDetail loginDetail = _da.GetModelBy<LoginDetail, PERSON_LOGIN>(x => x.Person_Id == _viewModel.Teacher.Person.Id);
                    TeacherOLevelResult firstSittingOLevelResult = _da.GetModelBy<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 1);
                    TeacherOLevelResult secondSittingOLevelResult = _da.GetModelBy<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>(x => x.Person_Id == _viewModel.Teacher.Person.Id && x.O_Level_Exam_Sitting_Id == 2);
                    List<TeacherEducationalQualification> teacherEducationalQualifications = _da.GetModelsBy<TeacherEducationalQualification, TEACHER_EDUCATIONAL_QUALIFICATION>(x => x.Person_Id == _viewModel.Teacher.Person.Id).ToList();
                    List<TeacherEmploymentHistory> teacherEmploymentHistories = _da.GetModelsBy<TeacherEmploymentHistory, TEACHER_EMPLOYMENT_HISTORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id).ToList();
                    List<TeacherAward> teacherAwards = _da.GetModelsBy<TeacherAward, TEACHER_AWARD>(x => x.Person_Id == _viewModel.Teacher.Person.Id).ToList();
                    List<TeacherStudentCategory> teacherStudentCategories = _da.GetModelsBy<TeacherStudentCategory, TEACHER_STUDENT_CATEGORY>(x => x.Person_Id == _viewModel.Teacher.Person.Id).ToList();
                    List<TeacherAvailability> teacherAvailabilities = _da.GetModelsBy<TeacherAvailability, TEACHER_AVAILABILITY>(x => x.Person_Id == _viewModel.Teacher.Person.Id).ToList();

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
                        firstSittingOLevelResultDetails = _da.GetModelsBy<TeacherOLevelResultDetail, TEACHER_O_LEVEL_RESULT_DETAIL>(x => x.Teacher_O_Level_Result_Id == firstSittingOLevelResult.Id).ToList();
                    }
                    if (secondSittingOLevelResult != null && secondSittingOLevelResult.Id > 0)
                    {
                        secondSittingOLevelResultDetails = _da.GetModelsBy<TeacherOLevelResultDetail, TEACHER_O_LEVEL_RESULT_DETAIL>(x => x.Teacher_O_Level_Result_Id == secondSittingOLevelResult.Id).ToList();
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
                                       
                    SetLgaIfExist(_viewModel);
                    SetDateOfBirth(_viewModel);
                    SetTeacherFirstSittingOLevelResult(firstSittingOLevelResult, firstSittingOLevelResultDetails);
                    SetTeacherSecondSittingOLevelResult(secondSittingOLevelResult, secondSittingOLevelResultDetails);
                    SetSelectedSittingSubjectAndGrade(_viewModel);
                    SetSelectedTeacherEducationalQualification(_viewModel);
                    SetSelectedTeacherEmploymentHistory(_viewModel);
                    SetSelectedTeacherAward(_viewModel);
                }

                SetPassportUrl(_viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            SaveUiState(_viewModel);
            return View(_viewModel);
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
        
        private void SetPassportUrl(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (TempData["imageUrl"] != null)
                {
                    viewModel.Teacher.ImageFileUrl = TempData["imageUrl"].ToString();
                    TempData["imageUrl"] = viewModel.Teacher.ImageFileUrl;
                }
                else
                {
                    viewModel.Teacher.ImageFileUrl = FileManager.DEFAULT_AVATAR;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Form(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                SetPassportUrl(viewModel);
                UpdateDropdownSelectList(viewModel);

                ModelState.Remove("Teacher.Person.Type.Id");
                ModelState.Remove("Teacher.Person.Country.Id");
                ModelState.Remove("Teacher.VerificationStatus.Id");
                ModelState.Remove("Teacher.AvailabilityStatus.Id");
                ModelState.Remove("Teacher.Person.IsBlacklisted");
                ModelState.Remove("Teacher.Rating.Id");

                if (ModelState.IsValid)
                {
                    if (IncompleteUserInput(viewModel))
                    {
                        SaveUiState(viewModel);
                        return View(viewModel);
                    }

                    SaveUiState(viewModel);
                    return RedirectToAction("FormPreview", "Subscription");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            SaveUiState(viewModel);
            return View(viewModel);
        }

        private void SaveUiState(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                PopulateAllDropDowns(viewModel);
                TempData["imageUrl"] = viewModel.Teacher.ImageFileUrl;
                TempData["TeacherSubscriptionViewModel"] = viewModel;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private bool IncompleteUserInput(TeacherSubscriptionViewModel viewModel)
        {
            const string FIRST_SITTING = "FIRST SITTING";
            const string SECOND_SITTING = "SECOND SITTING";

            try
            {
                TeacherSubscriptionViewModel savedViewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];

                if (InvalidDateOfBirth(viewModel))
                {
                    return true;
                }
                else if (NoPassportUploaded(viewModel))
                {
                    return true;
                }
                else if (savedViewModel == null || savedViewModel.OLevelGrades == null || savedViewModel.OLevelSubjects == null)
                {
                    SetMessage("View state cannot be null! ", ApplicationMessage.Category.Error);
                    return true;
                }
                else if (InvalidNumberOfOlevelSubject(viewModel.FirstSittingOLevelResultDetails, viewModel.SecondSittingOLevelResultDetails))
                {
                    return true;
                }
                else if (InvalidOlevelSubjectOrGrade(viewModel.FirstSittingOLevelResultDetails, savedViewModel.OLevelSubjects, savedViewModel.OLevelGrades, FIRST_SITTING))
                {
                    return true;
                }
                else if (viewModel.SecondSittingOLevelResult != null)
                {
                    if (viewModel.SecondSittingOLevelResult.ExamNumber != null && viewModel.SecondSittingOLevelResult.Type != null && viewModel.SecondSittingOLevelResult.Type.Id > 0 && viewModel.SecondSittingOLevelResult.ExamYear > 0)
                    {
                        if (InvalidOlevelSubjectOrGrade(viewModel.SecondSittingOLevelResultDetails, savedViewModel.OLevelSubjects, savedViewModel.OLevelGrades, SECOND_SITTING))
                        {
                            return true;
                        }
                    }
                }
                else if (InvalidOlevelResultHeaderInformation(viewModel.FirstSittingOLevelResultDetails, viewModel.FirstSittingOLevelResult, FIRST_SITTING))
                {
                    return true;
                }
                else if (InvalidOlevelResultHeaderInformation(viewModel.SecondSittingOLevelResultDetails, viewModel.SecondSittingOLevelResult, SECOND_SITTING))
                {
                    return true;
                }
                else if (NoOlevelSubjectSpecified(viewModel.FirstSittingOLevelResultDetails, viewModel.FirstSittingOLevelResult, FIRST_SITTING))
                {
                    return true;
                }
                else if (NoOlevelSubjectSpecified(viewModel.SecondSittingOLevelResultDetails, viewModel.SecondSittingOLevelResult, SECOND_SITTING))
                {
                    return true;
                }


                if (viewModel.TeacherEducationalQualifications == null || viewModel.TeacherEducationalQualifications.Count <= 0)
                {
                    SetMessage("No Educational Qualification entered! You must enter your educational qualifications", ApplicationMessage.Category.Error);
                    return true;
                }
                else
                {
                    List<TeacherEducationalQualification> educationalQualifications = viewModel.TeacherEducationalQualifications.Where(x => x.School != null && x.Qualification != null && x.Qualification.Id > 0 && x.YearOfAdmission > 0 && x.YearOfGraduation > 0).ToList();
                    if (educationalQualifications == null || educationalQualifications.Count <= 0)
                    {
                        SetMessage("No Educational Qualification entered! You must enter your educational qualifications", ApplicationMessage.Category.Error);
                        return true;
                    }
                    else
                    {
                        TeacherEducationalQualification primarySchool = educationalQualifications.Where(x => x.SchoolType.Id == 1).SingleOrDefault();
                        TeacherEducationalQualification secondarySchool = educationalQualifications.Where(x => x.SchoolType.Id == 2).SingleOrDefault();
                        TeacherEducationalQualification tertiaryInstitution = educationalQualifications.Where(x => x.SchoolType.Id == 3).SingleOrDefault();
                        if (primarySchool == null)
                        {
                            SetMessage("Please enter your primary school information at Educational Qualification section!", ApplicationMessage.Category.Error);
                            return true;
                        }
                        if (secondarySchool == null)
                        {
                            SetMessage("Please enter your secondary school information at Educational Qualification section!", ApplicationMessage.Category.Error);
                            return true;
                        }
                        if (tertiaryInstitution == null)
                        {
                            SetMessage("Please enter your tertiary institution information at Educational Qualification section!", ApplicationMessage.Category.Error);
                            return true;
                        }
                        
                        for (int i = 0; i < viewModel.TeacherEducationalQualifications.Count; i++)
                        {
                            bool noEntry = string.IsNullOrWhiteSpace(viewModel.TeacherEducationalQualifications[i].School) && viewModel.TeacherEducationalQualifications[i].Qualification.Id <= 0 && viewModel.TeacherEducationalQualifications[i].YearOfAdmission <= 0 && viewModel.TeacherEducationalQualifications[i].YearOfGraduation <= 0;
                            bool filled = !string.IsNullOrWhiteSpace(viewModel.TeacherEducationalQualifications[i].School) && viewModel.TeacherEducationalQualifications[i].Qualification.Id > 0 && viewModel.TeacherEducationalQualifications[i].YearOfAdmission > 0 && viewModel.TeacherEducationalQualifications[i].YearOfGraduation > 0;

                            if (noEntry == false && filled == false)
                            {
                                SetMessage("Incomplete entry found on row " + (i + 1) + " of Educational Qualification section! Please modify", ApplicationMessage.Category.Error);
                                return true;
                            }

                            if (viewModel.TeacherEducationalQualifications[i].YearOfAdmission > viewModel.TeacherEducationalQualifications[i].YearOfGraduation)
                            {
                                SetMessage("Admission Year greater than Graduation Year on row " + (i + 1) + " of Educational qualification Section! Please modify", ApplicationMessage.Category.Error);
                                return true;
                            }
                        }
                        
                        foreach (TeacherEducationalQualification educationalQualification in educationalQualifications)
                        {
                            if (educationalQualification.YearOfAdmission >= educationalQualification.YearOfGraduation)
                            {
                                SetMessage("Year of admission cannot be equal to or greater than year of graduation at Educational Qualification section! Please modify", ApplicationMessage.Category.Error);
                                return true;
                            }
                        }

                    }
                }
                
                if (viewModel.TeacherEmploymentHistories != null && viewModel.TeacherEmploymentHistories.Count > 0)
                {
                    for (int i = 0; i < viewModel.TeacherEmploymentHistories.Count; i++)
                    {
                        bool noEntry = string.IsNullOrWhiteSpace(viewModel.TeacherEmploymentHistories[i].Employer) && string.IsNullOrWhiteSpace(viewModel.TeacherEmploymentHistories[i].LastPositionHeld) && viewModel.TeacherEmploymentHistories[i].StartYear <= 0 && viewModel.TeacherEmploymentHistories[i].EndYear <= 0;
                        bool filled = !string.IsNullOrWhiteSpace(viewModel.TeacherEmploymentHistories[i].Employer) && !string.IsNullOrWhiteSpace(viewModel.TeacherEmploymentHistories[i].LastPositionHeld) && viewModel.TeacherEmploymentHistories[i].StartYear > 0 && viewModel.TeacherEmploymentHistories[i].EndYear > 0;

                        if (noEntry == false && filled == false)
                        {
                            SetMessage("Incomplete entry found on row " + (i + 1) + " of Employment History section! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }

                        if (viewModel.TeacherEmploymentHistories[i].StartYear > viewModel.TeacherEmploymentHistories[i].EndYear)
                        {
                            SetMessage("Start Year greater than End Year on row " + (i + 1) + " of Employment History section! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }
                    } 
                }

                if (viewModel.TeacherStudentCategories != null && viewModel.TeacherStudentCategories.Count > 0)
                {
                    List<TeacherStudentCategory> studentCategories = viewModel.TeacherStudentCategories.Where(x => x.IsSelected == true).ToList();
                    if (studentCategories == null || studentCategories.Count <= 0)
                    {
                        SetMessage("Please select at least one Student category at Student Category section!", ApplicationMessage.Category.Error);
                        return true;
                    }
                }

                if (viewModel.TeacherAvailabilities != null && viewModel.TeacherAvailabilities.Count > 0)
                {
                    List<TeacherAvailability> teacherAvailabilities = viewModel.TeacherAvailabilities.Where(x => x.IsAvailable == true).ToList();
                    if (teacherAvailabilities == null || teacherAvailabilities.Count <= 0)
                    {
                        SetMessage("Time to be available not set! Please modify", ApplicationMessage.Category.Error);
                        return true;
                    }
                }

                if (viewModel.TeacherAwards != null && viewModel.TeacherAwards.Count > 0)
                {
                    for (int i = 0; i < viewModel.TeacherAwards.Count; i++)
                    {
                        bool noEntry = string.IsNullOrWhiteSpace(viewModel.TeacherAwards[i].AwardBody) && string.IsNullOrWhiteSpace(viewModel.TeacherAwards[i].AwardName) && viewModel.TeacherAwards[i].YearAwarded <= 0;
                        bool filled = !string.IsNullOrWhiteSpace(viewModel.TeacherAwards[i].AwardBody) && !string.IsNullOrWhiteSpace(viewModel.TeacherAwards[i].AwardName) && viewModel.TeacherAwards[i].YearAwarded > 0;

                        if (noEntry == false && filled == false)
                        {
                            SetMessage("Incomplete entry found on row " + (i + 1) + " of Awards! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }
                    }
                }
                
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private bool NoPassportUploaded(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.Teacher.ImageFileUrl) || viewModel.Teacher.ImageFileUrl == FileManager.DEFAULT_AVATAR)
                {
                    SetMessage("No Passport uploaded! Please upload passport to continue.", ApplicationMessage.Category.Error);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult FormPreview()
        {
            TeacherSubscriptionViewModel viewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];

            try
            {
                if (viewModel != null)
                {
                    if (viewModel.Teacher != null && viewModel.Teacher.Person != null)
                    {
                        viewModel.Teacher.DateOfBirth = new DateTime(viewModel.Teacher.YearOfBirth.Id, viewModel.Teacher.MonthOfBirth.Id, viewModel.Teacher.DayOfBirth.Id);
                        viewModel.Teacher.Sex = viewModel.Sexs.Where(x => x.Id == viewModel.Teacher.Sex.Id).SingleOrDefault();
                        viewModel.Teacher.Person.State = viewModel.States.Where(x => x.Id == viewModel.Teacher.Person.State.Id).SingleOrDefault();
                        viewModel.Teacher.Person.Lga = _da.GetModelBy<Lga, LGA>(x => x.Lga_Id == viewModel.Teacher.Person.Lga.Id);

                        if (viewModel.Teacher.LoginDetail != null && viewModel.Teacher.LoginDetail.SecurityAnswer != null)
                        {
                            viewModel.Teacher.LoginDetail.SecurityQuestion = viewModel.SecurityQuestions.Where(x => x.Id == viewModel.Teacher.LoginDetail.SecurityQuestion.Id).SingleOrDefault();
                        }
                    }

                    if (viewModel.Teacher != null && viewModel.Teacher.Type != null)
                    {
                        viewModel.Teacher.Type = viewModel.TeacherTypes.Where(x => x.Id == viewModel.Teacher.Type.Id).SingleOrDefault();
                    }

                    viewModel.FirstSittingOLevelResult.Type = viewModel.OLevelTypes.Where(m => m.Id == viewModel.FirstSittingOLevelResult.Type.Id).SingleOrDefault();
                    if (viewModel.SecondSittingOLevelResult != null && viewModel.SecondSittingOLevelResult.Type != null)
                    {
                        viewModel.SecondSittingOLevelResult.Type = viewModel.OLevelTypes.Where(m => m.Id == viewModel.SecondSittingOLevelResult.Type.Id).SingleOrDefault();
                    }

                    if (viewModel.TeacherEducationalQualifications != null && viewModel.TeacherEducationalQualifications.Count > 0)
                    {
                        foreach (TeacherEducationalQualification educationalQualification in viewModel.TeacherEducationalQualifications)
                        {
                            educationalQualification.SchoolType = viewModel.SchoolTypes.Where(x => x.Id == educationalQualification.SchoolType.Id).SingleOrDefault();

                            if (educationalQualification.Qualification != null && educationalQualification.Qualification.Id > 0)
                            {
                                educationalQualification.Qualification = viewModel.Qualifications.Where(x => x.Id == educationalQualification.Qualification.Id).SingleOrDefault();
                            }
                            else
                            {
                                educationalQualification.Qualification = new Qualification() { Id = 0 };
                            }
                        }
                    }

                    if (viewModel.TeacherEmploymentHistories != null && viewModel.TeacherEmploymentHistories.Count > 0)
                    {
                        List<TeacherEmploymentHistory> employmentHistories = viewModel.TeacherEmploymentHistories.Where(x => x.Employer != null && x.LastPositionHeld != null && x.StartYear > 0 && x.EndYear > 0).ToList();
                        viewModel.EmploymentHistoryExist = employmentHistories != null && employmentHistories.Count > 0 ? true : false;
                    }
                                      
                    if (viewModel.TeacherAwards != null && viewModel.TeacherAwards.Count > 0)
                    {
                        List<TeacherAward> teacherAwards = viewModel.TeacherAwards.Where(x => x.AwardBody != null && x.AwardName != null).ToList();
                        viewModel.AwardExist = teacherAwards != null && teacherAwards.Count > 0 ? true : false;
                    }

                    UpdateOLevelResultDetail(viewModel);
                }
                else
                {

                }
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
        public ActionResult FormPreview(TeacherSubscriptionViewModel vm)
        {
            JsonResult json = null;
            TeacherSubscriptionViewModel viewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];

            try
            {
                Model.Model.Teacher teacher = new Model.Model.Teacher();
                teacher.Person = viewModel.Teacher.Person;
                teacher.Person.DateEntered = DateTime.Now;

                teacher.Referee = viewModel.Referee;
                teacher.Referee.Person = viewModel.Teacher.Person;
                teacher.DateOfBirth = viewModel.Teacher.DateOfBirth;
                teacher.ImageFileUrl = viewModel.Teacher.ImageFileUrl;
                teacher.HomeAddress = viewModel.Teacher.HomeAddress;
                teacher.HomeTown = viewModel.Teacher.HomeTown;
                teacher.Type = viewModel.Teacher.Type;
                teacher.Sex = viewModel.Teacher.Sex;

                if (viewModel.TeacherEducationalQualifications != null && viewModel.TeacherEducationalQualifications.Count > 0)
                {
                    List<TeacherEducationalQualification> educationalQualifications = viewModel.TeacherEducationalQualifications.Where(x => x.School != null && x.Qualification != null && x.YearOfAdmission > 0 && x.YearOfGraduation > 0).ToList();
                    if (educationalQualifications != null && educationalQualifications.Count > 0)
                    {
                        foreach (TeacherEducationalQualification educationalQualification in educationalQualifications)
                        {
                            educationalQualification.Person = viewModel.Teacher.Person;
                        }

                        teacher.EducationalQualifications = educationalQualifications;
                    }
                }

                if (viewModel.TeacherEmploymentHistories != null && viewModel.TeacherEmploymentHistories.Count > 0)
                {
                    List<TeacherEmploymentHistory> employmentHistories = viewModel.TeacherEmploymentHistories.Where(x => x.Employer != null && x.LastPositionHeld != null && x.StartYear > 0 && x.EndYear > 0).ToList();
                    if (employmentHistories != null && employmentHistories.Count > 0)
                    {
                        foreach (TeacherEmploymentHistory employmentHistory in employmentHistories)
                        {
                            employmentHistory.Person = viewModel.Teacher.Person;
                        }

                        teacher.EmploymentHistories = employmentHistories;
                    }
                }

                if (viewModel.TeacherStudentCategories != null && viewModel.TeacherStudentCategories.Count > 0)
                {
                    List<TeacherStudentCategory> studentCategories = viewModel.TeacherStudentCategories.Where(x => x.IsSelected == true).ToList();
                    if (studentCategories != null && studentCategories.Count > 0)
                    {
                        foreach (TeacherStudentCategory studentCategory in studentCategories)
                        {
                            studentCategory.Person = viewModel.Teacher.Person;
                        }

                        teacher.StudentCategories = studentCategories;
                    }
                }

                if (viewModel.TeacherAvailabilities != null && viewModel.TeacherAvailabilities.Count > 0)
                {
                    List<TeacherAvailability> teacherAvailabilities = viewModel.TeacherAvailabilities.Where(x => x.IsAvailable == true).ToList();
                    if (teacherAvailabilities != null && teacherAvailabilities.Count > 0)
                    {
                        foreach (TeacherAvailability teacherAvailability in teacherAvailabilities)
                        {
                            teacherAvailability.Person = viewModel.Teacher.Person;
                        }

                        teacher.TeacherAvailabilities = teacherAvailabilities;
                    }
                }

                if (viewModel.TeacherAwards != null && viewModel.TeacherAwards.Count > 0)
                {
                    List<TeacherAward> teacherAwards = viewModel.TeacherAwards.Where(x => x.AwardBody != null && x.AwardName != null).ToList();
                    if (teacherAwards != null && teacherAwards.Count > 0)
                    {
                        foreach (TeacherAward teacherAward in teacherAwards)
                        {
                            teacherAward.Person = viewModel.Teacher.Person;
                        }

                        teacher.TeacherAwards = teacherAwards;
                    }
                }

                teacher.OLevelResults = new List<TeacherOLevelResult>();
                OLevelExamSitting firstSitting = new OLevelExamSitting() { Id = 1 };
                OLevelExamSitting secondSitting = new OLevelExamSitting() { Id = 2 };

                if (viewModel.FirstSittingOLevelResultDetails != null && viewModel.FirstSittingOLevelResultDetails.Count > 0)
                {
                    List<TeacherOLevelResultDetail> firstSittingOLevelResultDetails = viewModel.FirstSittingOLevelResultDetails.Where(x => x.Subject != null && x.Subject.Id > 0 && x.Grade != null && x.Grade.Id > 0).ToList();
                    if (firstSittingOLevelResultDetails != null && firstSittingOLevelResultDetails.Count > 0)
                    {
                        viewModel.FirstSittingOLevelResult.Sitting = new OLevelExamSitting();
                        viewModel.FirstSittingOLevelResult.OLevelResultDetails = firstSittingOLevelResultDetails;
                        viewModel.FirstSittingOLevelResult.Sitting = firstSitting;
                        teacher.OLevelResults.Add(viewModel.FirstSittingOLevelResult);
                    }
                }
                if (viewModel.SecondSittingOLevelResultDetails != null && viewModel.SecondSittingOLevelResultDetails.Count > 0)
                {
                    List<TeacherOLevelResultDetail> secondSittingOLevelResultDetails = viewModel.SecondSittingOLevelResultDetails.Where(x => x.Subject != null && x.Subject.Id > 0 && x.Grade != null && x.Grade.Id > 0).ToList();
                    if (secondSittingOLevelResultDetails != null && secondSittingOLevelResultDetails.Count > 0)
                    {
                        viewModel.SecondSittingOLevelResult.Sitting = new OLevelExamSitting();
                        viewModel.SecondSittingOLevelResult.OLevelResultDetails = secondSittingOLevelResultDetails;
                        viewModel.SecondSittingOLevelResult.Sitting = secondSitting;
                        teacher.OLevelResults.Add(viewModel.SecondSittingOLevelResult);
                    }
                }

                if (teacher.OLevelResults != null && teacher.OLevelResults.Count > 0)
                {
                    foreach (TeacherOLevelResult oLevelResult in teacher.OLevelResults)
                    {
                        oLevelResult.Person = viewModel.Teacher.Person;
                    }
                }

                ISubscription subscriptionService = null;
                if (viewModel.PersonAlreadyExist)
                {
                    teacher.Rating = viewModel.Teacher.Rating;
                    teacher.VerificationStatus = viewModel.Teacher.VerificationStatus;
                    teacher.AvailabilityStatus = viewModel.Teacher.AvailabilityStatus;

                    subscriptionService = _subscriptionFactory.Create(Subscription.EditTeacher);
                }
                else
                {
                    teacher.Person.IsBlacklisted = false;
                    teacher.Rating = new Rating() { Id = 1 }; //temp
                    teacher.Person.Type = new PersonType() { Id = 2 }; //temp
                    teacher.Person.Country = new Country() { Id = 1 }; //temp
                    teacher.VerificationStatus = new TeacherVerificationStatus() { Id = 3 }; //temp
                    teacher.AvailabilityStatus = new TeacherAvailabilityStatus() { Id = 1 }; //temp

                    teacher.LoginDetail = viewModel.Teacher.LoginDetail;
                    teacher.LoginDetail.Person = viewModel.Teacher.Person;
                    teacher.LoginDetail.Role = new Role() { Id = 1 };
                    teacher.LoginDetail.IsFirstLogon = false;
                    teacher.LoginDetail.IsActivated = true;
                    teacher.LoginDetail.IsLocked = false;

                    Payment payment = new Payment();
                    payment.Mode = new PaymentMode() { Id = 1 };
                    payment.DateEntered = DateTime.Now;
                    payment.Person = teacher.Person;
                    payment.Paid = false;

                    List<Payment> payments = new List<Payment>();
                    payments.Add(payment);

                    teacher.Payments = payments;
                    subscriptionService = _subscriptionFactory.Create(Subscription.SubmitTeacher);
                }

                Model.Model.Teacher newTeacher = subscriptionService.Save(teacher);
                if (newTeacher != null && newTeacher.Person != null && newTeacher.Person.Id > 0)
                {
                    viewModel.Teacher.Person.Id = newTeacher.Person.Id;
                    viewModel.Teacher.PaymentSlip = newTeacher.PaymentSlip;
                    viewModel.Teacher.ImageFileUrl = newTeacher.ImageFileUrl;

                    SaveUiState(viewModel);
                    TempData["PaymentSlip"] = viewModel.Teacher.PaymentSlip;

                    //TempData["Person"] = viewModel.Teacher.Person;
                    //TempData["ServiceCharge"] = ServiceCharge.Services.TeacherSubscription;

                    //return RedirectToAction("AcknowledgementSlip", "Subscription");
                    json = Json(new { isDone = true, message = "Subscription has been saved successfully " }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    json = Json(new { isDone = false, message = "Subscription save operation failed! Please try again" }, "text/html", JsonRequestBehavior.AllowGet);
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

        public ActionResult AcknowledgementSlip()
        {
            TeacherSubscriptionViewModel viewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];
            SaveUiState(viewModel);
            return View(viewModel);
        }

        private void InitializeDropDowns()
        {
            try
            {
                _viewModel.Sexs = _da.GetAll<Sex, SEX>();
                _viewModel.States = _da.GetAll<State, STATE>();
                _viewModel.Qualifications = _da.GetAll<Qualification, QUALIFICATION>();
                _viewModel.TeacherTypes = _da.GetAll<TeacherType, TEACHER_TYPE>();
                _viewModel.SchoolTypes = _da.GetAll<SchoolType, SCHOOL_TYPE>();
                _viewModel.SecurityQuestions = _da.GetAll<SecurityQuestion, SECURITY_QUESTION>();

                _viewModel.SexSelectList = DropdownUtility.PopulateModelSelectList<Sex, SEX>(_da, false);
                _viewModel.StateSelectList = DropdownUtility.PopulateModelSelectList<State, STATE>(_da, false);
                _viewModel.CountrySelectList = DropdownUtility.PopulateModelSelectList<Country, COUNTRY>(_da, false);
                _viewModel.YearOfBirthSelectList = DropdownUtility.PopulateYearSelectListItem(START_YEAR, false, false);
                _viewModel.MonthOfBirthSelectList = DropdownUtility.PopulateMonthSelectListItem();
                _viewModel.QualificationSelectList = DropdownUtility.PopulateModelSelectList<Qualification, QUALIFICATION>(_da, true);
                _viewModel.TeacherTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.TeacherTypes, false);
                _viewModel.SecurityQuestionSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.SecurityQuestions, "Id", "Question");

                _viewModel.FirstSittingExamYearSelectList = DropdownUtility.PopulateYearSelectListItem(START_YEAR, true, false);
                _viewModel.FirstSittingOLevelTypeSelectList = DropdownUtility.PopulateModelSelectList<OLevelType, O_LEVEL_TYPE>(_da, false);
                _viewModel.SecondSittingOLevelTypeSelectList = DropdownUtility.PopulateModelSelectList<OLevelType, O_LEVEL_TYPE>(_da, true);
                _viewModel.SecondSittingExamYearSelectList = DropdownUtility.PopulateYearSelectListItem(START_YEAR, true, true);

                _viewModel.SchoolTypeSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.SchoolTypes, false);
                _viewModel.YearSelectList = DropdownUtility.PopulateYearSelectListItem(START_YEAR, false, true);
                
                _viewModel.OLevelTypes = _da.GetAll<OLevelType, O_LEVEL_TYPE>();
                _viewModel.OLevelGrades = _da.GetAll<OLevelGrade, O_LEVEL_GRADE>();
                _viewModel.OLevelSubjects = _da.GetAll<OLevelSubject, O_LEVEL_SUBJECT>();

                if (_viewModel.OLevelSubjects != null && _viewModel.OLevelSubjects.Count > 0)
                {
                    _viewModel.OLevelSubjects = _viewModel.OLevelSubjects.OrderBy(s => s.Name).ToList();
                }

                _viewModel.OLevelGradeSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.OLevelGrades, true);
                _viewModel.OLevelSubjectSelectList = DropdownUtility.PopulateModelSelectListHelper(_viewModel.OLevelSubjects, true);
                
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
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateDropdownSelectList(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                TeacherSubscriptionViewModel storedViewModel = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];
                if (storedViewModel != null)
                {
                    viewModel.SexSelectList = storedViewModel.SexSelectList;
                    viewModel.StateSelectList = storedViewModel.StateSelectList;
                    viewModel.CountrySelectList = storedViewModel.CountrySelectList;
                    viewModel.YearOfBirthSelectList = storedViewModel.YearOfBirthSelectList;
                    viewModel.MonthOfBirthSelectList = storedViewModel.MonthOfBirthSelectList;
                    viewModel.QualificationSelectList = storedViewModel.QualificationSelectList;
                    viewModel.FirstSittingOLevelTypeSelectList = storedViewModel.FirstSittingOLevelTypeSelectList;
                    viewModel.FirstSittingExamYearSelectList = storedViewModel.FirstSittingExamYearSelectList;
                    viewModel.SecondSittingOLevelTypeSelectList = storedViewModel.SecondSittingOLevelTypeSelectList;
                    viewModel.SecondSittingExamYearSelectList = storedViewModel.SecondSittingExamYearSelectList;
                    viewModel.OLevelGradeSelectList = storedViewModel.OLevelGradeSelectList;
                    viewModel.OLevelSubjectSelectList = storedViewModel.OLevelSubjectSelectList;
                    viewModel.SchoolTypeSelectList = storedViewModel.SchoolTypeSelectList;
                    viewModel.YearSelectList = storedViewModel.YearSelectList;
                    viewModel.TeacherTypeSelectList = storedViewModel.TeacherTypeSelectList;
                    viewModel.SecurityQuestionSelectList = storedViewModel.SecurityQuestionSelectList;

                    viewModel.OLevelTypes = storedViewModel.OLevelTypes;
                    viewModel.OLevelSubjects = storedViewModel.OLevelSubjects;
                    viewModel.OLevelGrades = storedViewModel.OLevelGrades;
                    viewModel.WeekDays = storedViewModel.WeekDays;
                    viewModel.Periods = storedViewModel.Periods;

                    viewModel.Sexs = storedViewModel.Sexs;
                    viewModel.States = storedViewModel.States;
                    viewModel.Qualifications = storedViewModel.Qualifications;
                    viewModel.TeacherTypes = storedViewModel.TeacherTypes;
                    viewModel.SchoolTypes = storedViewModel.SchoolTypes;
                    viewModel.StudentCategories = storedViewModel.StudentCategories;
                    viewModel.SecurityQuestions = storedViewModel.SecurityQuestions;
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex);
                throw;
            }
        }

        private void PopulateAllDropDowns(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    _viewModel = new TeacherSubscriptionViewModel();

                    InitializeDropDowns();
                    _viewModel.TeacherStudentCategories = UiUtility.InitializeStudentCategory(_viewModel.StudentCategories);
                    _viewModel.TeacherAvailabilities = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);

                    ViewBag.Sexs = _viewModel.SexSelectList;
                    ViewBag.Lgas = new SelectList(new List<Lga>(), DropdownUtility.ID, DropdownUtility.NAME);
                    ViewBag.States = _viewModel.StateSelectList;
                    ViewBag.Countries = _viewModel.CountrySelectList;
                    ViewBag.DaysOfBirth = new SelectList(new List<Value>(), DropdownUtility.ID, DropdownUtility.NAME);
                    ViewBag.MonthsOfBirth = _viewModel.MonthOfBirthSelectList;
                    ViewBag.YearsOfBirth = _viewModel.YearOfBirthSelectList;
                    ViewBag.Qualifications = _viewModel.QualificationSelectList;
                    ViewBag.FirstSittingOLevelGrades = _viewModel.OLevelGradeSelectList;
                    ViewBag.FirstSittingOLevelSubjects = _viewModel.OLevelSubjectSelectList;
                    ViewBag.SecondSittingOLevelGrades = _viewModel.OLevelGradeSelectList;
                    ViewBag.SecondSittingOLevelSubjects = _viewModel.OLevelSubjectSelectList;
                    ViewBag.FirstSittingOLevelTypes = _viewModel.FirstSittingOLevelTypeSelectList;
                    ViewBag.SecondSittingOLevelTypes = _viewModel.SecondSittingOLevelTypeSelectList;
                    ViewBag.FirstSittingExamYears = _viewModel.FirstSittingExamYearSelectList;
                    ViewBag.SecondSittingExamYears = _viewModel.SecondSittingExamYearSelectList;
                    ViewBag.TeacherTypes = _viewModel.TeacherTypeSelectList;
                    ViewBag.SchoolTypes = _viewModel.SchoolTypeSelectList;
                    ViewBag.SecurityQuestions = _viewModel.SecurityQuestionSelectList;
                }
                else
                {
                    _viewModel = viewModel;

                    ViewBag.Sexs = new SelectList(_viewModel.SexSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Teacher.Sex.Id);
                    ViewBag.States = new SelectList(_viewModel.StateSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Teacher.Person.State.Id);
                    ViewBag.Countries = new SelectList(_viewModel.CountrySelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Teacher.Person.Country.Id);
                    ViewBag.Qualifications = new SelectList(_viewModel.QualificationSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.TeacherEducationalQualification.Qualification.Id);
                    ViewBag.TeacherTypes = new SelectList(_viewModel.TeacherTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Teacher.Type.Id);

                    if (_viewModel.Teacher != null && _viewModel.Teacher.LoginDetail != null && _viewModel.Teacher.LoginDetail.SecurityQuestion != null)
                    {
                        ViewBag.SecurityQuestions = new SelectList(_viewModel.SecurityQuestionSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Teacher.LoginDetail.SecurityQuestion.Id);
                    }
                    else
                    {
                        ViewBag.SecurityQuestions = new SelectList(_viewModel.SecurityQuestionSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                    }

                    if (_viewModel.FirstSittingOLevelResult != null)
                    {
                        ViewBag.FirstSittingExamYears = new SelectList(_viewModel.FirstSittingExamYearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.FirstSittingOLevelResult.ExamYear);
                        if (_viewModel.SecondSittingOLevelResult.Type != null)
                        {
                            ViewBag.FirstSittingOLevelTypes = new SelectList(_viewModel.FirstSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.FirstSittingOLevelResult.Type.Id);
                        }
                        else
                        {
                            ViewBag.FirstSittingOLevelTypes = new SelectList(_viewModel.FirstSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }
                    }
                    else
                    {
                        ViewBag.FirstSittingExamYears = new SelectList(_viewModel.FirstSittingExamYearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        ViewBag.FirstSittingOLevelTypes = new SelectList(_viewModel.FirstSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                    }


                    if (_viewModel.SecondSittingOLevelResult != null)
                    {
                        ViewBag.SecondSittingExamYears = new SelectList(_viewModel.SecondSittingExamYearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.SecondSittingOLevelResult.ExamYear);
                        if (_viewModel.SecondSittingOLevelResult.Type != null)
                        {
                            ViewBag.SecondSittingOLevelTypes = new SelectList(_viewModel.SecondSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.SecondSittingOLevelResult.Type.Id);
                        }
                        else
                        {
                            ViewBag.SecondSittingOLevelTypes = new SelectList(_viewModel.SecondSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }
                    }
                    else
                    {
                        ViewBag.SecondSittingExamYears = new SelectList(_viewModel.SecondSittingExamYearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        ViewBag.SecondSittingOLevelTypes = new SelectList(_viewModel.SecondSittingOLevelTypeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                    }

                    if (_viewModel.TeacherAvailabilities == null || _viewModel.TeacherAvailabilities.Count <= 0)
                    {
                        //InitializeTeacherAvailability(_viewModel);
                        _viewModel.TeacherAvailabilities = UiUtility.InitializeAvailability(_viewModel.WeekDays, _viewModel.Periods);
                    }

                    if (_viewModel.TeacherStudentCategories == null || _viewModel.TeacherStudentCategories.Count <= 0)
                    {
                        //InitializeStudentCategory(_viewModel);
                        viewModel.TeacherStudentCategories = UiUtility.InitializeStudentCategory(_viewModel.StudentCategories);
                    }
                    
                    SetLgaIfExist(_viewModel);
                    SetDateOfBirthDropDown(_viewModel);
                }
                                
                SetSelectedSittingSubjectAndGrade(_viewModel);
                SetSelectedTeacherEducationalQualification(_viewModel);
                SetSelectedTeacherEmploymentHistory(_viewModel);
                SetSelectedTeacherAward(_viewModel);
            }
            catch (Exception)
            {
                //_logger.LogError(ex);
                throw;
            }
        }
     
        private void SetLgaIfExist(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Teacher.Person.State != null && !string.IsNullOrEmpty(viewModel.Teacher.Person.State.Id))
                {
                    List<Lga> lgas = _da.GetModelsBy<Lga, LGA>(l => l.State_Id == viewModel.Teacher.Person.State.Id);
                    if (viewModel.Teacher.Person.Lga != null && viewModel.Teacher.Person.Lga.Id > 0)
                    {
                        ViewBag.Lgas = new SelectList(lgas, DropdownUtility.ID, DropdownUtility.NAME, viewModel.Teacher.Person.Lga.Id);
                    }
                    else
                    {
                        ViewBag.Lgas = new SelectList(lgas, DropdownUtility.ID, DropdownUtility.NAME);
                    }
                }
                else
                {
                    ViewBag.Lgas = new SelectList(new List<Lga>(), DropdownUtility.ID, DropdownUtility.NAME);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetDateOfBirthDropDown(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                ViewBag.MonthsOfBirth = new SelectList(viewModel.MonthOfBirthSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, viewModel.Teacher.MonthOfBirth.Id);
                ViewBag.YearsOfBirth = new SelectList(viewModel.YearOfBirthSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, viewModel.Teacher.YearOfBirth.Id);

                if ((viewModel.DayOfBirthSelectList == null || viewModel.DayOfBirthSelectList.Count == 0) && (viewModel.Teacher.MonthOfBirth.Id > 0 && viewModel.Teacher.YearOfBirth.Id > 0))
                {
                    viewModel.DayOfBirthSelectList = DropdownUtility.PopulateDaySelectListItem(viewModel.Teacher.MonthOfBirth, viewModel.Teacher.YearOfBirth);
                }
                else
                {
                    if (viewModel.DayOfBirthSelectList != null && viewModel.Teacher.DayOfBirth.Id > 0)
                    {
                        ViewBag.DaysOfBirth = new SelectList(viewModel.DayOfBirthSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, viewModel.Teacher.DayOfBirth.Id);
                    }
                    else if (viewModel.DayOfBirthSelectList != null && viewModel.Teacher.DayOfBirth.Id <= 0)
                    {
                        ViewBag.DaysOfBirth = viewModel.DayOfBirthSelectList;
                    }
                    else if (viewModel.DayOfBirthSelectList == null)
                    {
                        viewModel.DayOfBirthSelectList = new List<SelectListItem>();
                        ViewBag.DaysOfBirth = new List<SelectListItem>();
                    }
                }

                if (viewModel.Teacher.DayOfBirth != null && viewModel.Teacher.DayOfBirth.Id > 0)
                {
                    ViewBag.DaysOfBirth = new SelectList(viewModel.DayOfBirthSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, viewModel.Teacher.DayOfBirth.Id);
                }
                else
                {
                    ViewBag.DaysOfBirth = viewModel.DayOfBirthSelectList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetDateOfBirth(TeacherSubscriptionViewModel viewModel)
        {
            const int START_VALUE = 1;

            try
            {
                //if (viewModel.Teacher.DateOfBirth.HasValue)
                //{
                    int noOfDays = DateTime.DaysInMonth(viewModel.Teacher.YearOfBirth.Id, viewModel.Teacher.MonthOfBirth.Id);
                    List<Value> days = UiUtility.CreateNumberListFrom(START_VALUE, noOfDays);
                    if (days != null && days.Count > 0)
                    {
                        days.Insert(0, new Value() { Name = "--DD--" });
                    }

                    if (viewModel.Teacher.DayOfBirth != null && viewModel.Teacher.DayOfBirth.Id > 0)
                    {
                        ViewBag.DaysOfBirth = new SelectList(days, DropdownUtility.ID, DropdownUtility.NAME, viewModel.Teacher.DayOfBirth.Id);
                    }
                    else
                    {
                        ViewBag.DaysOfBirth = new SelectList(days, DropdownUtility.ID, DropdownUtility.NAME);
                    }
                //}
                //else
                //{
                //    ViewBag.DaysOfBirth = new SelectList(new List<Value>(), DropdownUtility.ID, DropdownUtility.NAME);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetSelectedTeacherEducationalQualification(TeacherSubscriptionViewModel vm)
        {
            try
            {
                if (vm != null && vm.TeacherEducationalQualifications != null && vm.TeacherEducationalQualifications.Count > 0)
                {
                    int i = 0;
                    List<SchoolType> schoolTypes = _da.GetAll<SchoolType, SCHOOL_TYPE>();
                    if (schoolTypes == null || schoolTypes.Count <= 0)
                    {
                        throw new Exception("No school type found! Please populate and try again.");
                    }

                    foreach (TeacherEducationalQualification educationalQualification in vm.TeacherEducationalQualifications)
                    {
                        List<Qualification> qualifications = vm.Qualifications.Where(x => x.SchoolType.Id == educationalQualification.SchoolType.Id).ToList();
                        vm.QualificationSelectList = DropdownUtility.PopulateModelSelectListHelper(qualifications, true);

                        if (educationalQualification.SchoolType != null && educationalQualification.Qualification != null)
                        {
                            educationalQualification.SchoolType = schoolTypes.Where(x => x.Id == educationalQualification.SchoolType.Id).SingleOrDefault();

                            ViewData["YearOfAdmissions" + i] = new SelectList(vm.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, educationalQualification.YearOfAdmission);
                            ViewData["YearOfGraduations" + i] = new SelectList(vm.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, educationalQualification.YearOfGraduation);
                            ViewData["Qualifications" + i] = new SelectList(vm.QualificationSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, educationalQualification.Qualification.Id);
                        }
                        else
                        {
                            educationalQualification.SchoolType = new SchoolType();
                            ViewData["YearOfAdmissions" + i] = new SelectList(vm.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                            ViewData["YearOfGraduations" + i] = new SelectList(vm.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                            ViewData["Qualifications" + i] = new SelectList(vm.QualificationSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetSelectedTeacherEmploymentHistory(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.TeacherEmploymentHistories != null && viewModel.TeacherEmploymentHistories.Count > 0)
                {
                    int i = 0;

                    //List<SchoolType> schoolTypes = _da.GetAll<SchoolType, SCHOOL_TYPE>();
                    //if (schoolTypes == null || schoolTypes.Count <= 0)
                    //{
                    //    throw new Exception("No school type found! Please populate and try again.");
                    //}

                    foreach (TeacherEmploymentHistory employmentHistory in viewModel.TeacherEmploymentHistories)
                    {
                        if (employmentHistory.StartYear > 0 )
                        {
                            ViewData["StartYears" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employmentHistory.StartYear);
                        }
                        else
                        {
                            ViewData["StartYears" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        if (employmentHistory.EndYear > 0)
                        {
                            ViewData["EndYears" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, employmentHistory.EndYear);
                        }
                        else
                        {
                            ViewData["EndYears" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetSelectedTeacherAward(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.TeacherAwards != null && viewModel.TeacherAwards.Count > 0)
                {
                    int i = 0;

                    foreach (TeacherAward teacherAward in viewModel.TeacherAwards)
                    {
                        if (teacherAward.YearAwarded > 0)
                        {
                            ViewData["YearsAwarded" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, teacherAward.YearAwarded);
                        }
                        else
                        {
                            ViewData["YearsAwarded" + i] = new SelectList(viewModel.YearSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }
                        
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void SetSelectedSittingSubjectAndGrade(TeacherSubscriptionViewModel existingViewModel)
        {
            try
            {
                if (existingViewModel != null && existingViewModel.FirstSittingOLevelResultDetails != null && existingViewModel.FirstSittingOLevelResultDetails.Count > 0)
                {
                    int i = 0;
                    foreach (TeacherOLevelResultDetail firstSittingOLevelResultDetail in existingViewModel.FirstSittingOLevelResultDetails)
                    {
                        if (firstSittingOLevelResultDetail.Subject != null && firstSittingOLevelResultDetail.Grade != null)
                        {
                            ViewData["FirstSittingOLevelSubjects" + i] = new SelectList(existingViewModel.OLevelSubjectSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, firstSittingOLevelResultDetail.Subject.Id);
                            ViewData["FirstSittingOLevelGrades" + i] = new SelectList(existingViewModel.OLevelGradeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, firstSittingOLevelResultDetail.Grade.Id);
                        }
                        else
                        {
                            firstSittingOLevelResultDetail.Subject = new OLevelSubject() { Id = 0 };
                            firstSittingOLevelResultDetail.Grade = new OLevelGrade() { Id = 0 };

                            ViewData["FirstSittingOLevelSubjects" + i] = new SelectList(existingViewModel.OLevelSubjectSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                            ViewData["FirstSittingOLevelGrades" + i] = new SelectList(existingViewModel.OLevelGradeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        i++;
                    }
                }

                if (existingViewModel != null && existingViewModel.SecondSittingOLevelResultDetails != null && existingViewModel.SecondSittingOLevelResultDetails.Count > 0)
                {
                    int i = 0;
                    foreach (TeacherOLevelResultDetail secondSittingOLevelResultDetail in existingViewModel.SecondSittingOLevelResultDetails)
                    {
                        if (secondSittingOLevelResultDetail.Subject != null && secondSittingOLevelResultDetail.Grade != null)
                        {
                            ViewData["SecondSittingOLevelSubjects" + i] = new SelectList(existingViewModel.OLevelSubjectSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, secondSittingOLevelResultDetail.Subject.Id);
                            ViewData["SecondSittingOLevelGrades" + i] = new SelectList(existingViewModel.OLevelGradeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, secondSittingOLevelResultDetail.Grade.Id);
                        }
                        else
                        {
                            secondSittingOLevelResultDetail.Subject = new OLevelSubject() { Id = 0 };
                            secondSittingOLevelResultDetail.Grade = new OLevelGrade() { Id = 0 };

                            ViewData["SecondSittingOLevelSubjects" + i] = new SelectList(existingViewModel.OLevelSubjectSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                            ViewData["SecondSittingOLevelGrades" + i] = new SelectList(existingViewModel.OLevelGradeSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, 0);
                        }

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateOLevelResultDetail(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.FirstSittingOLevelResultDetails != null && viewModel.FirstSittingOLevelResultDetails.Count > 0)
                {
                    foreach (TeacherOLevelResultDetail firstSittingOLevelResultDetail in viewModel.FirstSittingOLevelResultDetails)
                    {
                        if (firstSittingOLevelResultDetail.Subject != null)
                        {
                            firstSittingOLevelResultDetail.Subject = viewModel.OLevelSubjects.Where(m => m.Id == firstSittingOLevelResultDetail.Subject.Id).SingleOrDefault();
                        }
                        if (firstSittingOLevelResultDetail.Grade != null)
                        {
                            firstSittingOLevelResultDetail.Grade = viewModel.OLevelGrades.Where(m => m.Id == firstSittingOLevelResultDetail.Grade.Id).SingleOrDefault();
                        }
                    }
                }

                if (viewModel != null && viewModel.SecondSittingOLevelResultDetails != null && viewModel.SecondSittingOLevelResultDetails.Count > 0)
                {
                    foreach (TeacherOLevelResultDetail secondSittingOLevelResultDetail in viewModel.SecondSittingOLevelResultDetails)
                    {
                        if (secondSittingOLevelResultDetail.Subject != null)
                        {
                            secondSittingOLevelResultDetail.Subject = viewModel.OLevelSubjects.Where(m => m.Id == secondSittingOLevelResultDetail.Subject.Id).SingleOrDefault();
                        }
                        if (secondSittingOLevelResultDetail.Grade != null)
                        {
                            secondSittingOLevelResultDetail.Grade = viewModel.OLevelGrades.Where(m => m.Id == secondSittingOLevelResultDetail.Grade.Id).SingleOrDefault();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        public JsonResult GetDayOfBirthBy(string monthId, string yearId)
        {
            try
            {
                if (string.IsNullOrEmpty(monthId) || string.IsNullOrEmpty(yearId))
                {
                    return null;
                }

                Value month = new Value() { Id = Convert.ToInt32(monthId) };
                Value year = new Value() { Id = Convert.ToInt32(yearId) };
                List<Value> days = UiUtility.GetNumberOfDaysInMonth(month, year);

                TeacherSubscriptionViewModel vm = (TeacherSubscriptionViewModel)TempData["TeacherSubscriptionViewModel"];
                vm.DayOfBirthSelectList = DropdownUtility.PopulateModelSelectListHelper(days, false);
                TempData["TeacherSubscriptionViewModel"] = vm;

                return Json(new SelectList(days, DropdownUtility.ID, DropdownUtility.NAME), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool InvalidDateOfBirth(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel.Teacher.YearOfBirth == null || viewModel.Teacher.YearOfBirth.Id <= 0)
                {
                    SetMessage("Please select Year of Birth!", ApplicationMessage.Category.Error);
                    return true;
                }
                else if (viewModel.Teacher.MonthOfBirth == null || viewModel.Teacher.MonthOfBirth.Id <= 0)
                {
                    SetMessage("Please select Month of Birth!", ApplicationMessage.Category.Error);
                    return true;
                }
                else if (viewModel.Teacher.DayOfBirth == null || viewModel.Teacher.DayOfBirth.Id <= 0)
                {
                    SetMessage("Please select Day of Birth!", ApplicationMessage.Category.Error);
                    return true;
                }

                viewModel.Teacher.DateOfBirth = new DateTime(viewModel.Teacher.YearOfBirth.Id, viewModel.Teacher.MonthOfBirth.Id, viewModel.Teacher.DayOfBirth.Id);
                if (viewModel.Teacher.DateOfBirth == null)
                {
                    SetMessage("Please enter Date of Birth!", ApplicationMessage.Category.Error);
                    return true;
                }

                TimeSpan difference = DateTime.Now - (DateTime)viewModel.Teacher.DateOfBirth;
                if (difference.Days == 0)
                {
                    SetMessage("Date of Birth cannot be todays date!", ApplicationMessage.Category.Error);
                    return true;
                }
                else if (difference.Days == -1)
                {
                    SetMessage("Date of Birth cannot be yesterdays date!", ApplicationMessage.Category.Error);
                    return true;
                }

                if (difference.Days < 1460)
                {
                    SetMessage("Teacher cannot be less than four years!", ApplicationMessage.Category.Error);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void SetDefaultSelectedSittingSubjectAndGrade(TeacherSubscriptionViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.FirstSittingOLevelResultDetails != null)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        ViewData["FirstSittingOLevelSubjects" + i] = viewModel.OLevelSubjectSelectList;
                        ViewData["FirstSittingOLevelGrades" + i] = viewModel.OLevelGradeSelectList;
                    }
                }

                if (viewModel != null && viewModel.SecondSittingOLevelResultDetails != null)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        ViewData["SecondSittingOLevelSubjects" + i] = viewModel.OLevelSubjectSelectList;
                        ViewData["SecondSittingOLevelGrades" + i] = viewModel.OLevelGradeSelectList;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetFirstSittingOLevelResult(TeacherOLevelResult oLevelResult, List<TeacherOLevelResultDetail> oLevelResultDetails)
        {
            try
            {
                if (oLevelResult != null && oLevelResult.Id > 0)
                {
                    if (oLevelResult.Type != null)
                    {
                        _viewModel.FirstSittingOLevelResult.Type = oLevelResult.Type;
                    }
                    else
                    {
                        _viewModel.FirstSittingOLevelResult.Type = new OLevelType();
                    }

                    //_viewModel.FirstSittingOLevelResult.Sitting = new OLevelExamSitting() { Id = 1 };
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

        public void SetSecondSittingOLevelResult(TeacherOLevelResult oLevelResult, List<TeacherOLevelResultDetail> oLevelResultDetails)
        {
            try
            {
                if (oLevelResult != null && oLevelResult.Id > 0)
                {
                    if (oLevelResult.Type != null)
                    {
                        _viewModel.SecondSittingOLevelResult.Type = oLevelResult.Type;
                    }
                    else
                    {
                        _viewModel.SecondSittingOLevelResult.Type = new OLevelType();
                    }

                    //_viewModel.FirstSittingOLevelResult.Sitting = new OLevelExamSitting() { Id = 2 };
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

        private bool InvalidOlevelResult(List<TeacherOLevelResultDetail> resultDetails, TeacherOLevelResult oLevelResult, string sitting)
        {
            try
            {
                if (resultDetails != null && resultDetails.Count > 0)
                {
                    List<TeacherOLevelResultDetail> subjectList = resultDetails.Where(r => r.Subject.Id > 0).ToList();

                    if (subjectList != null && subjectList.Count > 0)
                    {
                        if (string.IsNullOrEmpty(oLevelResult.ExamNumber) || oLevelResult.Type == null || oLevelResult.Type.Id <= 0 || oLevelResult.ExamYear <= 0)
                        {
                            SetMessage("Header Information not set for " + sitting + " O-Level Result Details! ", ApplicationMessage.Category.Error);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool InvalidOlevelSubjectOrGrade(List<TeacherOLevelResultDetail> oLevelResultDetails, List<OLevelSubject> subjects, List<OLevelGrade> grades, string sitting)
        {
            try
            {
                List<TeacherOLevelResultDetail> subjectList = null;
                if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
                {
                    subjectList = oLevelResultDetails.Where(r => r.Subject.Id > 0 || r.Grade.Id > 0).ToList();
                }

                foreach (TeacherOLevelResultDetail oLevelResultDetail in subjectList)
                {
                    OLevelSubject subject = subjects.Where(s => s.Id == oLevelResultDetail.Subject.Id).SingleOrDefault();
                    OLevelGrade grade = grades.Where(g => g.Id == oLevelResultDetail.Grade.Id).SingleOrDefault();

                    List<TeacherOLevelResultDetail> results = subjectList.Where(o => o.Subject.Id == oLevelResultDetail.Subject.Id).ToList();
                    if (results != null && results.Count > 1)
                    {
                        SetMessage("Duplicate " + subject.Name.ToUpper() + " Subject detected in " + sitting + "! Please modify.", ApplicationMessage.Category.Error);
                        return true;
                    }
                    else if (oLevelResultDetail.Subject.Id > 0 && oLevelResultDetail.Grade.Id <= 0)
                    {
                        SetMessage("No Grade specified for Subject " + subject.Name.ToUpper() + " in " + sitting + "! Please modify.", ApplicationMessage.Category.Error);
                        return true;
                    }
                    else if (oLevelResultDetail.Subject.Id <= 0 && oLevelResultDetail.Grade.Id > 0)
                    {
                        SetMessage("No Subject specified for Grade" + grade.Name.ToUpper() + " in " + sitting + "! Please modify.", ApplicationMessage.Category.Error);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool InvalidNumberOfOlevelSubject(List<TeacherOLevelResultDetail> firstSittingResultDetails, List<TeacherOLevelResultDetail> secondSittingResultDetails)
        {
            const int FIVE = 5;

            try
            {
                int totalNoOfSubjects = 0;

                List<TeacherOLevelResultDetail> firstSittingSubjectList = null;
                List<TeacherOLevelResultDetail> secondSittingSubjectList = null;

                if (firstSittingResultDetails != null && firstSittingResultDetails.Count > 0)
                {
                    firstSittingSubjectList = firstSittingResultDetails.Where(r => r.Subject.Id > 0).ToList();
                    if (firstSittingSubjectList != null)
                    {
                        totalNoOfSubjects += firstSittingSubjectList.Count;
                    }
                }

                if (secondSittingResultDetails != null && secondSittingResultDetails.Count > 0)
                {
                    secondSittingSubjectList = secondSittingResultDetails.Where(r => r.Subject.Id > 0).ToList();
                    if (secondSittingSubjectList != null)
                    {
                        totalNoOfSubjects += secondSittingSubjectList.Count;
                    }
                }

                if (totalNoOfSubjects == 0)
                {
                    SetMessage("No O-Level Result Details found for both sittings!", ApplicationMessage.Category.Error);
                    return true;
                }
                else if (totalNoOfSubjects < FIVE)
                {
                    SetMessage("O-Level Result cannot be less than " + FIVE + " subjects in both sittings!", ApplicationMessage.Category.Error);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool InvalidOlevelResultHeaderInformation(List<TeacherOLevelResultDetail> resultDetails, TeacherOLevelResult oLevelResult, string sitting)
        {
            try
            {
                if (resultDetails != null && resultDetails.Count > 0)
                {
                    List<TeacherOLevelResultDetail> subjectList = resultDetails.Where(r => r.Subject.Id > 0).ToList();

                    if (subjectList != null && subjectList.Count > 0)
                    {
                        if (string.IsNullOrEmpty(oLevelResult.ExamNumber))
                        {
                            SetMessage("O-Level Exam Number not set for " + sitting + " ! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }
                        else if (oLevelResult.Type == null || oLevelResult.Type.Id <= 0)
                        {
                            SetMessage("O-Level Exam Type not set for " + sitting + " ! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }
                        else if (oLevelResult.ExamYear <= 0)
                        {
                            SetMessage("O-Level Exam Year not set for " + sitting + " ! Please modify", ApplicationMessage.Category.Error);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool NoOlevelSubjectSpecified(List<TeacherOLevelResultDetail> oLevelResultDetails, TeacherOLevelResult oLevelResult, string sitting)
        {
            try
            {
                if (!string.IsNullOrEmpty(oLevelResult.ExamNumber) || (oLevelResult.Type != null && oLevelResult.Type.Id > 0) || (oLevelResult.ExamYear > 0))
                {
                    if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
                    {
                        List<TeacherOLevelResultDetail> oLevelResultDetailsEntered = oLevelResultDetails.Where(r => r.Subject.Id > 0).ToList();
                        if (oLevelResultDetailsEntered == null || oLevelResultDetailsEntered.Count <= 0)
                        {
                            SetMessage("No O-Level Subject specified for " + sitting + "! At least one subject must be specified when Exam Number, O-Level Type and Year are all specified for the sitting.", ApplicationMessage.Category.Error);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }




}