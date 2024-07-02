using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.RejectReasons.Dto
{
    public class RejectReasonDto : EntityDto<int>
    {
        public string Name { get; set; }
        public List<RejectReasonTranslationDto> Translations { get; set; }
    }
}
