using Abp.Application.Services.Dto;
using Mofleet.Domain.UserVerficationCodes;
using System;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.AskForHelps.Dto
{
    public class LiteAskForHelpDto : EntityDto
    {
        public LiteUserDto User { get; set; }
        public AskForHelpStatues Statues { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
