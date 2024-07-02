using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.Points;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.PointsValues
{
    public class PointsValue : FullAuditedEntity
    {

        public int? CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }
        public int? CompanyBranchId { get; set; }
        [ForeignKey(nameof(CompanyBranchId))]
        public virtual CompanyBranch CompanyBranche { get; set; }
        public int? PointId { get; set; }
        [ForeignKey(nameof(PointId))]
        public virtual Point Point { get; set; }
        public bool IsForFeature { get; set; }

    }
}
