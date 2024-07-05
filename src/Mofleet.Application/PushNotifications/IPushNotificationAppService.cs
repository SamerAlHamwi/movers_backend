using Mofleet.CrudAppServiceBase;
using Mofleet.PushNotifications.Dto;

namespace Mofleet.PushNotifications
{
    public interface IPushNotificationAppService : IMofleetAsyncCrudAppService<PushNotificationDetailsDto, int, LitePushNotificationDto, PagedPushNotificationResultRequestDto,
         CreatePushNotificationDto, UpdatePushNotificationDto>
    {
    }
}
