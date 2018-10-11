using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Translator
{
    public class TranslatorFactory
    {
        public static dynamic Create(string typeName)
        {
            try
            {
                typeName = typeName.Replace("_", "").ToLower();
                switch (typeName)
                {
                    case "country":
                        {
                            return new CountryTranslator();
                        }
                    case "employerdesiredtime":
                        {
                            return new EmployerDesiredTimeTranslator();
                        }
                    case "employerstudentcategory":
                        {
                            return new EmployerStudentCategoryTranslator();
                        }
                    case "employer":
                        {
                            return new EmployerTranslator();
                        }
                    case "lga":
                        {
                            return new LgaTranslator();
                        }
                    case "personlogin":
                    case "logindetail":
                        {
                            return new LoginDetailTranslator();
                        }
                    case "message":
                        {
                            return new MessageTranslator();
                        }
                    case "messagetype":
                        {
                            return new MessageTypeTranslator();
                        }
                    case "news":
                        {
                            return new NewsTranslator();
                        }
                    case "occupation":
                        {
                            return new OccupationTranslator();
                        }
                    case "olevelexamsitting":
                        {
                            return new OLevelExamSittingTranslator();
                        }
                    case "olevelgrade":
                        {
                            return new OLevelGradeTranslator();
                        }
                    case "olevelsubject":
                        {
                            return new OLevelSubjectTranslator();
                        }
                    case "oleveltype":
                        {
                            return new OLevelTypeTranslator();
                        }
                    case "paymentmode":
                        {
                            return new PaymentModeTranslator();
                        }
                    case "payment":
                        {
                            return new PaymentTranslator();
                        }
                    case "period":
                        {
                            return new PeriodTranslator();
                        }
                    case "person":
                        {
                            return new PersonTranslator();
                        }
                    case "persontype":
                        {
                            return new PersonTypeTranslator();
                        }
                    case "qualificationcategory":
                        {
                            return new QualificationCategoryTranslator();
                        }
                    case "qualification":
                        {
                            return new QualificationTranslator();
                        }
                    case "quicklink":
                        {
                            return new QuickLinkTranslator();
                        }
                    case "teacherreferee":
                        {
                            return new RefereeTranslator();
                        }
                    case "requestforemploymentcostimplication":
                        {
                            return new RequestForEmploymentCostImplicationTranslator();
                        }
                    case "requestforemploymentteacheravailability":
                        {
                            return new RequestForEmploymentTeacherAvailabilityTranslator();
                        }
                    case "requeststatus":
                        {
                            return new RequestStatusTranslator();
                        }
                    case "request":
                        {
                            return new RequestTranslator();
                        }
                    case "right":
                        {
                            return new RightTranslator();
                        }
                    case "roleright":
                        {
                            return new RoleRightTranslator();
                        }
                    case "role":
                        {
                            return new RoleTranslator();
                        }
                    case "schooltype":
                        {
                            return new SchoolTypeTranslator();
                        }
                    case "securityquestion":
                        {
                            return new SecurityQuestionTranslator();
                        }
                    case "servicecharge":
                        {
                            return new ServiceChargeTranslator();
                        }
                    case "service":
                        {
                            return new ServiceTranslator();
                        }
                    case "sex":
                        {
                            return new SexTranslator();
                        }
                    case "state":
                        {
                            return new StateTranslator();
                        }
                    case "studentcategory":
                        {
                            return new StudentCategoryTranslator();
                        }
                    case "teacheravailability":
                        {
                            return new TeacherAvailabilityTranslator();
                        }
                    case "teacheraward":
                        {
                            return new TeacherAwardTranslator();
                        }
                    case "teachereducationalqualification":
                        {
                            return new TeacherEducationalQualificationTranslator();
                        }
                    case "teacheremploymenthistory":
                        {
                            return new TeacherEmploymentHistoryTranslator();
                        }
                    case "teacherolevelresultdetail":
                        {
                            return new TeacherOLevelResultDetailTranslator();
                        }
                    case "teacherolevelresult":
                        {
                            return new TeacherOLevelResultTranslator();
                        }
                    case "teacherstudentcategory":
                        {
                            return new TeacherStudentCategoryTranslator();
                        }
                    case "teacher":
                        {
                            return new TeacherTranslator();
                        }
                    case "teachertype":
                        {
                            return new TeacherTypeTranslator();
                        }
                    case "teachingcost":
                        {
                            return new TeachingCostTranslator();
                        }
                    case "weekday":
                        {
                            return new WeekDayTranslator();
                        }
                    default:
                        {
                            throw new NotImplementedException(typeName + " Translator not implemented!");
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
