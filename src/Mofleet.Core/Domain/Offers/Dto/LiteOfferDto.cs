using Abp.Application.Services.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers.Dto;
using System;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Offers.Dto
{
    public class LiteOfferDto : EntityDto<Guid>
    {
        public SelectedCompaniesBySystemForRequestDto SelectedCompanies { get; set; }
        public double Price { get; set; }
        public OfferStatues Statues { get; set; }
        public OfferProvider Provider { get; set; }
        public string Note { get; set; }
        public string RejectReasonDescription { get; set; }
        public bool IsExtendStorage { get; set; }
        public double? PriceForOnDayStorage { get; set; }
    }

}
