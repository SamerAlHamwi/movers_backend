using Abp.Application.Services.Dto;
using Mofleet.Domain.Drivers;
using Mofleet.Domain.Drivers.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.Trucks;
using Mofleet.Domain.Trucks.Dto;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Offers.Dto
{
    public class OfferDetailsDto : EntityDto<Guid>
    {

        public OfferStatues Statues { get; set; }
        public OfferProvider Provider { get; set; }

        public LiteDriverDto Driver { get; set; }
        public LiteTruckDto Truck { get; set; }

        public double Price { get; set; }
        public SelectedCompaniesBySystemForRequestDto SelectedCompanies { get; set; }
        public List<ServiceDetailsDto> ServiceValueForOffers { get; set; }
        public string Note { get; set; }
        /*public string RejectReasonDescription { get; set; }*/

        public RejectReasonAndDescriptionForOffer RejectReasonAndDescription { get; set; }
        public string ReasonRefuse { get; set; }
        public bool IsExtendStorage { get; set; }
        public double? PriceForOnDayStorage { get; set; }

    }
}
