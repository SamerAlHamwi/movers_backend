using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mofleet.Notifications.Dto;
using System;
using System.Threading.Tasks;

namespace Mofleet.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        Task<PagedResultDto<NotificationDto>> GetUserNotificationsAsync(PagedNotificationResultRequestDto input);
        Task MarkNotificationAsReadAsync(EntityDto<Guid> input);
        Task<int> GetNumberOfUnReadUserNotificationsAsync();
    }
}