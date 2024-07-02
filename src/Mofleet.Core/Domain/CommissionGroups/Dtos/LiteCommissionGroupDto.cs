using Abp.Application.Services.Dto;
using Mofleet.Domain.Companies.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.CommissionGroups.Dtos
{
    public class LiteCommissionGroupDto : EntityDto
    {
        public string Name { get; set; }
        public List<CompanyDto> Companies { get; set; }
        public bool IsDefault { get; set; }

    }
}
