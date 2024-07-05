using Abp.Domain.Services;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.ServiceValues.Dto;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.TimeWorks.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Companies
{
    public interface ICompanyManager : IDomainService
    {
        Task<Company> GetSuperLiteEntityByIdAsync(int id);
        Task<Company> GetLiteEntityByIdAsync(int id);
        Task<Company> GetLiteCompanyByIdAsync(int id);
        Task<List<CompanyBranch>> CheckAndGetCompanyBranch(List<int> companyBranchIds);
        Task<bool> CheckIfCompanyBrachExict(int companyBranchId);
        Task<CompanyContactDetailsDto> GetCompanyContactDtoByCompanyId(int companyId);
        Task<string> CreateCodeForCompany();
        Task<List<Company>> GetListOfCompany(List<int> companyIds);
        Task<bool> CheckIfUserHasCompany(long userId);
        Task<bool> CheckIfCompanyExict(int companyId);
        Task<Company> GetEntityByIdAsync(int id);
        Task HardDeleteCompanyTranslation(List<CompanyTranslation> translations);
        //Task UpdateContcatCompany(CompanyContact companyContact, int companyId);
        Task UpdateAttachmentTypeListAsync(List<long> newAttachmentIds, AttachmentRefType attachmentType, long companyId);
        Task<CompanyAndBranchDto> GetSelectedCompanyAndBranch(CompanyAndCompanyBranchIdsDto input);
        Task<int> GetCompnayIdByUserId(long userId);
        Task<bool> UpdateCitiesByCompanyIdAsync(List<int> citiesIds, Company company);
        Task<List<int>> GetCompanyIdsThatContainsSameCitiesInRequest(long requestIds);
        Task<GeneralRatingDto> GetGeneralRatingDtoForComapny(int companyId);
        Task<CompanyStatuesDto> GetCompanyStatuesByCompanyBranchId(int companyBranchId);
        Task<List<ReviewDetailsDto>> GetReviewsForCompany(int companyId);
        Task GiftPointToCompany(int companyId, int sourceTypeId, List<int> choiceIds);
        Task<Company> GetCompanyEntityById(int companyId);
        Task GetPointFromCompanyForGettingContactRequest(int companyId, int pointsToGetRequest);
        Task AddPaidPointsToCompany(int numberOfPoint, int companyId);
        Task<int> GetCompaniesCount();
        Task<List<int>> FilterCompanyIdsThatOnlyAcceptPossibleRequest(List<int> companyIds);
        Task CheckIfCompanyIsFeature(int companyId);
        Task MakeCompanyAsFeature(int numberInMonths, int companyId);
        Task MakeAllCompaniesNotFeatureIfTimeEndedAsync();
        Task CheckIfCompanyHasTimeWorkThenDeleteIt(int companyId);
        Task InsertNewTimeWorksForCompany(List<TimeWork> timeWorksToInsert);
        Task<List<TimeOfWorkDto>> GetTimeWorksDtoForCompany(int companyId);
        Task<long> GetUserIdByCompanyIdAsync(int companyId);
        Task DeleteUpdatedCompanyInstance(int companyId);
        Task<CompanyStatuesDto> GetChildCompanyStatuesIfFound(int companyId);
        Task DeleteUserForCompanyOrBranchAsync(long userId);

    }
}
