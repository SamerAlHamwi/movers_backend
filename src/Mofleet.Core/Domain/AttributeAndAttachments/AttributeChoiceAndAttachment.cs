using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.RequestForQuotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.AttributeAndAttachments
{
    public class AttributeChoiceAndAttachment : FullAuditedEntity
    {
        public int AttributeChoiceId { get; set; }

        [ForeignKey(nameof(AttributeChoiceId))]
        public AttributeChoice AttributeChoice { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
        public long RequestForQuotationId { get; set; }

        [ForeignKey(nameof(RequestForQuotationId))]
        public RequestForQuotation RequestForQuotation { get; set; }
    }
}
