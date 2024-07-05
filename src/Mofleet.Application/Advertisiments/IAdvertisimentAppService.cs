using Mofleet.Advertisiments.Dto;
using Mofleet.CrudAppServiceBase;
using System.Threading.Tasks;


namespace Mofleet.Advertisiments
{
    public interface IAdvertisimentAppService : IMofleetAsyncCrudAppService<AdvertisimentDetailsDto, int, LiteAdvertisimentDto, PagedAdvertisimentResultRequestDto,
        CreateAdvertisimentDto, UpdateAdvertisimentDto>
    {
        Task AddAdvertisimentPositionToAdvertisiment(AddAdvertisimentPositionDto input);
    }
}
