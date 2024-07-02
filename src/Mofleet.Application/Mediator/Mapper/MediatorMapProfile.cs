using AutoMapper;
using Mofleet.Domain.Mediators;
using Mofleet.Domain.Mediators.Dto;
using Mofleet.Mediators.Dto;

namespace Mofleet.Mediators.Mapper
{
    public class MediatorMapProfile : Profile
    {
        public MediatorMapProfile()
        {
            CreateMap<MediatorDetailsDto, Mediator>();
            CreateMap<Mediator, MediatorDetailsDto>();
            CreateMap<CreateMediatorDto, Mediator>();
            CreateMap<Mediator, CreateMediatorDto>();
            CreateMap<Mediator, UpdateMediatorDto>();
            CreateMap<UpdateMediatorDto, Mediator>();


        }
    }
}
