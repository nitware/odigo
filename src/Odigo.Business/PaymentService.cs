using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Entities;
using System.Transactions;
using Odigo.Utility;

namespace Odigo.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository _da;
        private readonly IService _serviceCharge;

        public PaymentService(IRepository da, IService serviceCharge)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (serviceCharge == null)
            {
                throw new ArgumentNullException("serviceCharge");
            }

            _da = da;
            _serviceCharge = serviceCharge;
        }

        public PaymentSlip GetPaymentSlipBy(Person person)
        {
            try
            {
                if (person == null || person.Id <= 0 || person.Type == null || person.Type.Id <= 0)
                {
                    throw new ArgumentNullException("person");
                }

                ServiceCharge serviceCharge = _serviceCharge.GetChargesBy(person.Type);
                if (serviceCharge == null || serviceCharge.Id <= 0)
                {
                    throw new Exception("Service Charge retreival failed! Please try again");
                }

                Payment payment = _da.GetModelBy<Payment, PAYMENT>(p => p.Person_Id == person.Id && p.Service_Charge_Id == serviceCharge.Id);
                PaymentSlip slip = GenerateSlip(payment);

                return slip;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public PaymentSlip Pay(Payment payment)
        {
            try
            {
                if (payment == null || payment.Person == null || payment.Person.Id <= 0 || payment.Person.Type == null || payment.Person.Type.Id <= 0)
                {
                    throw new ArgumentNullException("payment or payment.Person or payment.Person.Type");
                }

                PaymentSlip slip = null;
                using (TransactionScope transaction = new TransactionScope())
                {
                    //payment.ServiceCharge = _serviceCharge.GetChargesBy(payment.Person.Type);

                    Payment newPayment = Create(payment);
                    if (newPayment == null || newPayment.Id <= 0)
                    {
                        throw new Exception("Payment operation failed! Please try again. But contact your system administrator after three unsuccessful trials.");
                    }

                    slip = GenerateSlip(payment);
                    
                    //PayHelper(payment);
                    //invoice = GenerateInvoice(payment);
                    
                    transaction.Complete();
                }

                return slip;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Payment Create(Payment payment)
        {
            try
            {
                payment.SerialNumber = 0;
                Payment newPayment = _da.Create(payment);
                if (newPayment == null || newPayment.Id <= 0)
                {
                    //throw new Exception("Payment operation failed! " + CONTACT_ADMIN);
                    throw new Exception("Payment operation failed! ");
                }

                payment.Id = newPayment.Id;
                payment = SetInvoiceNumber(payment);

                return payment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Payment SetInvoiceNumber(Payment payment)
        {
            const int MAX_RETRY = 10;

            for (int i = 1; i <= MAX_RETRY; i++)
            {
                try
                {
                    payment = SetNextInvoiceNumber(payment);
                    SetInvoiceNumberHelper(payment);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        if (i == MAX_RETRY)
                        {
                            throw new Exception("Violation of UNIQUE KEY constraint. Cannot insert duplicate key in object. The statement has been terminated. Please try again.");
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return payment;
        }

        private Payment SetNextInvoiceNumber(Payment payment)
        {
            try
            {
                long maximumSerialNo = _da.GetMaxValueBy<PAYMENT>(p => (long)p.Serial_Number);
                if (maximumSerialNo > 0)
                {
                    maximumSerialNo = ++maximumSerialNo;
                }
                else
                {
                    maximumSerialNo = 1;
                }

                payment.SerialNumber = maximumSerialNo;
                payment.InvoiceNumber = "OD" + DateTime.Now.ToString("yy") + SysUtil.PaddNumber(maximumSerialNo, 16);

                return payment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool SetInvoiceNumberHelper(Payment payment)
        {
            try
            {
                PAYMENT entity = _da.GetSingleBy<PAYMENT>(p => p.Payment_Id == payment.Id);
                if (entity == null)
                {
                    throw new Exception("NoItemFound");
                }

                entity.Serial_Number = payment.SerialNumber;
                entity.Invoice_Number = payment.InvoiceNumber;

                return _da.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PaymentSlip GenerateSlip(Payment payment)
        {
            try
            {
                PaymentSlip slip = new PaymentSlip();
                slip.Payment = payment;



                //slip.Number = payment.InvoiceNumber;
                //slip.IssuerName = payment.School.Name;
                //slip.IssuerLogoUrl = payment.School.LogoUrl;
                //slip.IssuerEmail = payment.School.Email;

                //slip.RecipientName = payment.Person.FullName;
                //slip.RecipientAddress = payment.Person.ContactAddress;
                //slip.RecipientPhone = payment.Person.MobilePhone;
                //slip.RecipientEmail = payment.Person.Email;

                //slip.Channel = payment.Channel.Name;
                //slip.Date = payment.DatePaid;
                //slip.Paid = payment.Paid;

                //InvoiceItem item = new InvoiceItem();
                //item.Item = payment.FeeType.Name;
                //item.UnitCost = GetUnitCost(payment.FeeType);
                //item.TotalCost = item.UnitCost;
                //item.Quantity = 1;

                //slip.Items = new List<InvoiceItem>();
                //slip.Items.Add(item);

                return slip;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }


}
