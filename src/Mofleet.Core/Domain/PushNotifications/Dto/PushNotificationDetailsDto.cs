using Abp.Application.Services.Dto;

namespace Mofleet.PushNotifications.Dto
{
    public class PushNotificationDetailsDto : EntityDto
    {
        public string Message { get; set; }
    }
}
