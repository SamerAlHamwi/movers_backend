using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.SourceTypes.Dto;
using Mofleet.Domain.UserVerficationCodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class LiteRequestForQuotationDto : CreationAuditedEntityDto<long>
    {
        public List<ServiceDto> Services { get; set; }
        public SourceTypeDto SourceType { get; set; }
        public LiteCityDto SourceCity { get; set; }
        public string SourceAddress { get; set; }
        public LiteCityDto DestinationCity { get; set; }
        public string DestinationAddress { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public int SourceTypeId { get; set; }
        public DateTime MoveAtUtc { get; set; }
        public RequestForQuotationStatues Statues { get; set; }
        public ServiceType ServiceType { get; set; }
        public LiteUserDto User { get; set; }
        public string ReasonRefuse { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsThisRequestOfferWithThisCompany { get; set; } = false;
        public bool IsThisCompnayProvideOfferWithThisRequest { get; set; } = false;
        public string SourcePlaceNameByGoogle { get; set; }
        public string DestinationPlaceNameByGoogle { get; set; }
        public OfferStatues OfferStatues { get; set; }
        public int DiscountPercentageIfUserCancelHisRequest { get; set; }
        public bool IsWillBeDiscount => DateTime.UtcNow.AddHours(48) >= MoveAtUtc;
    }
}
