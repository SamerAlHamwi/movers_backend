using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drivers.Dto
{
    public class PagedDriverResultRequestDto : PagedResultRequestDto
    {
        public int? CompanyId { get; set; }
        public string Keyword { get; set; }

    }
}
