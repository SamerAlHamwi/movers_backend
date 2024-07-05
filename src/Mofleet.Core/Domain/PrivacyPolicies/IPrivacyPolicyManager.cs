using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Mofleet.Domain.PrivacyPolicies
{
    public interface IPrivacyPolicyManager : IDomainService
    {
        Task<PrivacyPolicy> GetEntityByIdAsync(int id);
        Task<bool> CheckIfAnyPolicyExist();
    }
}
