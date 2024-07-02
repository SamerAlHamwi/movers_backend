using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Advertisiments.Dto
{
    public class UpdateAdvertisimentDto : CreateAdvertisimentDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }
    }
}
