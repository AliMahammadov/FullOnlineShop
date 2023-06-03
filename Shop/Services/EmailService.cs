using Shop.Abstraction.Services;
using System.Net;
using System.Net.Mail;

namespace Shop.Services
{
    public class EmailService:IEmailService
    {
        IConfiguration _configuaration { get; }
        public EmailService(IConfiguration configuaration) 
        {
            _configuaration = configuaration;
        }

      
       
        public  void Send(string mailTo,string subject, string body, bool isBodyHtml=false)
        {
            SmtpClient smtp = new SmtpClient(_configuaration["Email:Host"],//-
                Convert.ToInt32(_configuaration["Email:Port"]));//-
            smtp.EnableSsl= true;//-                                                       ne uzerinden gonderilecek
            smtp.Credentials = new NetworkCredential(_configuaration["Email:Login"],//-
                _configuaration["Email:Password"]); //bu emaili kim gonderecek onu tanitmaq ucun
            MailAddress from = new MailAddress(_configuaration["Email:Login"], "Facebook"); //kim gonderir
            MailAddress to = new MailAddress(mailTo/*"e.mehemmedov99@gmail.com"*/); //kime gonderir
            MailMessage message = new MailMessage(  from,to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml= isBodyHtml;
            smtp.Send(message);
        }
    }
}