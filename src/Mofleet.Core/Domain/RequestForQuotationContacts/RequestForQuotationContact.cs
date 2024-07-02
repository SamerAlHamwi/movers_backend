using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.RequestForQuotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotationContacts
{
    public class RequestForQuotationContact : FullAuditedEntity<long>
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string DailCode { get; set; }
        public bool IsWhatsAppAvailable { get; set; }
        public bool IsTelegramAvailable { get; set; }
        public bool IsCallAvailable { get; set; }
        public long RequestForQuotationId { get; set; }
        [ForeignKey(nameof(RequestForQuotationId))]
        public virtual RequestForQuotation RequestForQuotation { get; set; }
        public RequestForQuotationContactType RequestForQuotationContactType { get; set; }

    }
}
