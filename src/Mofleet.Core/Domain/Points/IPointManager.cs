using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Mofleet.Domain.Points
{
    // ICityManager
    public interface IPointManager : IDomainService
    {

        Task<Point> GetEntityByIdAsync(int id);

    }
}
