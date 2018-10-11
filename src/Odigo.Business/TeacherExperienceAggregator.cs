using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;

namespace Odigo.Business
{
    public class TeacherExperienceAggregator : IModelAggregator<TeacherEmploymentHistory>
    {
        public string Aggregate(List<TeacherEmploymentHistory> teacherEmploymentHistories)
        {
            try
            {
                string employmentHistories = null;
                if (teacherEmploymentHistories != null && teacherEmploymentHistories.Count > 0)
                {
                    for (int i = 0; i < teacherEmploymentHistories.Count; i++)
                    {
                        employmentHistories += teacherEmploymentHistories[i].Employer + ": " + teacherEmploymentHistories[i].LastPositionHeld + " (" + teacherEmploymentHistories[i].StartYear + " - " + teacherEmploymentHistories[i].EndYear + ")";
                        if (i != teacherEmploymentHistories.Count - 1)
                        {
                            employmentHistories += ", ";
                        }
                    }
                }

                return employmentHistories;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }



}
