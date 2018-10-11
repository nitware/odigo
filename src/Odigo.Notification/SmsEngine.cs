using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Web;
using Odigo.Model.Model;
using Odigo.Notification.Interfaces;
using System.Web.Script.Serialization;

namespace Odigo.Notification
{
    public class SmsEngine : INotificationProvider<Sms, NexmoResponse>
    {

        private readonly string _baseAddress;
        private readonly string _username;
        private readonly string _password;

        //private readonly string _username = "0cd0f2f5";
        //private readonly string _password = "f7dbe54b";

        public SmsEngine(string username, string password, string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            _username = username;
            _password = password;
            _baseAddress = baseAddress;

            Sender = "Sender";
        }

        public string Sender { get; set; }

        public NexmoResponse Send(Sms sms)
        {
            //var wc = new WebClient() { BaseAddress = "http://rest.nexmo.com/sms/json" };

            try
            {
                var wc = new WebClient() { BaseAddress = _baseAddress };
                wc.QueryString.Add("api_key", HttpUtility.UrlEncode(_username));
                wc.QueryString.Add("api_secret", HttpUtility.UrlEncode(_password));
                wc.QueryString.Add("from", HttpUtility.UrlEncode(Sender));
                wc.QueryString.Add("to", HttpUtility.UrlEncode(sms.RecepientPhoneNo));
                wc.QueryString.Add("text", HttpUtility.UrlEncode(sms.Message));

                return ParseSmsResponseJson(wc.DownloadString(""));
            }
            catch(Exception)
            {
                throw;
            }
        }

        private NexmoResponse ParseSmsResponseJson(string json)
        {
            try
            {
                json = json.Replace("-", ""); // hyphens are not allowed in .NET var names
                return new JavaScriptSerializer().Deserialize<NexmoResponse>(json);
            }
            catch(Exception)
            {
                throw;
            }
        }


    }

    public class NexmoResponse
    {
        public string Messagecount { get; set; }
        public List<NexmoMessageStatus> Messages { get; set; }
    }

    public class NexmoMessageStatus
    {
        public string Network;
        public string clientRef;

        public string To { get; set; }
        public string Status { get; set; }
        public string MessageId { get; set; }
        public string ErrorText { get; set; }
        public string RemainingBalance { get; set; }
        public string MessagePrice { get; set; }
        
    }




}

