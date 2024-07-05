using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks.Dto
{
    public class CreateTruckDto
    {
        public int CompanyId { get; set; }

        public string Number { get; set; }

        public int Payload { get; set; }


    }
}
