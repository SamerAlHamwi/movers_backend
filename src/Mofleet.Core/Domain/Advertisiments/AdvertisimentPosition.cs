using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Advertisiments
{
    public class AdvertisimentPosition : FullAuditedEntity
    {
        public int AdvertisimentId { get; set; }
        [ForeignKey(nameof(AdvertisimentId))]
        public Advertisiment Advertisiment { get; set; }
        public PositionForAdvertisiment Position { get; set; }
        public Screen Screen { get; set; }
    }
}
