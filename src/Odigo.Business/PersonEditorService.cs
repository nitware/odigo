using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Entities;

namespace Odigo.Business
{
    public class PersonEditorService : IPersonEditorService
    {
        private readonly IRepository _da;

        public PersonEditorService(IRepository da)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }

            _da = da;
        }

        public void Modify(Person person)
        {
            try
            {
                PERSON personEntity = _da.GetSingleBy<PERSON>(p => p.Person_Id == person.Id);
                if (personEntity != null)
                {
                    personEntity.First_Name = person.FirstName;
                    personEntity.Last_Name = person.LastName;
                    personEntity.Person_Type_Id = person.Type.Id;
                    personEntity.Other_Name = person.OtherName;
                    personEntity.Contact_Address = person.ContactAddress;
                    personEntity.Email = person.Email;
                    personEntity.Mobile_Phone = person.MobilePhone;
                    personEntity.State_Id = person.State.Id;
                    personEntity.Lga_Id = person.Lga.Id;
                    personEntity.Country_Id = person.Country.Id;

                    _da.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
