using Abp.Domain.Entities.Auditing;
using Mofleet.Authorization.Users;
using Mofleet.Countries;
using Mofleet.Domain.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drivers
{
    public class Driver : FullAuditedEntity
    {

        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public string  Name { get; set; }

        public string PhoneNumber { get; set; }



    }
}
