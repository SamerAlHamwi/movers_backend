using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.RequestForQuotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.AttributeForSourcTypeValues
{
    public class AttributeForSourceTypeValue : FullAuditedEntity
    {
        public int AttributeForSourcTypeId { get; set; }
        [ForeignKey(nameof(AttributeForSourcTypeId))]
        public virtual AttributeForSourceType AttributeForSourcType { get; set; }
        public long RequestForQuotationId { get; set; }
        [ForeignKey(nameof(RequestForQuotationId))]
        public virtual RequestForQuotation RequestForQuotation { get; set; }
        public int? AttributeChoiceId { get; set; }
        [ForeignKey(nameof(AttributeChoiceId))]
        public virtual AttributeChoice AttributeChoice { get; set; }
    }
}
