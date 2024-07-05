using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Mofleet.Domain.Trucks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Trucks
{
    public class TruckManager : ITruckManager
    {
        private readonly IRepository<Truck> repository;

        public TruckManager(IRepository<Truck> repository)
        {
            this.repository = repository;
        }

        public async Task<Truck> GetEntityByIdAsync(int id)
        {
            return await repository.GetAll()
                .AsNoTrackingWithIdentityResolution().
                Include(x => x.Company).ThenInclude(x=>x.Translations)                
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
