using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.CommissionGroups.Dtos;

namespace Mofleet.CommissionGroups
{
    public interface ICommissionGroupAppService : IMofleetAsyncCrudAppService<CommissionGroupDetailsDto, int, LiteCommissionGroupDto, PagedCommissionGroupResultRequestDto,
        CreateCommissionGroupDto, UpdateCommissionGroupDto>
    {
    }
}
