using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }
        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.From = new(_configuration["Mail:UserName"], "AD Eticaret", Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
            smtp.Port = Int32.Parse(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br/>Eğer yeni şifre talebinde bulunduysanız aşağıdaki link üzerinden işlem sağlayabilirsiniz.<br/> <strong><a target=\"_blank\"href=\"...../userId/resetToken");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.Append(resetToken);
            mail.AppendLine("\"> Yeni şifre talebi için tıklayınız...</a></strong>");

            await SendMailAsync(to,"Mail Yenileme Talebi",mail.ToString());

        }
    }

    //public class AuthService
    //{
    //    ITokenHandler _tokenHandler;

    //    public AuthService(ITokenHandler tokenHandler)
    //    {
    //        _tokenHandler = tokenHandler;
    //    }

    //    public Task PasswordResetAsync(string email)
    //    {
    //      _tokenHandler.CreateAccessToken(5);
    //    }
    //}
}
