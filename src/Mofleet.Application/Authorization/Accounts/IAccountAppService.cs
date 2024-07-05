using Abp.Application.Services;
using Mofleet.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace Mofleet.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        Task<dynamic> SignUpWithPhoneNumberAsync(SignInWithPhoneNumberInputDto input);

        Task<dynamic> VerifySignUpWithPhoneNumberAsync(VerifySignUpWithPhoneNumberInputDto input);

        Task<dynamic> SignInWithPhoneNumberAsync(SignInWithPhoneNumberInputDto input);
        Task<dynamic> ResendVerificationCodeAsync(VerifiyPhoneNumberInputDto input);
        Task DeleteAccount();
    }
}
