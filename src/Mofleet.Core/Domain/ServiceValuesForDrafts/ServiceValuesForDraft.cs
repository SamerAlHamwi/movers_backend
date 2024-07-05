using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Drafts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.ServiceValuesForDrafts
{
    public class ServiceValuesForDraft : FullAuditedEntity
    {
        public int DraftId { get; set; }
        [ForeignKey(nameof(DraftId))]
        public virtual Draft Draft { get; set; }
        public int? ServiceId { get; set; }
        public int? SubServiceId { get; set; }
        public int? ToolId { get; set; }
    }
}
