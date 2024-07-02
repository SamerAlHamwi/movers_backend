using Mofleet.Domain.AttributeChoices.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class AttributeChoiceAndAttachmentDetailsDto
    {
        public AttributeChoiceDetailsDto AttributeChoice { get; set; }
        public List<LiteAttachmentDto> Attachments { get; set; } = new List<LiteAttachmentDto>();
    }
}
