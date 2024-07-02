using AutoMapper;
using Mofleet.Domain.AttributeAndAttachmentsForDrafts;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using Mofleet.Domain.AttributeForSourceTypeValuesForDrafts;
using Mofleet.Domain.Drafts;
using Mofleet.Domain.Drafts.Dtos;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.RequestForQuotationContactsForDrafts;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.ServiceValues.Dto;
using Mofleet.Domain.ServiceValuesForDrafts;
using System.Linq;

namespace Mofleet.Drafts.Mapper
{
    public class DraftMapProfile : Profile
    {
        public DraftMapProfile()
        {
            CreateMap<CreateDraftDto, Draft>();
            CreateMap<Draft, DraftDetailsDto>();
            CreateMap<Draft, LiteDraftDto>();
            CreateMap<CreateAttributeForSourceTypeValueForDraftDto, AttributeForSourceTypeValuesForDraft>();
            CreateMap<AttributeForSourceTypeValuesForDraft, CreateAttributeForSourceTypeValueForDraftDto>();
            CreateMap<CreateRequestForQuotationContactDto, RequestForQuotationContactsForDraft>();
            CreateMap<RequestForQuotationContactsForDraft, CreateRequestForQuotationContactDto>();
            CreateMap<RequestForQuotationContactsForDraft, RequestForQuotationContactDto>();
            CreateMap<ServiceValuesForDraftDto, ServiceValuesForDraft>();
            CreateMap<ServiceValuesForDraft, ServiceValuesForDraftDto>();
            CreateMap<ServiceValuesForDraft, ServiceDetailsDto>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AttributeChoiceAndAttachmentDto, AttributeAndAttachmentsForDraft>();
            CreateMap<AttributeChoiceAndAttachmentForDraftDto, AttributeAndAttachmentsForDraft>();
            CreateMap<AttributeForSourceTypeValuesForDraft, AttributeForSourceTypeValueDto>();
            CreateMap<AttributeAndAttachmentsForDraft, AttributeChoiceAndAttachmentDto>()
             .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => src.Attachments.Select(x => x.Id)));
            CreateMap<AttributeAndAttachmentsForDraft, AttributeChoiceAndAttachmentForDraftDto>()
                      .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => src.Attachments.Select(x => x.Id)));


        }
    }
}
