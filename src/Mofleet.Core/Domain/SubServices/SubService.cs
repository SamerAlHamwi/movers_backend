﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Mofleet.Domain.services;
using Mofleet.Domain.Toolss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.SubServices
{
    public class SubService : FullAuditedEntity, IMultiLingualEntity<SubServiceTranslation>
    {
        public SubService()
        {
            Translations = new HashSet<SubServiceTranslation>();
        }
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
        public ICollection<SubServiceTranslation> Translations { get; set; }
        public ICollection<Tool> Tools { get; set; }

    }
}
