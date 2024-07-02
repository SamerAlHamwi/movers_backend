﻿using Abp.Application.Services.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.TermService.Dto
{
    public class TermDetailsDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TermTranslationDto> Translations { get; set; }
        public bool IsActive { get; set; }
        public AppType App { get; set; }

    }
}
