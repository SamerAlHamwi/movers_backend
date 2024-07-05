﻿using Abp.Domain.Entities.Auditing;
using Mofleet.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domains.UserVerficationCodes
{

    public class UserVerficationCode : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public string VerficationCode { get; set; }
        public ConfirmationCodeType ConfirmationCodeType { get; set; }
        public bool IsForEmail { get; set; } = false;

        public static bool IsValidConfirmationCodeType(byte confirmationCodeType)
        {
            return System.Enum.IsDefined(typeof(ConfirmationCodeType), confirmationCodeType);
        }

    }
}
