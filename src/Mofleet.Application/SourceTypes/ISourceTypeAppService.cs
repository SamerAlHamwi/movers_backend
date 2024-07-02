using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.SourceTypes.Dto;

namespace Mofleet.SourceTypes
{
    public interface ISourceTypeAppService : IMofleetAsyncCrudAppService<SourceTypeDetailsDto, int, LiteSourceTypeDto, PagedSourceTypeResultRequestDto,
        CreateSourceTypeDto, UpdateSourceTypeDto>
    {
    }
}
