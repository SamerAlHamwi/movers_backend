using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Drafts.Dtos;

namespace Mofleet.Drafts
{
    public interface IDraftAppService : IMofleetAsyncCrudAppService<DraftDetailsDto, int, LiteDraftDto, PagedDraftResultRequestDto,
        CreateDraftDto, UpdateDraftDto>
    {
    }
}
