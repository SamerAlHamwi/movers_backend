using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.AttributeChoices.Dto;

namespace Mofleet.AttributeChoices
{
    public interface IAttributeChoiceAppService : IMofleetAsyncCrudAppService<AttributeChoiceDetailsDto, int, LiteAttributeChoiceDto, PagedAttributeChoiceResultRequestDto,
        CreateAttributeChoiceDto, UpdateAttributeChoiceDto>
    {
        //Task<AttributeChoiceDetailsDto> CreateAttributeChoiceWithAttributeIdAsync(CreateAttributeChoiceWithAttributeIdDto input); 
    }
}
