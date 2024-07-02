using Abp.Domain.Services;
using Mofleet.Domain.Trucks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks
{
    public interface ITruckManager : IDomainService
    {
        Task <Truck> GetEntityByIdAsync(int id);
    }
}
