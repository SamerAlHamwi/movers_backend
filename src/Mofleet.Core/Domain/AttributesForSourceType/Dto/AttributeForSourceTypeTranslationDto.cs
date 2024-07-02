using Abp.AutoMapper;

namespace Mofleet.Domain.AttributesForSourceType.Dto
{
    [AutoMap(typeof(AttributeForSourceTypeTranslation))]
    public class AttributeForSourceTypeTranslationDto
    {
        public string Name { get; set; }

        public string Language { get; set; }

    }
}
