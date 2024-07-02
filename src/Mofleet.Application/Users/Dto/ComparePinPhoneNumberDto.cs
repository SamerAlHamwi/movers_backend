using System.ComponentModel.DataAnnotations;

namespace Mofleet.Users.Dto
{
    public class ComparePinPhoneNumberDto
    {
        [Required]
        public string PIN { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
