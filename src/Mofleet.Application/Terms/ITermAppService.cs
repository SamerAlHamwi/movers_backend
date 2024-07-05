using Mofleet.CrudAppServiceBase;
using Mofleet.TermService.Dto;

namespace Mofleet.TermService
{
    public interface ITermAppService : IMofleetAsyncCrudAppService<TermDetailsDto, int, LiteTermDto, PagedTermResultRequestDto,
         CreateTermDto, UpdateTermDto>
    {
    }
}
