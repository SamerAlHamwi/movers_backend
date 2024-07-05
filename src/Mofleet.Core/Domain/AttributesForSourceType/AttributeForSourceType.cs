using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.SourceTypes;
using System.Collections.Generic;

namespace Mofleet.Domain.AttributesForSourceType
{
    public class AttributeForSourceType : FullAuditedEntity, IActiveEntity, IMultiLingualEntity<AttributeForSourceTypeTranslation>
    {

        public ICollection<SourceType> SourceTypes { get; set; }
        public ICollection<AttributeChoice> AttributeChoices { get; set; }
        public ICollection<AttributeForSourceTypeTranslation> Translations { get; set; }
        public bool IsActive { get; set; }

    }
}
