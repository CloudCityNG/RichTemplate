using System;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.IO;
/// <summary>
/// This function contain method for getting HTML from a page 
/// and also contains functions for sending mail.
/// </summary>
public class Mail
{
    public static void SendEmail(string To, string From, string Subject, string Contents)
    {
        try
        {
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["smptUser"].ToString(),ConfigurationSettings.AppSettings["smptPass"].ToString());
            SmtpClient MailObj = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(From);
            message.From = fromAddress;
            message.To.Add(To);
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = Contents;
            MailObj.Host = ConfigurationSettings.AppSettings["smptAddress"].ToString();
            MailObj.Credentials = SMTPUserInfo;
            MailObj.Send(message);

	  

        }
        catch (Exception ex)
        {
           
        }
    }
}
