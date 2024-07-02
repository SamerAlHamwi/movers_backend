using Abp.Application.Services.Dto;
using Mofleet.Domain.Toolss.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.SubServices.Dto
{
    public class SubServiceDto : EntityDto<int>
    {
        public string Name { get; set; }
        public List<SubServiceTranslationDto> Translations { get; set; }
        public LiteAttachmentDto Attachment { get; set; }
        public List<ToolDetailsDto> Tools { get; set; }

    }
}
