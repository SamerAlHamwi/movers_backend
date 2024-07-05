using System.Collections.Generic;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class AttributeChoiceAndAttachmentDto
    {
        public int? AttributeChoiceId { get; set; }
        public List<long> AttachmentIds { get; set; }
    }
    public class AttributeChoiceAndAttachmentForDraftDto : AttributeChoiceAndAttachmentDto
    {
        public List<LiteAttachmentDto> Attachments { get; set; } = new List<LiteAttachmentDto>();
    }
}
