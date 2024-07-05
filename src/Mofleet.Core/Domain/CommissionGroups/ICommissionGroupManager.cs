using Abp.Domain.Services;
using Mofleet.Domain.CommissionGroups.Dtos;
using Mofleet.Domain.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.CommissionGroups
{
    public interface ICommissionGroupManager : IDomainService
    {
        Task<bool> CheckIfGroupExistAsync(int groupId);
        Task<CommissionGroup> GetCommissionGroupAsync(int groupId);
        Task<bool> CheckIfGroupContainCompanyAsync(int groupId, int companyId);
        Task CheckIfNameWasExisted(double name);
        Task AddNewCompanyToDefaultGroup(int companyId);
        Task<CommissionGroupDto> GetCommissionByCompanyIdAsync(int companyId);
        Task<List<CommissionGroupWithCompanyIdsDto>> GetCommissionGroupByCompanyIds(List<int> companyIds);
        Task<int> GetCurrentCommissionGroupIdByCompanyId(int companyId);
        Task ReAddCompanyInToOwnsGroup(Company company, int groupId);
        Task RemoveCompanyFromCommission(Company company);
    }
}
