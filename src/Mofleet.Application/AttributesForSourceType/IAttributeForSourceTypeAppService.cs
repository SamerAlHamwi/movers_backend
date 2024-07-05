using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.AttributesForSourceType.Dto;

namespace Mofleet.AttributesForSourceType
{
    public interface IAttributeForSourceTypeAppService : IMofleetAsyncCrudAppService<AttributeForSourceTypeDetailsDto, int, LiteAttributeForSourceTypeDto
        , PagedAttributeForSourceTypeResultRequestDto, CreateAttributeForSourceTypeDto, UpdateAttributeForSourceTypeDto>
    {
    }
}
