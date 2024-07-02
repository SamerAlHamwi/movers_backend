using Abp.Application.Services;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.EmailSender
{
    public interface IEmailSenderAppService : IApplicationService
    {
        Task SendEmail(string email, string Code, ConfirmationCodeType codeType, bool usingWithNotification = false);
    }
}
