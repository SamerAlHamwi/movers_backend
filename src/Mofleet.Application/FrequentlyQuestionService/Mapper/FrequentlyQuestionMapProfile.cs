using AutoMapper;
using Mofleet.Domain.FrequentlyQuestions;
using Mofleet.Domain.FrequentlyQuestions.Dto;

namespace KeyFinder.FrequentlyQuestionService.Mapper
{
    public class FrequentlyQuestionMapProfile : Profile
    {
        public FrequentlyQuestionMapProfile()
        {
            CreateMap<CreateFrequentlyQuestionDto, FrequentlyQuestion>();
            //CreateMap<FrequentlyQuestion, FrequentlyQuestionDetailsDto>();
        }
    }
}
