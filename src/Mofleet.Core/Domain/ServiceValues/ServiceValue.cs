using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.services;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.ServiceValues
{
    public class ServiceValue : FullAuditedEntity<long>
    {
        public long? RequestForQuotationId { get; set; }
        [ForeignKey(nameof(RequestForQuotationId))]
        public RequestForQuotation RequestForQuotation { get; set; }
        public ServiceValueType ServiceValueType { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }
        public int? CompanyBranchId { get; set; }
        [ForeignKey(nameof(CompanyBranchId))]
        public virtual CompanyBranch CompanyBranche { get; set; }
        public int? ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
        public int? SubServiceId { get; set; }
        public int? ToolId { get; set; }
    }
}
