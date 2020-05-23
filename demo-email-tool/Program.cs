using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace demo_email_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            //declarations
            var _username = System.Configuration.ConfigurationSettings.AppSettings["username"];
            var _password = System.Configuration.ConfigurationSettings.AppSettings["password"];
            const string _gmailSmtpServer = "smtp.gmail.com";
            const int _port = 587;
            var EmailDisplay = "AUTO:";
            List<string> recipients = new List<string>();

            MailAddress fromAddress = new MailAddress(_username, EmailDisplay, Encoding.UTF8);
            recipients.Add("indaleenq@gmail.com");

            //send the email
            using (var client = new SmtpClient(_gmailSmtpServer, _port))
            {
                var credential = new NetworkCredential(_username, _password);
                client.Credentials = credential;
                client.EnableSsl = true;

                using (var email = new MailMessage())
                {
                    recipients.ForEach(x => email.To.Add(x));
                    email.From = fromAddress;
                    email.IsBodyHtml = true;
                    email.Body = "<h1> Hi, this is an auto-email </h1>";
                    email.BodyEncoding = Encoding.UTF8;
                    email.Subject = "Auto-email alert";
                    email.SubjectEncoding = Encoding.UTF8;
                    if (email.To.Any())
                    {
                        try
                        {
                            client.Send(email);
                            Console.WriteLine("Successfully sent email., check the inbox..");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong...");
                        }
                    }
                }
            }
        }
    }
}

