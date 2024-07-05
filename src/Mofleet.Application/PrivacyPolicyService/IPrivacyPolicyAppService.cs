using Mofleet.CrudAppServiceBase;
using Mofleet.PrivacyPolicyService.Dto;

namespace Mofleet.PrivacyPolicyService
{
    public interface IPrivacyPolicyAppService : IMofleetAsyncCrudAppService<PrivacyPolicyDetailsDto, int, LitePrivacyPolicyDto, PagedPrivacyPolicyResultRequestDto,
         CreatePrivacyPolicyDto, UpdatePrivacyPolicyDto>
    {
    }
}
