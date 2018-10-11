using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class CountryTranslator : BaseTranslator<Country, COUNTRY>
    {
        public override Country TranslateToModel(COUNTRY countryEntity)
        {
            try
            {
                Country country = null;
                if (countryEntity != null)
                {
                    country = new Country();
                    country.Id = countryEntity.Country_Id;
                    country.Name = countryEntity.Country_Name;
                }

                return country;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override COUNTRY TranslateToEntity(Country country)
        {
            try
            {
                COUNTRY countryEntity = null;
                if (country != null)
                {
                    countryEntity = new COUNTRY();
                    countryEntity.Country_Id = country.Id;
                    countryEntity.Country_Name = country.Name;
                }

                return countryEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
