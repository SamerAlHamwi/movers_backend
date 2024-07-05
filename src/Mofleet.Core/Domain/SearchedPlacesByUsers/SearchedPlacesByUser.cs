using Abp.Domain.Entities.Auditing;
using Mofleet.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.SearchedPlacesByUsers
{
    public class SearchedPlacesByUser : FullAuditedEntity
    {
        public string PlaceId { get; set; }
        public string AddressName { get; set; }
        public double lang { get; set; }
        public double lat { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
