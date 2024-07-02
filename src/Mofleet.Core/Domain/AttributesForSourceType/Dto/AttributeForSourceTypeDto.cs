﻿using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.AttributesForSourceType.Dto
{
    public class AttributeForSourceTypeDto : EntityDto
    {
        public string Name { get; set; }
        public List<AttributeForSourceTypeTranslationDto> Translations { get; set; }
    }
}
