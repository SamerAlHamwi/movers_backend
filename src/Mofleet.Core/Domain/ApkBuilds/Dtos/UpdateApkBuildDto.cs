using Abp.Application.Services.Dto;

namespace Mofleet.Domain.ApkBuilds.Dtos
{
    public class UpdateApkBuildDto : CreateApkBuildDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
