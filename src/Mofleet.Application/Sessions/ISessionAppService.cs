using Abp.Application.Services;
using Mofleet.Sessions.Dto;
using System.Threading.Tasks;

namespace Mofleet.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
