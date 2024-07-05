using Abp.Domain.Services;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.RequestForQuotationContacts
{
    public interface IRequestForQuotationContactManager : IDomainService
    {
        Task<List<RequestForQuotationContactDto>> GetAllContactsByRequestForQuotationId(long requestId);
    }

}
