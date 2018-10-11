using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using System.Transactions;
using Odigo.Notification.Interfaces;
using Odigo.Notification;

namespace Odigo.Business
{
    public class RequestService : IRequestService
    {
        private readonly IRepository _da;
        private readonly IPaymentService _paymentService;
        private readonly INotificationProvider<Email, bool> _emailEngine;
        private readonly INotificationProvider<Sms, NexmoResponse> _smsEngine;

        public RequestService(IRepository da, IPaymentService paymentService, INotificationProvider<Email, bool> emailEngine, INotificationProvider<Sms, NexmoResponse> smsEngine)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (emailEngine == null)
            {
                throw new ArgumentNullException("emailEngine");
            }
            if (paymentService == null)
            {
                throw new ArgumentNullException("paymentService");
            }
            if (smsEngine == null)
            {
                throw new ArgumentNullException("smsEngine");
            }

            _da = da;
            _smsEngine = smsEngine;
            _emailEngine = emailEngine;
            _paymentService = paymentService;
        }

        public PaymentSlip Send(Request request)
        {
            try
            {
                PaymentSlip paymentSlip = null;

                using (TransactionScope transaction = new TransactionScope())
                {
                    Request newRequest = _da.Create(request);
                    if (newRequest == null || newRequest.Id <= 0)
                    {
                        throw new Exception("Sending of request failed! Please try again");
                    }

                    foreach (RequestForEmploymentCostImplication requestForEmploymentCostImplication in request.ForEmploymentCostImplications)
                    {
                        requestForEmploymentCostImplication.Request = newRequest;
                        RequestForEmploymentCostImplication newRequestForEmploymentCostImplication = _da.Create(requestForEmploymentCostImplication);
                        if (newRequestForEmploymentCostImplication == null || newRequestForEmploymentCostImplication.Id <= 0)
                        {
                            throw new Exception("Cost implication save operation failed! Please try again");
                        }

                        foreach (RequestForEmploymentTeacherAvailability requestForEmploymentTeacherAvailability in requestForEmploymentCostImplication.TeacherAvailabilities)
                        {
                            requestForEmploymentTeacherAvailability.CostImplication = newRequestForEmploymentCostImplication;
                        }

                        int rowsAdded = _da.Create(requestForEmploymentCostImplication.TeacherAvailabilities);
                        if (rowsAdded <= 0 || rowsAdded != requestForEmploymentCostImplication.TeacherAvailabilities.Count)
                        {
                            throw new Exception("Storing Availability of teacher failed! Please try again");
                        }
                    }

                    paymentSlip = MakePaymentEntry(request);
                    //SendSMS(request.RequestMessage.Text, "2348035303653");
                    SendEmail(request);
                    
                    transaction.Complete();
                }

                return paymentSlip;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private PaymentSlip MakePaymentEntry(Request request)
        {
            try
            {
                Payment payment = new Payment();
                payment.DateEntered = request.Date;
                payment.Mode = new PaymentMode() { Id = (int)PaymentMode.List.Online };
                payment.ServiceCharge = request.ServiceCharge;
                payment.Person = request.FromPerson;
                payment.Paid = false;

                PaymentSlip paymentSlip = _paymentService.Pay(payment);
                if (paymentSlip == null || paymentSlip.Payment == null || paymentSlip.Payment.Id <= 0)
                {
                    throw new Exception("Payment entry creation failed!");
                }

                return paymentSlip;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool SendEmail(Request request)
        {
            bool sent = false;
            const string mailTitle = "EMPLOYMENT REQUEST";

            try
            {
                Email email = new Email();
                email.Addressee = request.ToPerson.ContactAddress;
                email.MailBody = request.RequestMessage.Text;
                email.MailDate = request.Date;
                email.MailTitle = mailTitle;
                email.MailTo = request.ToPerson.Email;
                email.NameFrom = "Nmutaka Team, Nitware Solutions";
                email.Salutation = "Dear " + request.ToPerson.Name;
                email.Subject = mailTitle;
                                
                sent = _emailEngine.Send(email);
            }
            catch (Exception)
            {
                //suppress the error (some email address might be incorrectly entered);
            }

            return sent;
        }

        public void SendSMS(string message, string recipientPhoneNo)
        {
            try
            {
                Sms sms = new Sms();
                sms.RecepientPhoneNo = recipientPhoneNo;
                sms.Message = message;
               
               NexmoResponse response = _smsEngine.Send(sms);
               
            }
            catch (Exception)
            {
                //suppress the error (some email address might be incorrectly entered);
            }
        }







    }


}
