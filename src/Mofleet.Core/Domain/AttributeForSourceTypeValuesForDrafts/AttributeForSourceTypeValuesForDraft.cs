using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Drafts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.AttributeForSourceTypeValuesForDrafts
{
    public class AttributeForSourceTypeValuesForDraft : FullAuditedEntity
    {
        public int DraftId { get; set; }
        [ForeignKey(nameof(DraftId))]
        public Draft Draft { get; set; }
        public int AttributeForSourcTypeId { get; set; }
        public int AttributeChoiceId { get; set; }
    }
}
