using Abp.Application.Services.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Codes.Dto
{
    public class CodeDto : EntityDto
    {
        public string RSMCode { get; set; }

        public decimal DiscountPercentage { get; set; }
        public List<string> PhonesNumbers { get; set; }
        public bool IsActive { get; set; }
        public CodeType CodeType { get; set; }
    }
}
