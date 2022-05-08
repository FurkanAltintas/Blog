using Blog.Entities.Dtos;
using Blog.Shared.Utilities.Results.Abstract;

namespace Blog.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);

        IResult SendContactEmail(EmailSendDto emailSendDto);
    }
}