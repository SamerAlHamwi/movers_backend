using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Companies.Dto
{
    public class RequestIncludeBranchDto
    {
        [Required]
        public string DialCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PinCode { get; set; }
    }


}
