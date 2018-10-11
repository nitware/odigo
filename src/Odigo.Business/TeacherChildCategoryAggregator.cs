using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;

namespace Odigo.Business
{
    public class TeacherChildCategoryAggregator : IModelAggregator<TeacherStudentCategory>
    {
        public string Aggregate(List<TeacherStudentCategory> teacherStudentCategories)
        {
            try
            {
                string studentCategories = null;
                if (teacherStudentCategories != null && teacherStudentCategories.Count > 0)
                {
                    for (int i = 0; i < teacherStudentCategories.Count; i++)
                    {
                        studentCategories += teacherStudentCategories[i].StudentCategory.Name;
                        if (i != teacherStudentCategories.Count - 1)
                        {
                            studentCategories += ", ";
                        }
                    }
                }

                return studentCategories;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }

}
