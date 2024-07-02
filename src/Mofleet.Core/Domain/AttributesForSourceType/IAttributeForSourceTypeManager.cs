using Abp.Domain.Services;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.AttributesForSourceType
{
    public interface IAttributeForSourceTypeManager : IDomainService
    {
        Task<AttributeForSourceType> GetEntityByIdAsync(int Id);
        Task SoftDeleteForEntityTranslation(List<AttributeForSourceTypeTranslation> translations);
        Task<bool> CheckAttributeForSourceTypeIsCorrect(List<CreateAttributeForSourceTypeValueDto> AttributeForSourceTypeValues);
        Task CheckIfAttribteChoiceHasPoints(int sourceTypeId);

    }
}
