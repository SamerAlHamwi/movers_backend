using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Companies;
using System.Collections.Generic;

namespace Mofleet.Domain.CommissionGroups
{
    public class CommissionGroup : FullAuditedEntity
    {
        public double Name { get; set; }
        public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
        public bool IsDefault { get; set; }
    }
}
