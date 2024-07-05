﻿using Abp.Domain.Entities.Auditing;
using Mofleet.Authorization.Users;
using Mofleet.Domain.Offers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.Reviews
{
    public class Review : FullAuditedEntity
    {
        public string ReviewDescription { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public Guid? OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }

        public double Quality { get; set; }
        public double CustomerService { get; set; }
        public double ValueOfServiceForMoney { get; set; }

        public double OverallRating { get; set; }

    }
}
