using Abp.Domain.Services;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.TimeWorks.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.CompanyBranches
{
    public interface ICompanyBranchManager : IDomainService
    {
        Task<CompanyBranch> GetSuperLiteEntityByIdAsync(int companyBranchId);
        Task<CompanyBranch> GetEntityByIdAsync(int companyBranchId);
        Task<List<CompanyBranch>> CheckAndGetCompanyBranch(List<int> companyBranchIds);
        Task<List<CompanyBranch>> GetCompanyBranchesByCompanyId(int companyId);
        Task<List<CompanyBranchDetailsDto>> GetCompanyBranchesDtoByCompanyId(int companyId);
        Task<CompanyBranchDetailsDto> GetCompanyBranchDtoById(int companyBranchId);
        Task<int> GetCompnayBranchIdByUserId(long userId);
        Task<List<int>> GetCompanyBranchIdsThatContainsSameCitiesInRequest(long requestId);
        Task<bool> UpdateCitiesForCompanyBranchAsync(List<int> citiesIds, CompanyBranch companyBranch);
        Task<bool> CheckIfCompanyBrachExict(int companyBranchId);
        Task<GeneralRatingDto> GetGeneralRatingDtoForComapnyBranch(int companyBranchId);
        Task<List<ReviewDetailsDto>> GetReviewsForCompanyBranch(int companyBranchId);
        Task GiftPointToCompanyBranch(int companyBranchId, int sourceTypeId, List<int> choiceIds);
        Task<CompanyBranch> GetCompanyBranchEntityById(int companyBranchId);
        Task GetPointFromCompanyBranchForGettingContactRequest(int companyBranchId, int pointsToGetRequest);
        Task AddPaidPointsToCompanyBrnach(int numberOfPoint, int companyBranchId);
        Task<int> GetCompanyBranchesCount();
        Task<CompanyBranch> GetLiteEntityByIdAsync(int companyBranchId);
        Task<List<int>> FilterCompanyBranchIdsThatOnlyAcceptPossibleRequest(List<int> companyBranchIds);
        Task CheckIfCompanyBranchIsFeature(int companyBranchId);
        Task MakeCompanyBranchAsFeature(int numberInMonths, int companyBranchId);
        Task<int> GetcompanyIdByCompanyBranchIdAsync(int companyBranchId);
        Task MakeAllCompanyBranchesNotFeatureIfTimeEndedAsync();
        Task CheckIfCompanyBranchHasTimeWorkThenDeleteIt(int companyBranchId);
        Task InsertNewTimeWorksForCompanyBranch(List<TimeWork> timeWorks);
        Task<List<TimeOfWorkDto>> GetTimeWorksDtoForCompanyBranch(int companyBranchId);
        Task<CompanyBranch> GetCompanyBranchByUserIdAsync(long userId);
        Task UpdateBranchAsync(CompanyBranch companyBranch);
        Task<bool> CheckIfCompanyHasBranches(int companyId);

    }
}
