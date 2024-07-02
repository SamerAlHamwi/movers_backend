﻿using Abp.Application.Services.Dto;
using Mofleet.Domain.AttributeForSourceTypeValues.Dto;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.ServiceValues.Dto;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Drafts.Dtos
{
    public class LiteDraftDto : EntityDto
    {
        public List<CreateAttributeForSourceTypeValueForDraftDto> AttributeForSourceTypeValues { get; set; }
        public int? SourceTypeId { get; set; }
        public double? SourceLongitude { get; set; }
        public double? SourceLatitude { get; set; }
        public int? SourceCityId { get; set; }
        public string SourceAddress { get; set; }
        public List<CreateRequestForQuotationContactDto> RequestForQuotationContacts { get; set; }
        public List<ServiceValuesForDraftDto> Services { get; set; }
        public DateTime? MoveAtUtc { get; set; }
        public double? DestinationLongitude { get; set; }
        public double? DestinationLatitude { get; set; }
        public int? DestinationCityId { get; set; }
        public string DestinationAddress { get; set; }
        public DateTime? ArrivalAtUtc { get; set; }
        public string Comment { get; set; }
        public ServiceType? ServiceType { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
