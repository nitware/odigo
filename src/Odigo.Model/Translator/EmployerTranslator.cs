using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class EmployerTranslator : BaseTranslator<Employer, EMPLOYER>
    {
        private PaymentTranslator _paymentTranslator;
        private LoginDetailTranslator _loginDetalTranslator;
        private PersonTranslator _personTranslator;
        private SexTranslator _sexTranslator;

        public EmployerTranslator()
        {
            _paymentTranslator = new PaymentTranslator();
            _loginDetalTranslator = new LoginDetailTranslator();
            _personTranslator = new PersonTranslator();
            _sexTranslator = new SexTranslator();
        }

        public override Employer TranslateToModel(EMPLOYER entity)
        {
            try
            {
                Employer model = null;
                if (entity != null)
                {
                    model = new Employer();
                    model.Person = _personTranslator.Translate(entity.PERSON);
                    model.Name = entity.Employer_Name;
                    model.Website = entity.Website;
                    model.Sex = _sexTranslator.Translate(entity.SEX);

                    if (entity.PERSON != null && entity.PERSON.PERSON_LOGIN != null)
                    {
                        model.LoginDetail = _loginDetalTranslator.Translate(entity.PERSON.PERSON_LOGIN);
                    }
                    if (entity.PERSON != null && entity.PERSON.PAYMENT != null && entity.PERSON.PAYMENT.Count > 0)
                    {
                        model.Payments = _paymentTranslator.Translate(entity.PERSON.PAYMENT.ToList());
                    }
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override EMPLOYER TranslateToEntity(Employer model)
        {
            try
            {
                EMPLOYER entity = null;
                if (model != null)
                {
                    entity = new EMPLOYER();
                    entity.Person_Id = model.Person.Id;
                    entity.Employer_Name = model.Name;
                    entity.Website = model.Website;

                    if (model.Sex != null)
                    {
                        entity.Sex_Id = model.Sex.Id;
                    }

                    entity.PERSON = _personTranslator.TranslateToEntity(model.Person);
                    if (model.LoginDetail != null && model.LoginDetail.Person != null)
                    {
                        entity.PERSON.PERSON_LOGIN = _loginDetalTranslator.Translate(model.LoginDetail);
                    }
                    //if (model.Payments != null && model.Payments.Count > 0)
                    //{
                    //    entity.PERSON.PAYMENT = _paymentTranslator.Translate(model.Payments);
                    //}
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
