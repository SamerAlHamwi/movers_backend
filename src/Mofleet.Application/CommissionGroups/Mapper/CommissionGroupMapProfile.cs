using AutoMapper;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.CommissionGroups.Dtos;
using System.Linq;

namespace Mofleet.CommissionGroups.Mapper
{
    public class CommissionGroupMapProfile : Profile
    {
        public CommissionGroupMapProfile()
        {
            CreateMap<CreateCommissionGroupDto, CommissionGroup>();
            CreateMap<UpdateCommissionGroupDto, CommissionGroup>();
            CreateMap<CommissionGroup, CommissionGroupDetailsDto>();
            CreateMap<CommissionGroup, LiteCommissionGroupDto>();
            CreateMap<CommissionGroup, CommissionGroupWithCompanyIdsDto>()
                                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies.Select(x => x.Id).ToList()));

        }
    }
}
