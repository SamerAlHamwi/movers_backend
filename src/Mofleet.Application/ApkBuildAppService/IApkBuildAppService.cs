using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.ApkBuilds.Dtos;

namespace Mofleet.ApkBuildAppService
{
    public interface IApkBuildAppService : IMofleetAsyncCrudAppService<ApkBuildDetailsDto, int, LiteApkBuildDto, PagedApkBuildResultRequestDto,
        CreateApkBuildDto, UpdateApkBuildDto>
    {
    }
}
