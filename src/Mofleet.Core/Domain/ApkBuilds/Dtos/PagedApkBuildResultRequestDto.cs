using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.ApkBuilds.Dtos
{
    public class PagedApkBuildResultRequestDto : PagedResultRequestDto
    {
        public AppType? AppType { get; set; }
        public SystemType? SystemType { get; set; }
        public UpdateOptions? UpdateOptions { get; set; }

    }
}
