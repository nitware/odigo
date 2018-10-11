using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using System.Transactions;
using Odigo.Business.Interfaces;
using Odigo.Entities;

namespace Odigo.Business
{
    public class TeacherSubscriptionEditorService : BaseTeacherSubscriptionService
    {
        private readonly IPersonEditorService _personEditorService;

        public TeacherSubscriptionEditorService(IRepository da, IImageManager passportManager, IPersonEditorService personEditorService, IPaymentService paymentService) : base(da, passportManager, paymentService)
        {
            if (personEditorService == null)
            {
                throw new ArgumentNullException("personEditorService");
            }

            _personEditorService = personEditorService;
        }
        
        public override Teacher Save(Teacher teacher)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    ModifyTeacher(teacher);
                    //ModifyPerson(teacher.Person);
                    _personEditorService.Modify(teacher.Person);
                    ModifyReferee(teacher.Referee);
                    DeleteOLevelResult(teacher);
                    
                    _da.Delete<TEACHER_EDUCATIONAL_QUALIFICATION>(p => p.Person_Id == teacher.Person.Id);
                    _da.Delete<TEACHER_EMPLOYMENT_HISTORY>(p => p.Person_Id == teacher.Person.Id);
                    _da.Delete<TEACHER_STUDENT_CATEGORY>(p => p.Person_Id == teacher.Person.Id);
                    _da.Delete<TEACHER_AVAILABILITY>(p => p.Person_Id == teacher.Person.Id);
                    _da.Delete<TEACHER_AWARD>(p => p.Person_Id == teacher.Person.Id);
                                        
                    _da.Create(teacher.StudentCategories);
                    _da.Create(teacher.TeacherAvailabilities);
                    _da.Create(teacher.EducationalQualifications);

                    if (teacher.EmploymentHistories != null && teacher.EmploymentHistories.Count > 0)
                    {
                        _da.Create(teacher.EmploymentHistories);
                    }
                    if (teacher.TeacherAwards != null && teacher.TeacherAwards.Count > 0)
                    {
                        _da.Create(teacher.TeacherAwards);
                    }
                    
                    string passportFilePath = SaveHelper(teacher);
                    teacher.ImageFileUrl = passportFilePath;
                    teacher.PaymentSlip = _paymentService.GetPaymentSlipBy(teacher.Person);
                    
                    transaction.Complete();
                }

                return teacher;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private void ModifyPerson(Person person)
        //{
        //    try
        //    {
        //        PERSON personEntity = _da.GetSingleBy<PERSON>(p => p.Person_Id == person.Id);
        //        if (personEntity != null)
        //        {
        //            personEntity.First_Name = person.FirstName;
        //            personEntity.Last_Name = person.LastName;
        //            personEntity.Person_Type_Id = person.Type.Id;
        //            personEntity.Other_Name = person.OtherName;
        //            personEntity.Contact_Address = person.ContactAddress;
        //            personEntity.Email = person.Email;
        //            personEntity.Mobile_Phone = person.MobilePhone;
        //            personEntity.State_Id = person.State.Id;
        //            personEntity.Lga_Id = person.Lga.Id;
        //            personEntity.Country_Id = person.Country.Id;
                    
        //            _da.Save();
        //        }
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        private void ModifyTeacher(Teacher teacher)
        {
            try
            {
                TEACHER teacherEntity = _da.GetSingleBy<TEACHER>(p => p.Person_Id == teacher.Person.Id);
                if (teacherEntity != null)
                {
                    teacherEntity.Teacher_Type_Id = teacher.Type.Id;
                    teacherEntity.Sex_Id = teacher.Sex.Id;
                    teacherEntity.Image_File_Url = teacher.ImageFileUrl;
                    teacherEntity.Date_Of_Birth = teacher.DateOfBirth;
                    teacherEntity.Home_Town = teacher.HomeTown;
                    teacherEntity.Home_Address = teacher.HomeAddress;

                    _da.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ModifyReferee(Referee referee)
        {
            try
            {
                TEACHER_REFEREE refereeEntity = _da.GetSingleBy<TEACHER_REFEREE>(p => p.Person_Id == referee.Person.Id);
                if (refereeEntity != null)
                {
                    refereeEntity.Referee_Name = referee.Name;
                    refereeEntity.Referee_Contact_Address = referee.ContactAddress;
                    refereeEntity.Referee_Mobile_Phone = referee.MobilePhone;
                    refereeEntity.Referee_Email_Address = referee.Email;

                    _da.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteOLevelResult(Teacher teacher)
        {
            try
            {
                List<TEACHER_O_LEVEL_RESULT> oLevelResultEntities = _da.FindAllBy<TEACHER_O_LEVEL_RESULT>(p => p.Person_Id == teacher.Person.Id);
                if (oLevelResultEntities != null && oLevelResultEntities.Count > 0)
                {
                    foreach (TEACHER_O_LEVEL_RESULT oLevelResultEntity in oLevelResultEntities)
                    {
                        _da.Delete<TEACHER_O_LEVEL_RESULT_DETAIL>(p => p.Teacher_O_Level_Result_Id == oLevelResultEntity.Teacher_O_Level_Result_Id);
                    }

                    _da.Delete<TEACHER_O_LEVEL_RESULT>(p => p.Person_Id == teacher.Person.Id);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        




    }


}
