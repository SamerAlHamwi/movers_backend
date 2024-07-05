using Abp.Application.Services.Dto;

namespace Mofleet.Domain.Drafts.Dtos
{
    public class UpdateDraftDto : CreateDraftDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
