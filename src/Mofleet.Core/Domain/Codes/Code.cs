using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.Partners;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Codes
{
    public class Code : FullAuditedEntity, IActiveEntity
    {
        [Required]
        [StringLength(8)]
        public string RSMCode { get; set; }

        public decimal DiscountPercentage { get; set; }
        public int PartnerId { get; set; }
        [ForeignKey(nameof(PartnerId))]
        public virtual Partner Partner { get; set; }

        public string PhonesNumbers { get; set; }
        public bool IsActive { get; set; }
        public CodeType CodeType { get; set; }
    }
}
