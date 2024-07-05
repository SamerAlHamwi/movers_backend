using System.ComponentModel.DataAnnotations;

namespace Mofleet.Advertisiments.Dto
{
    public class AddadvertisimentElementDto : CreateAdvertisimentElementDto
    {
        [Required]
        public int AdvertisimentId { get; set; }
    }
}
