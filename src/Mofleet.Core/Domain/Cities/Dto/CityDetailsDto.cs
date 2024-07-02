using Abp.Application.Services.Dto;
using Mofleet.Countries.Dto;
using System;
using System.Collections.Generic;

namespace Mofleet.Cities.Dto
{
    public class CityDetailsDto : EntityDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public CountryDto Country { get; set; }
        public List<CityTranslationDto> Translations { get; set; }

    }
}
