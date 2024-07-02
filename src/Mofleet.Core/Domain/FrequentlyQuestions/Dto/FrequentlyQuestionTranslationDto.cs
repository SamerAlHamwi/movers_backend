using Abp.AutoMapper;

namespace Mofleet.Domain.FrequentlyQuestions.Dto
{
    [AutoMap(typeof(FrequentlyQuestionTranslation))]

    public class FrequentlyQuestionTranslationDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Language { get; set; }

    }
}
