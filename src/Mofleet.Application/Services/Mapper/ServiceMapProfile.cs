using AutoMapper;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues.Dto;

namespace Mofleet.Offers.Mapper
{
    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<ServiceValueForOfferDto, ServiceValueForOffer>();
            CreateMap<ServiceDetailsDto, ServiceValuesDto>();

        }
    }
}
