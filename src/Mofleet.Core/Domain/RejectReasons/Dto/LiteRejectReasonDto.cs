﻿using Abp.Application.Services.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RejectReasons.Dto
{
    public class LiteRejectReasonDto : EntityDto<int>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public PossibilityPotentialClient PossibilityPotentialClient { get; set; }
        public List<RejectReasonTranslationDto> Translations { get; set; }


    }
}
