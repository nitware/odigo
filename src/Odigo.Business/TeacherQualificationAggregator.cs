using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;

namespace Odigo.Business
{
    public class TeacherQualificationAggregator : IModelAggregator<TeacherEducationalQualification>
    {
        public string Aggregate(List<TeacherEducationalQualification> teacherEducationalQualifications)
        {
            try
            {
                string qualification = null;
                if (teacherEducationalQualifications != null && teacherEducationalQualifications.Count > 0)
                {
                    List<int> schoolTypeIds = teacherEducationalQualifications.GroupBy(sp => sp.SchoolType.Id).Select(k => k.Key).ToList();
                    if (schoolTypeIds != null && schoolTypeIds.Count > 0)
                    {
                        for (int i = 0; i < schoolTypeIds.Count; i++)
                        {
                            List<TeacherEducationalQualification> educationalQualifications = teacherEducationalQualifications.Where(t => t.SchoolType.Id == schoolTypeIds[i]).ToList();
                            if (educationalQualifications != null && educationalQualifications.Count > 0)
                            {
                                for (int j = 0; j < educationalQualifications.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        qualification += educationalQualifications[j].SchoolType.Name + " (" + educationalQualifications[j].Qualification.Name;
                                    }
                                    else
                                    {
                                        qualification += ", " + educationalQualifications[j].Qualification.Name;
                                    }
                                }

                                qualification += ")";
                            }

                            if (i != schoolTypeIds.Count - 1)
                            {
                                qualification += ", ";
                            }
                        }
                    }
                }

                return qualification;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }


}
