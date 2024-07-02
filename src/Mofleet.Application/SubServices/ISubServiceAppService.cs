using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.SubServices.Dto;

namespace Mofleet.AttributesForSourceType
{
    public interface ISubServiceAppService : IMofleetAsyncCrudAppService<SubServiceDetailsDto, int, LiteSubServiceDto
        , PagedSubServiceResultRequestDto, CreateSubServiceDto, UpdateSubServiceDto>
    {
    }
}
