using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class PersonTranslator : BaseTranslator<Person, PERSON>
    {
        private LgaTranslator lgaTranslator;
        private StateTranslator stateTranslator;
        private PersonTypeTranslator personTypeTranslator;
        private CountryTranslator countryTranslator;

        public PersonTranslator()
        {
            lgaTranslator = new LgaTranslator();
            stateTranslator = new StateTranslator();
            countryTranslator = new CountryTranslator();
            personTypeTranslator = new PersonTypeTranslator();
        }

        public override Person TranslateToModel(PERSON entity)
        {
            try
            {
                Person person = null;
                if (entity != null)
                {
                    person = new Person();
                    person.Id = entity.Person_Id;
                    person.FirstName = entity.First_Name;
                    person.LastName = entity.Last_Name;
                    person.Type = personTypeTranslator.TranslateToModel(entity.PERSON_TYPE);
                    person.OtherName = entity.Other_Name;
                    person.ContactAddress = entity.Contact_Address;
                    person.Email = entity.Email;
                    person.MobilePhone = entity.Mobile_Phone;
                    person.Lga = lgaTranslator.TranslateToModel(entity.LGA);
                    person.State = stateTranslator.TranslateToModel(entity.STATE);
                    person.Country = countryTranslator.TranslateToModel(entity.COUNTRY);
                    person.DateEntered = entity.Date_Entered;
                    person.IsBlacklisted = entity.Is_Blacklisted;
                }

                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public override PERSON TranslateToEntity(Person person)
        {
            try
            {
                PERSON entity = null;
                if (person != null)
                {
                    entity = new PERSON();
                    entity.Person_Id = person.Id;
                    entity.First_Name = person.FirstName;
                    entity.Last_Name = person.LastName;
                    entity.Person_Type_Id = person.Type.Id;
                    entity.Other_Name = person.OtherName;
                    entity.Contact_Address = person.ContactAddress;
                    entity.Email = person.Email;
                    entity.Mobile_Phone = person.MobilePhone;
                    entity.State_Id = person.State.Id;
                    entity.Lga_Id = person.Lga.Id;
                    entity.Country_Id = person.Country.Id;
                    entity.Date_Entered = person.DateEntered;
                    entity.Is_Blacklisted = person.IsBlacklisted;
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
