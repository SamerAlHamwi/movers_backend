using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.RejectReasons.Dto;

namespace Mofleet.RejectReasons
{
    public interface IRejectReasonAppService : IMofleetAsyncCrudAppService<RejectReasonDetailsDto, int, LiteRejectReasonDto, PagedRejectReasonResultRequestDto, CreateRejectReasonDto, UpdateRejectReasonDto>
    {
    }
}
