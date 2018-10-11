using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Globalization;

namespace Odigo.Web.Models
{
    public class UiUtility
    {
        public static List<Value> CreateNumberListFrom(int startValue, int endValue)
        {
            List<Value> values = new List<Value>();
            
            try
            {
                for (int i = startValue; i <= endValue; i++)
                {
                    Value value = new Value();
                    value.Id = i;
                    value.Name = i.ToString();
                    values.Add(value);
                }

                return values;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Value> GetMonthsInYear()
        {
            List<Value> values = new List<Value>();

            try
            {
                string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;

                for (int i = 0; i < names.Length; i++)
                {
                    int j = i + 1;
                    Value value = new Value();
                    value.Id = j;
                    value.Name = names[i];
                    values.Add(value);
                }

                return values;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Value> GetNumberOfDaysInMonth(Value month, Value year)
        {
            try
            {
                int noOfDays = DateTime.DaysInMonth(year.Id, month.Id);
                List<Value> days = CreateNumberListFrom(1, noOfDays);
                return days;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static bool IsDateInTheFuture(DateTime date)
        {
            try
            {
                TimeSpan difference = DateTime.Now - date;
                if (difference.Days <= 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsStartDateGreaterThanEndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                TimeSpan difference = endDate - startDate;
                if (difference.Days <= 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsDateOutOfRange(DateTime startDate, DateTime endDate, int noOfDays)
        {
            try
            {
                TimeSpan difference = endDate - startDate;
                if (difference.Days < noOfDays)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<TeacherAvailability> InitializeAvailability(List<WeekDay> weekDays, List<Period> periods)
        {
            try
            {
                List<TeacherAvailability> teacherAvailabilities = new List<TeacherAvailability>();
                for (int j = 0; j < periods.Count; j++)
                {
                    for (int i = 0; i < weekDays.Count; i++)
                    {
                        if (i == 0 || j == 0)
                        {
                            continue;
                        }

                        TeacherAvailability teacherAvailability = new TeacherAvailability();
                        teacherAvailability.WeekDay = weekDays[i];
                        teacherAvailability.Period = periods[j];
                        teacherAvailability.IsAvailable = false;

                        teacherAvailabilities.Add(teacherAvailability);
                    }
                }

                return teacherAvailabilities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<EmployerDesiredTime> InitializeDesiredTime(List<WeekDay> weekDays, List<Period> periods)
        {
            try
            {
                List<EmployerDesiredTime> employerDesiredTimes = new List<EmployerDesiredTime>();
                for (int j = 0; j < periods.Count; j++)
                {
                    for (int i = 0; i < weekDays.Count; i++)
                    {
                        if (i == 0 || j == 0)
                        {
                            continue;
                        }

                        EmployerDesiredTime employerDesiredTime = new EmployerDesiredTime();
                        employerDesiredTime.WeekDay = weekDays[i];
                        employerDesiredTime.Period = periods[j];
                        employerDesiredTime.IsAvailable = false;

                        employerDesiredTimes.Add(employerDesiredTime);
                    }
                }

                return employerDesiredTimes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<EmployerStudentCategory> InitializeStudentCategory(List<EmployerStudentCategory> employerStudentCategories, List<WeekDay> weekDays, List<Period> periods)
        {
            try
            {
                if (employerStudentCategories == null || employerStudentCategories.Count <= 0)
                {
                    return null;
                }
                               
                for (int i = 0; i < employerStudentCategories.Count; i++)
                {
                    employerStudentCategories[i].NoOfStudent = 0;
                    employerStudentCategories[i].IsSelected = false;
                    employerStudentCategories[i].DesiredTimes = InitializeDesiredTime(weekDays, periods);
                }

                return employerStudentCategories;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<TeacherStudentCategory> InitializeStudentCategory(List<StudentCategory> studentCategories)
        {
            try
            {
                List<TeacherStudentCategory> teacherStudentCategories = new List<TeacherStudentCategory>();
                for (int i = 0; i < studentCategories.Count; i++)
                {
                    TeacherStudentCategory teacherStudentCategory = new TeacherStudentCategory();
                    teacherStudentCategory.StudentCategory = studentCategories[i];
                    teacherStudentCategory.IsSelected = false;

                    teacherStudentCategories.Add(teacherStudentCategory);
                }

                return teacherStudentCategories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<TeachingCost> InitializeTeachingCost(List<StudentCategory> studentCategories, List<QualificationCategory> qualificationCategories, List<TeachingCost> existingTeachingCosts)
        {
            try
            {
                //InitializeTeachingCostHelper();

                List<TeachingCost> teachingCosts = new List<TeachingCost>();
                for (int i = 0; i < qualificationCategories.Count; i++)
                {
                    for (int j = 0; j < studentCategories.Count; j++)
                    {

                        if (i == 0 || j == 0)
                        {
                            continue;
                        }

                        TeachingCost teachingCost = existingTeachingCosts.Where(t => t.StudentCategory.Id == studentCategories[j].Id && t.QualificationCategory.Id == qualificationCategories[i].Id).SingleOrDefault();

                        teachingCosts.Add(teachingCost);
                    }
                }

                return teachingCosts;
            }
            catch (Exception)
            {
                throw;
            }
        }








    }
}