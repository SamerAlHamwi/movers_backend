using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Mofleet.Domain.PointsValues
{
    public interface IPointsValueManager : IDomainService
    {
        Task InsertPointsValue(PointsValue pointsValue);
    }
}
