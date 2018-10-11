using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class RequestForEmploymentTeacherAvailabilityTranslator : BaseTranslator<RequestForEmploymentTeacherAvailability, REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY>
    {
        private TeacherAvailabilityTranslator _teacherAvailabilityTranslator;
        private RequestForEmploymentCostImplicationTranslator _requestForEmploymentCostImplicationTranslator;
        private TeachingCostTranslator _teachingCostTranslator;

        public RequestForEmploymentTeacherAvailabilityTranslator()
        {
            _teachingCostTranslator = new TeachingCostTranslator();
            _teacherAvailabilityTranslator = new Translator.TeacherAvailabilityTranslator();
            _requestForEmploymentCostImplicationTranslator = new RequestForEmploymentCostImplicationTranslator();
        }

        public override RequestForEmploymentTeacherAvailability TranslateToModel(REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY entity)
        {
            try
            {
                RequestForEmploymentTeacherAvailability model = null;
                if (entity != null)
                {
                    model = new RequestForEmploymentTeacherAvailability();
                    model.Id = entity.Request_For_Employment_Teacher_Availability_Id;
                    model.CostImplication = _requestForEmploymentCostImplicationTranslator.Translate(entity.REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION);
                    model.TeacherAvailability = _teacherAvailabilityTranslator.Translate(entity.TEACHER_AVAILABILITY);
                    model.TeachingCost = _teachingCostTranslator.Translate(entity.TEACHING_COST);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY TranslateToEntity(RequestForEmploymentTeacherAvailability model)
        {
            try
            {
                REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY entity = null;
                if (model != null)
                {
                    entity = new REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY();
                    entity.Request_For_Employment_Teacher_Availability_Id = model.Id;
                    entity.Request_For_Employment_Cost_Implication_Id = model.CostImplication.Id;
                    entity.Teacher_Availability_Id = model.TeacherAvailability.Id;
                    entity.Teaching_Cost_Id = model.TeachingCost.Id;
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
