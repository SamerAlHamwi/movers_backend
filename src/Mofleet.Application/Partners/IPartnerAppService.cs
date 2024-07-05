using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Partners.Dto;

namespace Mofleet.Partners
{
    public interface IPartnerAppService : IMofleetAsyncCrudAppService<PartnerDetailsDto, int, LitePartnerDto, PagedPartnerResultRequestDto,
         CreatePartnerDto, UpdatePartnerDto>
    {

    }
}
