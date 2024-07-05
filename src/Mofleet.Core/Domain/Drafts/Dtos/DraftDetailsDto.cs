using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.SourceTypes.Dto;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Drafts.Dtos
{
    public class DraftDetailsDto : EntityDto
    {
        public List<CreateAttributeForSourceTypeValueForDraftDto> AttributeForSourceTypeValues { get; set; }
        public SourceTypeDto SourceType { get; set; }
        public double? SourceLongitude { get; set; }
        public double? SourceLatitude { get; set; }
        public LiteCityDto? SourceCity { get; set; }
        public string SourceAddress { get; set; }
        public List<RequestForQuotationContactDto> RequestForQuotationContacts { get; set; }
        public List<ServiceDetailsDto> Services { get; set; }
        public DateTime? MoveAtUtc { get; set; }
        public double? DestinationLongitude { get; set; }
        public double? DestinationLatitude { get; set; }
        public LiteCityDto? DestinationCity { get; set; }
        public string DestinationAddress { get; set; }
        public DateTime? ArrivalAtUtc { get; set; }
        public string Comment { get; set; }
        public ServiceType? ServiceType { get; set; }
        public List<AttributeChoiceAndAttachmentForDraftDto> AttributeChoiceAndAttachments { get; set; }
        public long UserId { get; set; }


    }
}
