using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using Mofleet.Domain.UserVerficationCodes;
using System;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class RequestForQuotationDto : EntityDto<int>
    {
        public LiteUserDto User { get; set; }
        public CityDto SourceCity { get; set; }
        public CityDto DestinationCity { get; set; }
        public DateTime MoveAtUtc { get; set; }
        public DateTime ArrivalAtUtc { get; set; }
        public RequestForQuotationStatues Statues { get; set; }
        public ServiceType ServiceType { get; set; }
        public string ReasonRefuse { get; set; }
    }
}
