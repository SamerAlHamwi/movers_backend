using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.Drafts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.AttributeAndAttachmentsForDrafts
{
    public class AttributeAndAttachmentsForDraft : FullAuditedEntity
    {
        public int DraftId { get; set; }
        [ForeignKey(nameof(DraftId))]
        public virtual Draft Draft { get; set; }
        public int? AttributeChoiceId { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
