using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks
{

    public class Truck : FullAuditedEntity
    {
        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public string Number { get; set; }

        public int Payload { get; set; }
    }
}
