using Abp.Domain.Services;
using Mofleet.Domain.RequestForQuotations.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.AttributeAndAttachments
{
    public interface IAttributeChoiceAndAttachmentManager : IDomainService
    {
        Task<List<AttributeChoiceAndAttachmentDetailsDto>> GetAttributeChoiceAndAttachmentDetailsAsyncByRequestId(long requestId);
    }
}
