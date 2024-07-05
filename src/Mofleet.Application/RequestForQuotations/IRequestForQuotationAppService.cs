using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.ServiceValues.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.RequestForQuotations
{
    public interface IRequestForQuotationAppService : IMofleetAsyncCrudAppService<RequestForQuotationDetailsDto, long, LiteRequestForQuotationDto
        , PagedRequestForQuotationResultRequestDto, CreateRequestForQuotationDto, UpdateRequestForQuotationDto>
    {
        Task<List<long>> InsertAndNoticFilteredCompanies(CompanyAndCompanyBranchIdsDto input, long requestId, bool forPossibleRequest = false);
        Task GiftCompanyThatProvideOfferBySourceTypePoints(TinySelectedCompanyDto selectedCompany, int sourceTypeId, List<int> choiceIds);
        Task GiveMediatorCommissionForFinishRequest(string code, TinySelectedCompanyDto selectedCompany, Guid offerId);
    }
}
