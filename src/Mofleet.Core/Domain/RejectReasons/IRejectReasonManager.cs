using Abp.Domain.Services;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RejectReasons
{
    public interface IRejectReasonManager : IDomainService
    {
        Task<RejectReason> GetEntityByIdAsync(int Id);
        Task CheckIfRejectReasonIsExist(int Id);
        Task<PossibilityPotentialClient> GetRejectResonTypeByIdAsync(int Id);
    }
}
