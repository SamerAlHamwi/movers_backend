﻿using Abp.Application.Services.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.PrivacyPolicyService.Dto
{
    public class LitePrivacyPolicyDto : EntityDto<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PrivacyPolicyTranslationDto> Translations { get; set; }
        public bool IsForMoney { get; set; }
        public bool IsActive { get; set; }
        public AppType App { get; set; }

    }
}
