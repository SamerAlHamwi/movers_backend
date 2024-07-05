using Abp.Application.Services.Dto;

namespace Mofleet.TermService.Dto
{
    public class UpdateTermDto : CreateTermDto, IEntityDto
    {
        public int Id { get; set; }
    }
}