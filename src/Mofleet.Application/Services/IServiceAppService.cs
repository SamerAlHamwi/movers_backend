using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.services.Dto;

namespace Mofleet.AttributesForSourceType
{
    public interface IServiceAppService : IMofleetAsyncCrudAppService<ServiceDetailsDto, int, LiteServiceDto
        , PagedServiceResultRequestDto, CreateServiceDto, UpdateServiceDto>
    {
    }
}
