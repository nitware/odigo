using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class PaymentTranslator : BaseTranslator<Payment, PAYMENT>
    {
        private PersonTranslator _personTranslator;
        private ServiceChargeTranslator _serviceChargeTranslator;
        private PaymentModeTranslator _paymentModeTranslator;

        public PaymentTranslator()
        {
            _personTranslator = new PersonTranslator();
            _serviceChargeTranslator = new ServiceChargeTranslator();
            _paymentModeTranslator = new PaymentModeTranslator();
        }

        public override Payment TranslateToModel(PAYMENT entity)
        {
            try
            {
                Payment model = null;
                if (entity != null)
                {
                    model = new Payment();
                    model.Id = entity.Payment_Id;
                    model.Person = _personTranslator.Translate(entity.PERSON);
                    model.Mode = _paymentModeTranslator.Translate(entity.PAYMENT_MODE);
                    model.ServiceCharge = _serviceChargeTranslator.Translate(entity.SERVICE_CHARGE);
                    model.SerialNumber = entity.Serial_Number;
                    model.InvoiceNumber = entity.Invoice_Number;
                    model.DateEntered = entity.Date_Entered;
                    model.Paid = entity.Paid;
                    model.DatePaid = entity.Date_Paid;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PAYMENT TranslateToEntity(Payment model)
        {
            try
            {
                PAYMENT entity = null;
                if (model != null)
                {
                    entity = new PAYMENT();
                    entity.Payment_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Payment_Mode_Id = model.Mode.Id;
                    entity.Service_Charge_Id = model.ServiceCharge.Id;
                    entity.Serial_Number = model.SerialNumber;
                    entity.Invoice_Number = model.InvoiceNumber;
                    entity.Date_Entered = model.DateEntered;
                    entity.Paid = model.Paid;
                    entity.Date_Paid = model.DatePaid;
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
