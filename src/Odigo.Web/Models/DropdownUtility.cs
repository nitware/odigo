using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Web.Mvc;
using System.Reflection;
using Odigo.Business.Interfaces;

namespace Odigo.Web.Models
{
    public class DropdownUtility
    {
        public const string ID = "Id";
        public const string NAME = "Name";
        public const string TEXT = "Text";
        public const string VALUE = "Value";
        public const string Select = "-- Select --";
        
        public static List<Value> CreateYearListFrom(int startYear)
        {
            List<Value> years = new List<Value>();

            try
            {
                DateTime currentDate = DateTime.Now;
                int currentYear = currentDate.Year;

                for (int i = startYear; i <= currentYear; i++)
                {
                    Value value = new Value();
                    value.Id = i;
                    value.Name = i.ToString();
                    years.Add(value);
                }

                return years;
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        public static List<SelectListItem> PopulateModelSelectList<T, E>(IRepository da, bool isNullable) where E : class
        {
            try
            {
                List<T> models = da.GetAll<T, E>();

                if (models == null || models.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                return PopulateModelSelectListHelper<T>(models, isNullable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateModelSelectListHelper<T>(List<T> models, bool isNullable)
        {
            try
            {
                return LoadModelSelectListBy(models, isNullable, Select);

                //List<SelectListItem> modelListItems = new List<SelectListItem>();
                //if (models != null && models.Count > 0)
                //{
                //    SelectListItem list = new SelectListItem();
                //    if (isNullable)
                //    {
                //        list.Value = "0";
                //    }
                //    else
                //    {
                //        list.Value = "";
                //    }

                //    list.Text = Select;
                //    modelListItems.Add(list);

                //    foreach (T t in models)
                //    {
                //        SelectListItem selectList = SetModelValue<T>(t);
                //        modelListItems.Add(selectList);
                //    }
                //}

                //return modelListItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateModelSelectListHelper<T>(List<T> models, bool isNullable, string defaultText)
        {
            try
            {
                return LoadModelSelectListBy(models, isNullable, defaultText);

                //List<SelectListItem> modelListItems = new List<SelectListItem>();
                //if (models != null && models.Count > 0)
                //{
                //    SelectListItem list = new SelectListItem();
                //    if (isNullable)
                //    {
                //        list.Value = "0";
                //    }
                //    else
                //    {
                //        list.Value = "";
                //    }

                //    list.Text = defaultText;
                //    modelListItems.Add(list);

                //    foreach (T t in models)
                //    {
                //        SelectListItem selectList = SetModelValue<T>(t);
                //        modelListItems.Add(selectList);
                //    }
                //}

                //return modelListItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<SelectListItem> LoadModelSelectListBy<T>(List<T> models, bool isNullable, string defaultText)
        {
            try
            {
                List<SelectListItem> modelListItems = new List<SelectListItem>();
                if (models != null && models.Count > 0)
                {
                    SelectListItem list = new SelectListItem();
                    if (isNullable)
                    {
                        list.Value = "0";
                    }
                    else
                    {
                        list.Value = "";
                    }

                    list.Text = defaultText;
                    modelListItems.Add(list);

                    foreach (T t in models)
                    {
                        SelectListItem selectList = SetModelValue<T>(t);
                        modelListItems.Add(selectList);
                    }
                }

                return modelListItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateModelSelectListHelper<T>(List<T> models, string id, string name)
        {
            try
            {
                List<SelectListItem> modelListItems = new List<SelectListItem>();
                if (models != null && models.Count > 0)
                {
                    SelectListItem list = new SelectListItem();
                    list.Value = "";
                    list.Text = Select;
                    modelListItems.Add(list);

                    foreach (T t in models)
                    {
                        SelectListItem selectList = SetModelValue<T>(t, id, name);
                        modelListItems.Add(selectList);
                    }
                }

                return modelListItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SelectListItem SetModelValue<T>(T t)
        {
            try
            {
                if (t == null)
                {
                    throw new NullReferenceException("Model cannot be null! Please contact your system administrator.");
                }

                PropertyInfo idPropertyInfo = t.GetType().GetProperty(ID);
                PropertyInfo namePropertyInfo = t.GetType().GetProperty(NAME);

                if (idPropertyInfo == null || namePropertyInfo == null)
                {
                    throw new NullReferenceException("Property Name specified does not exist in the supplied Model! Please contact your system administrator.");
                }

                SelectListItem selectList = new SelectListItem();
                selectList.Value = t.GetType().GetProperty(ID).GetValue(t, null).ToString();
                selectList.Text = t.GetType().GetProperty(NAME).GetValue(t, null).ToString();

                return selectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SelectListItem SetModelValue<T>(T t, string id, string name)
        {
            try
            {
                if (t == null)
                {
                    throw new NullReferenceException("Model cannot be null! Please contact your system administrator.");
                }

                PropertyInfo idPropertyInfo = t.GetType().GetProperty(id);
                PropertyInfo namePropertyInfo = t.GetType().GetProperty(name);

                if (idPropertyInfo == null || namePropertyInfo == null)
                {
                    throw new NullReferenceException("Property Name specified does not exist in the supplied Model! Please contact your system administrator.");
                }

                SelectListItem selectList = new SelectListItem();
                selectList.Value = t.GetType().GetProperty(id).GetValue(t, null).ToString();
                selectList.Text = t.GetType().GetProperty(name).GetValue(t, null).ToString();

                return selectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateMonthSelectListItem()
        {
            try
            {
                List<Value> months = new List<Value>();
                Value january = new Value() { Id = 1, Name = "January" };
                Value february = new Value() { Id = 2, Name = "February" };
                Value march = new Value() { Id = 3, Name = "March" };
                Value april = new Value() { Id = 4, Name = "April" };
                Value may = new Value() { Id = 5, Name = "May" };
                Value june = new Value() { Id = 6, Name = "June" };
                Value july = new Value() { Id = 7, Name = "July" };
                Value august = new Value() { Id = 8, Name = "August" };
                Value september = new Value() { Id = 9, Name = "September" };
                Value october = new Value() { Id = 10, Name = "October" };
                Value november = new Value() { Id = 11, Name = "November" };
                Value december = new Value() { Id = 12, Name = "December" };

                months.Add(january);
                months.Add(february);
                months.Add(march);
                months.Add(april);
                months.Add(may);
                months.Add(june);
                months.Add(july);
                months.Add(august);
                months.Add(september);
                months.Add(october);
                months.Add(november);
                months.Add(december);

                List<SelectListItem> monthList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = "--MM--";
                monthList.Add(list);

                foreach (Value month in months)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = month.Id.ToString();
                    selectList.Text = month.Name;

                    monthList.Add(selectList);
                }

                return monthList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateYearSelectListItem(int startYear, bool withSelect, bool isNullable)
        {
            try
            {
                List<Value> years = CreateYearListFrom(startYear);
                if (years == null || years.Count <= 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> yearList = new List<SelectListItem>();

                SelectListItem list = new SelectListItem();

                //list.Value = "";
                if (isNullable)
                {
                    list.Value = "0";
                }
                else
                {
                    list.Value = "";
                }

                if (withSelect)
                {
                    list.Text = Select;
                }
                else
                {
                    list.Text = "--YY--";
                }

                yearList.Add(list);

                foreach (Value year in years)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = year.Id.ToString();
                    selectList.Text = year.Name;

                    yearList.Add(selectList);
                }

                return yearList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateDaySelectListItem(Value month, Value year)
        {
            try
            {
                List<Value> days = GetNumberOfDaysInMonth(month, year);

                List<SelectListItem> dayList = new List<SelectListItem>();
                SelectListItem list = new SelectListItem();
                list.Value = "";
                list.Text = "--DD--";

                dayList.Add(list);
                foreach (Value day in days)
                {
                    SelectListItem selectList = new SelectListItem();
                    selectList.Value = day.Id.ToString();
                    selectList.Text = day.Name;

                    dayList.Add(selectList);
                }

                return dayList;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}