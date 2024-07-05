using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.FrequentlyQuestions.Dto;

namespace Mofleet.FrequentlyQuestionService
{
    public interface IFrequentlyQuestionAppService : IMofleetAsyncCrudAppService<FrequentlyQuestionDetailsDto, int, LiteFrequentlyQuestionDto, PagedFrequentlyQuestionResultRequestDto,
         CreateFrequentlyQuestionDto, UpdateFrequentlyQuestionDto>
    {
    }
}
