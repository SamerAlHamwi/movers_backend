using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.SourceTypes.Dto
{
    public class UpdateSourceTypeDto : CreateSourceTypeDto, IEntityDto
    {
        [Required]
        public int Id { get; set; }

    }
}
