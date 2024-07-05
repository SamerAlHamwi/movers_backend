using System.ComponentModel.DataAnnotations;

namespace Mofleet.Authorization.Accounts.Dto
{
    public class ChangePhoneNumberDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string DialCode { get; set; }
    }
}
