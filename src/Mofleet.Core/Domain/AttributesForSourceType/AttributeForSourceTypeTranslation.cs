using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Mofleet.Domain.AttributesForSourceType
{
    public class AttributeForSourceTypeTranslation : FullAuditedEntity, IEntityTranslation<AttributeForSourceType>
    {
        public string Name { get; set; }
        public AttributeForSourceType Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
