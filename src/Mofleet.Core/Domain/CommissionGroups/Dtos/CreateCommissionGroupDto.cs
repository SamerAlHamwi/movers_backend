﻿using System.Collections.Generic;

namespace Mofleet.Domain.CommissionGroups.Dtos
{
    public class CreateCommissionGroupDto
    {
        public double Name { get; set; }
        public List<int> CompanyIds { get; set; } = new List<int>();
        public bool IsDefault { get; set; }
    }
}
