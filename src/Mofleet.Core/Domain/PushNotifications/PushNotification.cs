﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.PushNotifications
{
    public class PushNotification : FullAuditedEntity, IMultiLingualEntity<PushNotificationTranslation>
    {
        public ICollection<PushNotificationTranslation> Translations { get; set; }
        public TopicType Destination { get; set; }
    }
}
