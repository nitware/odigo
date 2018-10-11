using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using Odigo.Model.Model;
using Odigo.Notification.Interfaces;
using System.Net;

namespace Odigo.Notification
{
    public class EmailEngine : INotificationProvider<Email, bool>
    {
        private readonly string _smtpServer;
        private readonly string _adminEmail;

        public EmailEngine(string smtpServer, string adminEmail)
        {
            if (string.IsNullOrWhiteSpace(smtpServer))
            {
                throw new ArgumentNullException("SmtpServer");
            }
            if (string.IsNullOrWhiteSpace(adminEmail))
            {
                throw new ArgumentNullException("AdminEmail");
            }

            _smtpServer = smtpServer;
            _adminEmail = adminEmail;
        }

        public bool Send(Email mail)
        {
            try
            {
                string msg = "";
                
                //constant variable declaration
                const string DIV = "<DIV>";
                const string END_DIV = "</DIV>";
                const string PARAG = "<P>";
                const string END_PARAG = "</P>";
                const string NEW_LINE = "<BR>";
                const string BOLD = "<B>";
                const string END_BOLD = "</B>";
                const string FONT_FACE = "<FONT FACE = ";
                const string END_FONT = "</FONT>";
                const string FONT_SIZE = "<FONT SIZE = ";

                //build font face for the mail
                msg = FONT_SIZE + 2;

                //msg = FONT_FACE & "'Verdana'"
                //msg += FONT_FACE + "'Tahoma'";
                msg += FONT_FACE + "'Century Gothic'";

                //build date part of the mail
                msg += DIV + mail.MailDate + END_DIV;

                //build the address of the addressee mail part
                msg += DIV + PARAG + mail.Addressee + END_PARAG + END_DIV;

                //build the address of the addressee phone part
                msg += DIV + PARAG + mail.Phone + END_PARAG + END_DIV + NEW_LINE;

                //build salutation mail part
                msg += DIV + mail.Salutation + END_DIV + NEW_LINE;

                //build mail title part
                if (mail.MailTitle != null)
                {
                    msg += DIV + BOLD + mail.MailTitle + END_BOLD + END_DIV;
                }

                //build body of the mail
                msg += DIV + mail.MailBody + END_DIV;

                //build mail closing part
                //msg += DIV + PARAG + "Thank you," + NEW_LINE + "Yours Faithfully" + END_PARAG;
                msg += DIV + PARAG + NEW_LINE + "Regards," + NEW_LINE + END_PARAG;
                msg += PARAG + mail.NameFrom + END_PARAG + END_DIV;

                //end font face
                msg += END_FONT;
                msg += END_FONT;

                //message.Cc = adminEmail;
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(mail.MailTo));
                email.From = new MailAddress(_adminEmail);
                email.Priority = MailPriority.High;
                email.Subject = mail.Subject;
                email.IsBodyHtml = true;
                email.Body = msg;

                //configure smtp server
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"C:\Users\Dan\Documents\Visual Studio 2015\Projects\Odigo\Odigo.Web\Content\mail";
                smtp.Host = _smtpServer;
                smtp.Port = 25;


                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                ////smtp.Port = 465;
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential("egenti.daniel@gmail.com", "$abundance$");

                //smtp.Host = "smtp.mail.yahoo.co.uk";
                //smtp.Port = 465;
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("linkdanex@yahoo.co.uk", "@C0nnect@");

                //smtp.Host = "smtp.o2.ie";
                //smtp.Port = 25;
                //smtp.UseDefaultCredentials = true;


                smtp.Send(email);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }




}
