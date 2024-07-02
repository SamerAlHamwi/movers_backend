﻿using Abp.Application.Services.Dto;

using System.Collections.Generic;

namespace Mofleet.Domain.SourceTypes.Dto
{
    public class LiteSourceTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public List<SourceTypeTranslationDto> Translations { get; set; }
        public int PointsToGiftToCompany { get; set; }
        public int PointsToBuyRequest { get; set; }
        public LiteAttachmentDto Icon { get; set; } = new LiteAttachmentDto();
        public bool IsMainForPoints { get; set; }
        public bool IsActive { get; set; }

    }
}
