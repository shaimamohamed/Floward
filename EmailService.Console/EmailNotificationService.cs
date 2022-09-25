using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace EmailService.Console
{
    public class EmailNotificationService
    {
        public static bool SendEmail(string emailBody) {

            #region Get Default EmailSetting ..
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            string defaultFrom = config.GetSection("Email:DefaultFrom").Value;
            string defaultto = config.GetSection("Email:DefaultTo").Value;
            string defaultSubject = config.GetSection("Email:DefaultSubject").Value;
            string smtpHost = config.GetSection("Email:SmtpHost").Value;
            string smtpPort = config.GetSection("Email:SmtpPort").Value;
            string smtpUserName = config.GetSection("Email:SmtpUserName").Value;
            string smtpPassWord = config.GetSection("Email:SmtpPassWord").Value;

            // Prepare Email body..

            //string emailBody = "this prouct has been added ..";

            #endregion

            #region Send Email Notification

            var client = new SmtpClient(smtpHost,Convert.ToInt32(smtpPort))
            {
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(smtpUserName, smtpPassWord),
                EnableSsl = false
            };
            client.Send(defaultFrom, defaultto, defaultSubject, emailBody);
            System.Console.WriteLine("Email Sent");

            #endregion

            return true;
        }
    }
}
