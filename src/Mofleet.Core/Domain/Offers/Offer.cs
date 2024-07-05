using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Drivers;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.Trucks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Offers
{
    public class Offer : FullAuditedEntity<Guid>
    {

        public Guid SelectedCompaniesId { get; set; }
        [ForeignKey(nameof(SelectedCompaniesId))]
        public virtual SelectedCompaniesBySystemForRequest SelectedCompanies { get; set; }
        public virtual ICollection<ServiceValueForOffer> ServiceValueForOffers { get; set; } = new List<ServiceValueForOffer>();
        public double Price { get; set; }
        public OfferStatues Statues { get; set; }
        public OfferProvider Provider { get; set; }
        public string Note { get; set; }

        public int? RejectReasonId { get; set; }
        [ForeignKey(nameof(RejectReasonId))]
        public virtual RejectReason RejectReason { get; set; }

        public string RejectReasonDescription { get; set; }
        public string ReasonRefuse { get; set; }
        public bool IsExtendStorage { get; set; }
        public double? PriceForOnDayStorage { get; set; }

        public int? DriverId { get; set; }
        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; }

        public int? TruckId { get; set; }
        [ForeignKey(nameof(TruckId))]
        public virtual Truck Truck { get; set; }


    }
}
