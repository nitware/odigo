using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class QuickLinkTranslator : BaseTranslator<QuickLink, QUICK_LINK>
    {
        public override QuickLink TranslateToModel(QUICK_LINK entity)
        {
            try
            {
                QuickLink model = null;
                if (entity != null)
                {
                    model = new QuickLink();
                    model.Id = entity.Quick_Link_Id;
                    model.Name = entity.Quick_Link_Name;
                    model.Description = entity.Quick_Link_Description;
                    model.Url = entity.Quick_Link_Url;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override QUICK_LINK TranslateToEntity(QuickLink model)
        {
            try
            {
                QUICK_LINK entity = null;
                if (model != null)
                {
                    entity = new QUICK_LINK();
                    entity.Quick_Link_Id = model.Id;
                    entity.Quick_Link_Name = model.Name;
                    entity.Quick_Link_Description = model.Description;
                    entity.Quick_Link_Url = model.Url;
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
