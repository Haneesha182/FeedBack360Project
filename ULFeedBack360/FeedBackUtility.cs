using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ULFeedBack360
{
        public class FeedbackUtility
        {
            private static FeedbackUtility obj;
            public static FeedbackUtility getInstant()
            {
                //FeedbackUtility obj;
                if (obj is null)
                {
                    obj = new FeedbackUtility();
                }
                return obj;
            }


            public string SendEmail(string from, string to, string subject, string msgBody)
            {
                string result = string.Empty;
                try
                {

                    MailMessage message = new MailMessage(from, to);   // From address has to be GMAIL only             
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = msgBody;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";           // This is free SMTP given by gmail
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("haneesha0809@gmail.com", "usio gyfq vwyc mxfy");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;                        // 587 is for free account

                    smtp.Send(message);
                    result = "Success";
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
                return result;
            }

        }
    }
