using Abp.Application.Services.Dto;
using Mofleet.Domain.SourceTypes.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.AttributesForSourceType.Dto
{
    public class AttributeForSourceTypeDetailsDto : EntityDto
    {
        public string Name { get; set; }
        public SourceTypeDetailsDto SourceType { get; set; }

        public List<AttributeForSourceTypeTranslationDto> Translations { get; set; }
        public bool IsActive { get; set; }

    }
}
