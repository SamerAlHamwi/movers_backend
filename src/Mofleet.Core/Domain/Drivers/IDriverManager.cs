using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drivers
{
    public interface IDriverManager : IDomainService
    {
        Task <Driver> GetEntityByIdAsync(int id);
    }
}
