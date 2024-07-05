using Mofleet.CrudAppServiceBase;
using Mofleet.Points.Dto;

namespace Mofleet.Points
{
    public interface IPointAppService : IMofleetAsyncCrudAppService<PointDetailsDto, int, LitePointDto, PagedPointResultRequestDto,
        CreatePointDto, UpdatePointDto>
    {
    }
}
