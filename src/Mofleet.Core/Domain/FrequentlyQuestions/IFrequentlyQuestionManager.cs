using Abp.Domain.Services;
using Mofleet.Domain.FrequentlyQuestions;
using System.Threading.Tasks;

namespace Mofleet.FrequentlyQuestions
{
    public interface IFrequentlyQuestionManager : IDomainService
    {
        Task<FrequentlyQuestion> GetEntityByIdAsync(int id);
    }
}
