using System.Net;
using System.Net.Mail;

namespace dotnet_basic.Services;

public interface IEmailService
{
    // Email => Hangi mail adresine gönderileceği
    // Subject => Konu başlığı
    // Message => Mail içeriği
    Task SendEmailAsync(string email, string subject, string message);
}



public class SmtpEmailServis : IEmailService
{
    private IConfiguration _configuration;

    public SmtpEmailServis(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {

        using (var client = new SmtpClient(_configuration["Email:Host"]))
        {
            // kendi ayarlarımızı girmek için default ayarları kapatıyoruz.
            client.UseDefaultCredentials = false;
            // kendi ayarlarımız
            client.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);

            // gmail için
            client.Port = 587;
            client.EnableSsl = true;
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:Username"]!),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            await client.SendMailAsync(mailMessage);
        }
    }
}



