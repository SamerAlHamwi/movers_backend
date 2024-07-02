using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.SubServices.Dto
{
    public class SubServiceSwitchActivationDto
    {
        [Required]
        public int Id { get; set; }
    }
}
