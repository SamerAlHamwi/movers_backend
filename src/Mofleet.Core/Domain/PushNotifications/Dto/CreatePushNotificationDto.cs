using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.PushNotifications.Dto
{
    public class CreatePushNotificationDto
    {
        public List<PushNotificationTranslationDto> Translations { get; set; }

        public TopicType Destination { get; set; }

    }
}
