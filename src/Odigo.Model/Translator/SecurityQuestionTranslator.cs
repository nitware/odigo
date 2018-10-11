using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class SecurityQuestionTranslator : BaseTranslator<SecurityQuestion, SECURITY_QUESTION>
    {
        public override SecurityQuestion TranslateToModel(SECURITY_QUESTION entity)
        {
            try
            {
                SecurityQuestion model = null;
                if (entity != null)
                {
                    model = new SecurityQuestion();
                    model.Id = entity.Security_Question_Id;
                    model.Question = entity.Question;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override SECURITY_QUESTION TranslateToEntity(SecurityQuestion model)
        {
            try
            {
                SECURITY_QUESTION entity = null;
                if (model != null)
                {
                    entity = new SECURITY_QUESTION();
                    entity.Security_Question_Id = model.Id;
                    entity.Question = model.Question;
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
