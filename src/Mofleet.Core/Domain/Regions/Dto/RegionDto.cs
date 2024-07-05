using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Regions.Dto
{
    public class RegionDto : EntityDto<int>
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public LiteCityDto City { get; set; }
        public List<RegionTranslationDto> Translations { get; set; }

    }
}
