using Abp.AutoMapper;
using Mofleet.Domain.PushNotifications;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.PushNotifications.Dto
{
    [AutoMap(typeof(PushNotificationTranslation))]
    public class PushNotificationTranslationDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Language { get; set; }
    }
}
