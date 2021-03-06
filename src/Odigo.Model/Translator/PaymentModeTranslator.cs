﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class PaymentModeTranslator : BaseTranslator<PaymentMode, PAYMENT_MODE>
    {
        public override PaymentMode TranslateToModel(PAYMENT_MODE entity)
        {
            try
            {
                PaymentMode model = null;
                if (entity != null)
                {
                    model = new PaymentMode();
                    model.Id = entity.Payment_Mode_Id;
                    model.Name = entity.Payment_Mode_Name;
                    model.Description = entity.Payment_Mode_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PAYMENT_MODE TranslateToEntity(PaymentMode model)
        {
            try
            {
                PAYMENT_MODE entity = null;
                if (model != null)
                {
                    entity = new PAYMENT_MODE();
                    entity.Payment_Mode_Id = model.Id;
                    entity.Payment_Mode_Name = model.Name;
                    entity.Payment_Mode_Description = model.Description;
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
