using Abp.Application.Services.Dto;

namespace Mofleet.PushNotifications.Dto
{
    public class PushNotificationDto : EntityDto<int>
    {
        public string Message { get; set; }
    }
}
