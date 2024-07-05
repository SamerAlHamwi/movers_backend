using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks.Dto
{
    public class TruckDto : EntityDto
    {
        public int CompanyId { get; set; }

        public string Number { get; set; }

        public int Payload { get; set; }
    }
}
