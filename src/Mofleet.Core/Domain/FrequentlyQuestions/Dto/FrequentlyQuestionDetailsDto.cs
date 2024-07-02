using Abp.Application.Services.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.FrequentlyQuestions.Dto
{
    public class FrequentlyQuestionDetailsDto : EntityDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public List<FrequentlyQuestionTranslationDto> Translations { get; set; }
        public bool IsActive { get; set; }
        public AppType App { get; set; }

    }
}
