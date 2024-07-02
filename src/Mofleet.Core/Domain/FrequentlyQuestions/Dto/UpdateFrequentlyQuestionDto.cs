using Abp.Application.Services.Dto;

namespace Mofleet.Domain.FrequentlyQuestions.Dto
{
    public class UpdateFrequentlyQuestionDto : CreateFrequentlyQuestionDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
