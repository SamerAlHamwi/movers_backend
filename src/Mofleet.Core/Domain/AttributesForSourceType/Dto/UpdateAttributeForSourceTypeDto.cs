using Abp.Application.Services.Dto;

namespace Mofleet.Domain.AttributesForSourceType.Dto
{
    public class UpdateAttributeForSourceTypeDto : CreateAttributeForSourceTypeDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
