using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Codes.Dto
{
    public class CreateCodeDto
    {
        [Required]
        [StringLength(8)]
        public string RSMCode { get; set; }

        public decimal DiscountPercentage { get; set; }

        public List<string> PhoneNumbers { get; set; }
        public CodeType CodeType { get; set; }
    }
}
