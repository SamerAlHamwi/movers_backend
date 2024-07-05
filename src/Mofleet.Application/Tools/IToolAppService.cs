using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.Toolss.Dto;
using System.Threading.Tasks;

namespace Mofleet.AttributesForSourceType
{
    public interface IToolAppService : IMofleetAsyncCrudAppService<ToolDetailsDto, int, LiteToolDto
        , PagedToolResultRequestDto, CreateToolDto, UpdateToolDto>
    {
        Task<ToolDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input);
    }
}
