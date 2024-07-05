using Abp.Application.Services.Dto;
using Mofleet.Domain.Services.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.services.Dto
{
    public class ServiceDto : EntityDto<int>
    {
        public string Name { get; set; }
        public List<ServiceTranslationDto> Translations { get; set; }
    }
}
