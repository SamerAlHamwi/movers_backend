using AutoMapper;
using Mofleet.Domain.ApkBuilds;
using Mofleet.Domain.ApkBuilds.Dtos;

namespace Mofleet.ApkBuildAppService.Mapper
{
    public class ApkBuildMapProfile : Profile
    {
        public ApkBuildMapProfile()
        {
            CreateMap<CreateApkBuildDto, ApkBuild>();
            CreateMap<ApkBuild, LiteApkBuildDto>();
            CreateMap<ApkBuild, ApkBuildDetailsDto>();
            CreateMap<UpdateApkBuildDto, ApkBuild>();
        }
    }
}
