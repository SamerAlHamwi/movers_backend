using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.SubServices.Dto
{
    public class UpdateSubServiceDto : CreateSubServiceDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }

    }
}
