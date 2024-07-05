using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.CompanyBranches.Dto;

namespace Mofleet.CompanyBranches
{
    public interface ICompanyBranchAppService : IMofleetAsyncCrudAppService<CompanyBranchDetailsDto, int, LiteCompanyBranchDto,
        PagedCompanyBranchResultRequestDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>
    {
    }
}
