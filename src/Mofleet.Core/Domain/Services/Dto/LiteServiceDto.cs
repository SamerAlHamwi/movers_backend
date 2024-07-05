using Abp.Application.Services.Dto;
using Mofleet.Domain.Services.Dto;
using Mofleet.Domain.SubServices.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.services.Dto
{
    public class LiteServiceDto : EntityDto<int>
    {
        public string Name { get; set; }
        public List<ServiceTranslationDto> Translations { get; set; }
        public LiteAttachmentDto Attachment { get; set; }
        public bool IsForStorage { get; set; }
        public bool IsForTruck { get; set; }
        public List<SubServiceDto> SubServices { get; set; }
        public bool Active { get; set; }

    }
}
