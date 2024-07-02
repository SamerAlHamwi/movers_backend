using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Codes;
using System.Collections.Generic;

namespace Mofleet.Domain.Partners
{
    public class Partner : FullAuditedEntity, IActiveEntity
    {
        public string PartnerPhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Code> Codes { get; set; }

        public virtual ICollection<City> CitiesPartner { get; set; }
    }
}
