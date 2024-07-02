using AutoMapper;
using Mofleet.Domain.AskForHelps;
using Mofleet.Domain.AskForHelps.Dto;
using Mofleet.Domain.AttributeAndAttachments;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using Mofleet.Domain.AttributeForSourcTypeValues;
using Mofleet.Domain.RequestForQuotationContacts;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.SearchedPlacesByUsers;
using Mofleet.Domain.SearchedPlacesByUsers.Dtos;
using Mofleet.Domain.services;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.ServiceValues.Dto;

namespace Mofleet.RequestForQuotations.Mapper
{
    public class RequestForQuotationMapProfile : Profile
    {
        public RequestForQuotationMapProfile()
        {
            CreateMap<RequestForQuotation, RequestForQuotationDetailsDto>();
            CreateMap<RequestForQuotation, RequestForQuotationDto>();
            CreateMap<RequestForQuotationDetailsDto, RequestForQuotation>();
            CreateMap<RequestForQuotationDto, RequestForQuotation>();
            CreateMap<CreateRequestForQuotationDto, RequestForQuotation>();
            CreateMap<UpdateRequestForQuotationDto, RequestForQuotation>();
            CreateMap<LiteRequestForQuotationDto, RequestForQuotation>();
            CreateMap<RequestForQuotation, LiteRequestForQuotationDto>();
            CreateMap<CreateRequestForQuotationContactDto, RequestForQuotationContact>();
            CreateMap<CreateServiceDto, Service>();
            //CreateMap<List<CreateRequestForQuotationContactDto>, List<RequestForQuotationContact>>();
            CreateMap<CreateAttributeForSourceTypeValueDto, AttributeForSourceTypeValue>();
            CreateMap<AttributeForSourceTypeValue, AttributeForSourceTypeValueDto>();
            CreateMap<RequestForQuotationContact, RequestForQuotationContactDto>();
            CreateMap<ServiceValuesDto, ServiceValue>();
            CreateMap<ServiceValue, ServiceValuesDto>();
            CreateMap<ServiceValueForOfferDto, ServiceValuesDto>();
            CreateMap<ServiceValue, ServiceDetailsDto>();
            CreateMap<ServiceValue, ServiceDto>();
            //CreateMap<ServiceValue, ServiceDto>();
            CreateMap<AttributeChoiceAndAttachmentDto, AttributeChoiceAndAttachment>();
            CreateMap<AttributeChoiceAndAttachment, AttributeChoiceAndAttachmentDetailsDto>();
            CreateMap<AskForHelp, LiteAskForHelpDto>();
            CreateMap<SearchedPlacesByUser, SearchedPlacesByUserDto>();
            CreateMap<SearchedPlacesByUserDto, SearchedPlacesByUser>();
            //CreateMap<Service, ServiceDetailsDto>();
            //CreateMap<List<CreateAttributeForSourceTypeValueDto>, List<AttributeForSourceTypeValue>>();
        }
    }
}
