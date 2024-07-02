using Mofleet.Domain.AttributeChoices.Dto;
using Mofleet.Domain.AttributesForSourceType.Dto;

namespace Mofleet.Domain.AttributeForSourceTypeValues.Dto
{
    public class AttributeForSourceTypeValueDto
    {
        public AttributeForSourceTypeDetailsDto AttributeForSourcType { get; set; }
        public AttributeChoiceDetailsDto AttributeChoice { get; set; }
    }
}
