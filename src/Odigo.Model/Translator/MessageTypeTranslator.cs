using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class MessageTypeTranslator : BaseTranslator<MessageType, MESSAGE_TYPE>
    {
        public override MessageType TranslateToModel(MESSAGE_TYPE entity)
        {
            try
            {
                MessageType model = null;
                if (entity != null)
                {
                    model = new MessageType();
                    model.Id = entity.Message_Type_Id;
                    model.Name = entity.Message_Type_Name;
                    model.Description = entity.Message_Type_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override MESSAGE_TYPE TranslateToEntity(MessageType model)
        {
            try
            {
                MESSAGE_TYPE entity = null;
                if (model != null)
                {
                    entity = new MESSAGE_TYPE();
                    entity.Message_Type_Id = model.Id;
                    entity.Message_Type_Name = model.Name;
                    entity.Message_Type_Description = model.Description;
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
