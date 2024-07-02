using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.PushNotifications.Dto
{
    public class PagedPushNotificationResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        /// <summary>
        ///  All User = 0, HealthCareAdmin = 1, Doctor = 2,Receptionist = 3, Patient = 4
        /// </summary>

        public TopicType? Destination { get; set; }

    }
}
