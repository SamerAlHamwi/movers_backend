using AutoMapper;
using Mofleet.Domain.Offers;
using Mofleet.Domain.Offers.Dto;
using Mofleet.Domain.Reviews;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.SelectedCompaniesByUsers.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues.Dto;

namespace Mofleet.Offers.Mapper
{
    public class OfferMapProfile : Profile
    {
        public OfferMapProfile()
        {
            CreateMap<Offer, OfferDetailsDto>();
            CreateMap<ServiceValueForOfferDto, ServiceValueForOffer>();
            CreateMap<SelectedCompaniesBySystemForRequest, SelectedCompaniesBySystemForRequestDto>();
            CreateMap<CreateOfferDto, Offer>();
            CreateMap<UpdateOfferDto, Offer>();
            CreateMap<Offer, LiteOfferDto>();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<ServiceValueForOffer, ServiceValueForOfferDto>();
            CreateMap<ServiceValueForOffer, ServiceDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
