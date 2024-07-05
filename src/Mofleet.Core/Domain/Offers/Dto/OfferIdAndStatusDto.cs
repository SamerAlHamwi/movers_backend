using System;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Offers.Dto
{
    public class OfferIdAndStatusDto
    {
        public Guid? SelectedOfferId { get; set; }
        public OfferStatues OfferStatues { get; set; }
    }
}
