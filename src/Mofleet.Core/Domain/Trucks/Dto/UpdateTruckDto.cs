using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks.Dto
{
    public class UpdateTruckDto : CreateTruckDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
