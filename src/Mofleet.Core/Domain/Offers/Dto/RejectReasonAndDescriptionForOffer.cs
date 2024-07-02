using Mofleet.Domain.RejectReasons.Dto;

namespace Mofleet.Domain.Offers.Dto
{
    public class RejectReasonAndDescriptionForOffer
    {
        public RejectReasonDetailsDto RejectReason { get; set; }
        public string RejectReasonDescription { get; set; }
    }
}
