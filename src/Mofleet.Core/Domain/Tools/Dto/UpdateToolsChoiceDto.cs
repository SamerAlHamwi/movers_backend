using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Toolss.Dto
{
    public class UpdateToolDto : CreateToolDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }
    }
}
