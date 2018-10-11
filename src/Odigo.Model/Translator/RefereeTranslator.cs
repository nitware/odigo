using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
   public class RefereeTranslator : BaseTranslator<Referee, TEACHER_REFEREE>
    {
       private PersonTranslator personTranslator;
       
       public RefereeTranslator()
       {
           personTranslator = new PersonTranslator();
       }

       public override Referee TranslateToModel(TEACHER_REFEREE entity)
        {
            try
            {
                Referee model = null;
                if (entity != null)
                {
                    model = new Referee();
                    model.Person = personTranslator.Translate(entity.TEACHER.PERSON);
                    model.Name = entity.Referee_Name;
                    model.ContactAddress = entity.Referee_Contact_Address;
                    model.MobilePhone = entity.Referee_Mobile_Phone;
                    model.Email = entity.Referee_Email_Address;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

       public override TEACHER_REFEREE TranslateToEntity(Referee model)
       {
           try
           {
                TEACHER_REFEREE entity = null;
               if (model != null)
               {
                    entity = new TEACHER_REFEREE();
                    entity.Person_Id = model.Person.Id;
                    entity.Referee_Name = model.Name;
                    entity.Referee_Contact_Address = model.ContactAddress;
                    entity.Referee_Mobile_Phone = model.MobilePhone;
                    entity.Referee_Email_Address = model.Email;
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
