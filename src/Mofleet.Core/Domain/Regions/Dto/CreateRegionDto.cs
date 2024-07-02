﻿using Abp.Runtime.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Regions.Dto
{
    public class CreateRegionDto : ICustomValidate
    {
        [Required]
        public List<RegionTranslationDto> Translations { get; set; }
        [Required]
        public int CityId { get; set; }
        public virtual void AddValidationErrors(CustomValidationContext context)
        {
            if (CityId == 0)
                context.Results.Add(new ValidationResult("CityId must has value"));
            if (Translations is null || Translations.Count < 2)
                context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
        }
    }
}
