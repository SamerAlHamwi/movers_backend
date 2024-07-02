using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Companies.Dto;

namespace Mofleet.Cities
{
    public interface ICompanyAppService : IMofleetAsyncCrudAppService<CompanyDetailsDto, int, LiteCompanyDto, PagedCompanyResultRequestDto,
        CreateCompanyDto, UpdateCompanyDto>
    {

    }
}
