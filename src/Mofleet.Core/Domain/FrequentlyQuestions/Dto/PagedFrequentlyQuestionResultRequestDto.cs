using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.FrequentlyQuestions.Dto
{
    public class PagedFrequentlyQuestionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public AppType? App { get; set; }

    }
}
