using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class LoginDetailTranslator : BaseTranslator<LoginDetail, PERSON_LOGIN>
    {
        private RoleTranslator _roleTranslator;
        private PersonTranslator _personTranslator;
        private SecurityQuestionTranslator _securityQuestionTranslator;

        public LoginDetailTranslator()
        {
            _roleTranslator = new RoleTranslator();
            _personTranslator = new PersonTranslator();
            _securityQuestionTranslator = new SecurityQuestionTranslator();
        }

        public override LoginDetail TranslateToModel(PERSON_LOGIN entity)
        {
            try
            {
                LoginDetail model = null;
                if (entity != null)
                {
                    model = new LoginDetail();
                    model.Person = _personTranslator.Translate(entity.PERSON);
                    //model.Username = entity.Username;
                    model.Password = entity.Password;
                    model.SecurityQuestion = _securityQuestionTranslator.Translate(entity.SECURITY_QUESTION);
                    model.SecurityAnswer = entity.Security_Answer;
                    model.Role = _roleTranslator.Translate(entity.ROLE);
                    model.IsActivated = entity.Is_Activated;
                    model.IsLocked = entity.Is_Locked;
                    model.IsFirstLogon = entity.Is_First_Login;
                    model.FirstLogonDate = entity.First_Login_Date;
                    model.LastLogonDate = entity.Last_Login_Date;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PERSON_LOGIN TranslateToEntity(LoginDetail model)
        {
            try
            {
                PERSON_LOGIN entity = null;
                if (model != null)
                {
                    entity = new PERSON_LOGIN();
                    entity.Person_Id = model.Person.Id;
                    //entity.Username = model.Username;
                    entity.Password = model.Password;
                    entity.Security_Question_Id = model.SecurityQuestion.Id;
                    entity.Security_Answer = model.SecurityAnswer;
                    entity.Role_Id = model.Role.Id;
                    entity.Is_Activated = model.IsActivated;
                    entity.Is_Locked = model.IsLocked;
                    entity.Is_First_Login = model.IsFirstLogon;
                    entity.First_Login_Date = model.FirstLogonDate;
                    entity.Last_Login_Date = model.LastLogonDate;
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
