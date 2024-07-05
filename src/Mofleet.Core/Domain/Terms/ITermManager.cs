using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Mofleet.Domain.Terms
{
    public interface ITermManager : IDomainService
    {
        Task<Term> GetEntityByIdAsync(int id);
        Task<bool> CheckIfAnyPolicyExist();
    }
}
