using System.ComponentModel.DataAnnotations;

namespace Mofleet.Authorization.Accounts.Dto
{
    public class AddUserProfilePhotoDto
    {
        [Required]
        public long PhotoId { get; set; }
    }
}

