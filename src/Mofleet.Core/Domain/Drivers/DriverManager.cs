using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drivers
{
    public class DriverManager : IDriverManager
    {
        private readonly IRepository<Driver> repository;

        public DriverManager(IRepository<Driver> repository)
        {
            this.repository = repository;
        }

        public async Task<Driver> GetEntityByIdAsync(int id)
        {
            return await repository.GetAll()
                .AsNoTrackingWithIdentityResolution().
                Include(x => x.Company)                
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
