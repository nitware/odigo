using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class StateTranslator : BaseTranslator<State, STATE>
    {
        private CountryTranslator _countryTranslator;

        public StateTranslator()
        {
            _countryTranslator = new CountryTranslator();
        }

        public override State TranslateToModel(STATE stateEntity)
        {
            try
            {
                State state = null;
                if (stateEntity != null)
                {
                    state = new State();
                    state.Id = stateEntity.State_Id;
                    state.Name = stateEntity.State_Name;
                    state.Country = _countryTranslator.TranslateToModel(stateEntity.COUNTRY);
                }

                return state;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STATE TranslateToEntity(State state)
        {
            try
            {
                STATE stateEntity = null;
                if (state != null)
                {
                    stateEntity = new STATE();
                    stateEntity.State_Id = state.Id;
                    stateEntity.State_Name = state.Name;
                    stateEntity.Country_Id = state.Country.Id;
                }

                return stateEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
