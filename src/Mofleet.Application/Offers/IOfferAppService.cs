
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Offers;
using Mofleet.Domain.Offers.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.Offers
{
    public interface IOfferAppService : IMofleetAsyncCrudAppService<OfferDetailsDto, Guid, LiteOfferDto
        , PagedOfferResultRequestDto,
        CreateOfferDto, UpdateOfferDto>
    {
        Task<bool> RejectOffersByUser(RejectOffersInputDto input);
        Task NoticOtherCompaniesForPossibleRequest(List<Offer> offers);
    }
}
