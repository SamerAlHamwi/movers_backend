using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drivers.Dto
{
    public class CreateDriverDto
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }


    }
}
