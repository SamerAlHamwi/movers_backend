using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Mofleet.Domain.PushNotifications
{
    public interface IPushNotificationManager : IDomainService
    {
        Task<PushNotification> GetPushNotificationById(int pushNotificationId);
    }
}
