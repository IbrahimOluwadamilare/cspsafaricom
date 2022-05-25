using cspv3.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public class GmailService : IGmailSender
    {
        SmtpClient SmtpServer;
        string MailerResponse;
        private EmailSettings emailSettings { get; set; }

        public GmailService(IOptions<EmailSettings> emailsettings)
        {

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpServer = smtpClient;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailerResponse = "";
            emailSettings = emailsettings.Value;



        }
        public string SendLinkEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress(emailSettings.FromEmail);
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }

        public string SendPlainEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress(emailSettings.FromEmail);
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }
    }
    }

