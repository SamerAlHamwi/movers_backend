using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.PrivacyPolicies
{
    public class PrivacyPolicy : FullAuditedEntity, IMultiLingualEntity<PrivacyPolicyTranslation>, IActiveEntity
    {
        public ICollection<PrivacyPolicyTranslation> Translations { get; set; }
        public bool IsActive { get; set; }
        public bool IsForMoney { get; set; }
        public AppType App { get; set; }
    }
}
