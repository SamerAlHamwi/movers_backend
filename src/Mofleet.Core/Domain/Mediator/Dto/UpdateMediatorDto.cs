using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Mediators.Dto
{
    public class UpdateMediatorDto : CreateMediatorDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }
    }
}
