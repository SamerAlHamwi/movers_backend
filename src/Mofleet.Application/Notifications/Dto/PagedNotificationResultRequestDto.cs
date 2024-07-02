using Abp.Application.Services.Dto;
using Abp.Notifications;
using Mofleet.NotificationService;

namespace Mofleet.Notifications.Dto
{
    public class PagedNotificationResultRequestDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// State: 0- Unread, 1- Read
        /// </summary>
        public UserNotificationState? State { get; set; }

        public NotificationType? Type { get; set; }
    }
}