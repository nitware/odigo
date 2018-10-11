using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherTranslator : BaseTranslator<Teacher, TEACHER>
    {
        private RatingTranslator _ratingTranslator;
        private PersonTranslator _personTranslator;
        private TeacherTypeTranslator _teacherTypeTranslator;
        private TeacherAvailabilityStatusTranslator _teacherAvailabilityStatusTranslator;
        private TeacherVerificationStatusTranslator _teacherVerificationStatusTranslator;
        private TeacherEducationalQualificationTranslator _teacherEducationalQualificationTranslator;
        private TeacherEmploymentHistoryTranslator _teacherEmploymentHistoryTranslator;
        private TeacherStudentCategoryTranslator _teacherStudentCategoryTranslator;
        private TeacherAvailabilityTranslator _teacherAvailabilityTranslator;
        private TeacherOLevelResultTranslator _teacherOLevelResultTranslator;
        private TeacherAwardTranslator _teacherAwardTranslator;
        private RefereeTranslator _refereeTranslator;
        private SexTranslator _sexTranslator;
        private LoginDetailTranslator loginDetalTranslator;

        public TeacherTranslator()
        {
            _ratingTranslator = new RatingTranslator();
            _personTranslator = new PersonTranslator();
            _refereeTranslator = new RefereeTranslator();
            _teacherTypeTranslator = new TeacherTypeTranslator();
            _teacherEmploymentHistoryTranslator = new TeacherEmploymentHistoryTranslator();
            _teacherAvailabilityStatusTranslator = new TeacherAvailabilityStatusTranslator();
            _teacherVerificationStatusTranslator = new TeacherVerificationStatusTranslator();
            _teacherEducationalQualificationTranslator = new TeacherEducationalQualificationTranslator();
            _teacherStudentCategoryTranslator = new TeacherStudentCategoryTranslator();
            _teacherAvailabilityTranslator = new TeacherAvailabilityTranslator();
            _teacherAwardTranslator = new TeacherAwardTranslator();
            _sexTranslator = new Translator.SexTranslator();
            loginDetalTranslator = new LoginDetailTranslator();

            _teacherOLevelResultTranslator = new TeacherOLevelResultTranslator();
        }

        public override Teacher TranslateToModel(TEACHER entity)
        {
            try
            {
                Teacher model = null;
                if (entity != null)
                {
                    model = new Teacher();
                    model.Person = _personTranslator.Translate(entity.PERSON);
                    model.Type = _teacherTypeTranslator.Translate(entity.TEACHER_TYPE);
                    model.Rating = _ratingTranslator.Translate(entity.RATING);
                    model.VerificationStatus = _teacherVerificationStatusTranslator.Translate(entity.TEACHER_VERIFICATION_STATUS);
                    model.AvailabilityStatus = _teacherAvailabilityStatusTranslator.Translate(entity.TEACHER_AVAILABILITY_STATUS);

                    model.Sex = _sexTranslator.TranslateToModel(entity.SEX);
                    model.ImageFileUrl = entity.Image_File_Url;
                    model.DateOfBirth = entity.Date_Of_Birth;
                    model.HomeTown = entity.Home_Town;
                    model.HomeAddress = entity.Home_Address;

                    model.YearOfBirth = new Value() { Id = model.DateOfBirth.Year };
                    model.MonthOfBirth = new Value() { Id = model.DateOfBirth.Month };
                    model.DayOfBirth = new Value() { Id = model.DateOfBirth.Day };
                    
                    //if (model.DateOfBirth.HasValue)
                    //{
                    //    model.YearOfBirth = new Value() { Id = model.DateOfBirth.Value.Year };
                    //    model.MonthOfBirth = new Value() { Id = model.DateOfBirth.Value.Month };
                    //    model.DayOfBirth = new Value() { Id = model.DateOfBirth.Value.Day };
                    //}

                    if (entity.PERSON != null && entity.PERSON.PERSON_LOGIN != null)
                    {
                        model.LoginDetail = loginDetalTranslator.Translate(entity.PERSON.PERSON_LOGIN);
                    }
                    if (entity.TEACHER_EDUCATIONAL_QUALIFICATION != null && entity.TEACHER_EDUCATIONAL_QUALIFICATION.Count > 0)
                    {
                        model.EducationalQualifications = _teacherEducationalQualificationTranslator.Translate(entity.TEACHER_EDUCATIONAL_QUALIFICATION.ToList());
                    }
                    if (entity.TEACHER_STUDENT_CATEGORY != null && entity.TEACHER_STUDENT_CATEGORY.Count > 0)
                    {
                        model.StudentCategories = _teacherStudentCategoryTranslator.Translate(entity.TEACHER_STUDENT_CATEGORY.ToList());
                    }
                    if (entity.TEACHER_EMPLOYMENT_HISTORY != null && entity.TEACHER_EMPLOYMENT_HISTORY.Count > 0)
                    {
                        model.EmploymentHistories = _teacherEmploymentHistoryTranslator.Translate(entity.TEACHER_EMPLOYMENT_HISTORY.ToList());
                    }
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER TranslateToEntity(Teacher model)
        {
            try
            {
                TEACHER entity = null;
                if (model != null)
                {
                    entity = new TEACHER();
                    entity.Person_Id = model.Person.Id;
                    entity.Teacher_Type_Id = model.Type.Id;
                    entity.Rating_Id = model.Rating.Id;
                    entity.Verification_Status_Id = model.VerificationStatus.Id;
                    entity.Availability_Status_Id = model.AvailabilityStatus.Id;
                    entity.Sex_Id = model.Sex.Id;
                    entity.Image_File_Url = model.ImageFileUrl;
                    entity.Date_Of_Birth = model.DateOfBirth;
                    entity.Home_Town = model.HomeTown;
                    entity.Home_Address = model.HomeAddress;

                    entity.PERSON = _personTranslator.TranslateToEntity(model.Person);
                    entity.TEACHER_REFEREE = _refereeTranslator.TranslateToEntity(model.Referee);
                    entity.TEACHER_EDUCATIONAL_QUALIFICATION = _teacherEducationalQualificationTranslator.Translate(model.EducationalQualifications);

                    if (model.EmploymentHistories != null && model.EmploymentHistories.Count > 0)
                    {
                        entity.TEACHER_EMPLOYMENT_HISTORY = _teacherEmploymentHistoryTranslator.Translate(model.EmploymentHistories);
                    }
                    entity.TEACHER_STUDENT_CATEGORY = _teacherStudentCategoryTranslator.Translate(model.StudentCategories);
                    entity.TEACHER_AVAILABILITY = _teacherAvailabilityTranslator.Translate(model.TeacherAvailabilities);

                    if (model.TeacherAwards != null && model.TeacherAwards.Count > 0)
                    {
                        entity.TEACHER_AWARD = _teacherAwardTranslator.Translate(model.TeacherAwards);
                    }

                    if (model.LoginDetail != null && model.LoginDetail.Person != null)
                    {
                        entity.PERSON.PERSON_LOGIN = loginDetalTranslator.Translate(model.LoginDetail);
                    }
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }
}
