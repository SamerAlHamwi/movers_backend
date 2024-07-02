using Abp.Domain.Services;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using Mofleet.Domain.AttributeForSourcTypeValues;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.AttributeForSourceTypeValues
{
    public interface IAttributeForSourceTypeValueManager : IDomainService
    {
        Task<List<AttributeForSourceTypeValueDto>> GetAllAttributeForSourceTypeValuesByRequestForQuotationId(long quotationId);
        Task HardDeleteEntity(List<AttributeForSourceTypeValue> attributeForSourceTypeValues);
    }
}
