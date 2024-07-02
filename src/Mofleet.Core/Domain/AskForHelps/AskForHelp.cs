using Abp.Domain.Entities.Auditing;
using Mofleet.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.AskForHelps
{
    public class AskForHelp : FullAuditedEntity
    {
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public string Message { get; set; }
        public AskForHelpStatues Statues { get; set; }
    }
}
