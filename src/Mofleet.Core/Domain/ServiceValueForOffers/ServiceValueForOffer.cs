using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.services;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.ServiceValueForOffers
{
    public class ServiceValueForOffer : FullAuditedEntity<Guid>
    {
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
        public int SubServiceId { get; set; }
        public int? ToolId { get; set; }
        public int? Amount { get; set; }
    }
}
