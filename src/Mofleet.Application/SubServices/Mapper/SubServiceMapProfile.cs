using AutoMapper;
using Mofleet.Domain.SubServices;
using Mofleet.Domain.SubServices.Dto;

namespace Mofleet.SubServices.Mapper
{
    public class SubServiceMapProfile : Profile
    {
        public SubServiceMapProfile()
        {
            CreateMap<CreateSubServiceDto, SubService>();
        }
    }
}
