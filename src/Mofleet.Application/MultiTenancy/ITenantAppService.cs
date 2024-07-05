using Abp.Application.Services;
using Mofleet.MultiTenancy.Dto;

namespace Mofleet.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

