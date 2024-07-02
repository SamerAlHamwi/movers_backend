using Abp.Application.Services;
using System.Threading.Tasks;

namespace Mofleet.SmsSender
{
    public interface ISmsSenderAppService : IApplicationService
    {
        Task<dynamic> SendOTPVerificationSMS(string to, string channel = "sms");
        Task<dynamic> VerificationCheckOTP(string code, string to);
    }
}
