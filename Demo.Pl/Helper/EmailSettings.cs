using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Demo.Pl.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("yusufkamal2030@gmail.com", "joqdccvkypbrvlqy");
            client.Send("yusufkamal2030@gmail.com", email.To, email.Title, email.Body);
           
        }
    }
}
