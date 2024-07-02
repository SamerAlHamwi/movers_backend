using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.Partners.Dto
{
    public class UpdatePartnerDto : CreatePartnerDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }
    }
}
