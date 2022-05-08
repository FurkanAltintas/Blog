using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Blog.Services.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public IResult Send(EmailSendDto emailSendDto)
        {
            MailMessage message = new()
            {
                From = new(_smtpSettings.SenderEmail),
                To = { new MailAddress(emailSendDto.Email) },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true, // Html standartlarında gelmesi için yapıyoruz
                Body = emailSendDto.Message
            };

            SmtpClient smtpClient = new()
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtpClient.Send(message);

            return new Result(ResultStatus.Success, "E-Postanız başarıyla gönderilmiştir.");
        }

        public IResult SendContactEmail(EmailSendDto emailSendDto)
        {
            MailMessage message = new()
            {
                From = new(_smtpSettings.SenderEmail),
                To = { new MailAddress("furkanaltintas785@gmail.com") },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true, // Html standartlarında gelmesi için yapıyoruz
                Body = $"Gönderen Kişi: {emailSendDto.Name}, Gönderen E-Posta Adresi: {emailSendDto.Email} </br> {emailSendDto.Message}"
            };

            SmtpClient smtpClient = new()
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtpClient.Send(message);

            return new Result(ResultStatus.Success, "E-Postanız başarıyla gönderilmiştir.");
        }
    }
}