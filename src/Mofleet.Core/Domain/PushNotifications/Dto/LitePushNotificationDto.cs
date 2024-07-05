using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static Mofleet.Enums.Enum;

namespace Mofleet.PushNotifications.Dto
{
    public class LitePushNotificationDto : EntityDto<int>
    {
        public List<PushNotificationTranslationDto> Translations { get; set; }
        [JsonIgnore]
        public TopicType Destination { get; set; }
        public string DestinationText { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
    }
}

