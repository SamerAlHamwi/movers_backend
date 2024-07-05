using Abp.AutoMapper;
using Mofleet.Domain.services;

namespace Mofleet.Domain.Services.Dto
{
    [AutoMap(typeof(ServiceTranslation))]
    public class ServiceTranslationDto
    {
        public string Name { get; set; }

        public string Language { get; set; }

    }
}
