using Abp.Domain.Services;
using Mofleet.Domain.Cities.Dto;
using System;
using System.Threading.Tasks;

namespace Mofleet.Domain.Reviews
{
    public interface IReviewManager : IDomainService
    {
        Task InserReviewToCompanyOrCompanyBranch(Review review);
        Task<OutPutBooleanStatuesDto> CheckIfUserRateOfferOrNot(long userId, Guid offerId);
    }
}
