using Abp.Application.Services;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.MoneyTransfers.Dtos;
using System.Threading.Tasks;

namespace Mofleet.MoneyTransfers
{
    public interface IMoneyTransferAppService : IAsyncCrudAppService<MoneyTransferDto, int, PagedMoneyTransferResultRequestDto, CreateMoneyTransferDto, UpdateMoneyTransferDto, MoneyTransferDto>
    {
        Task InsertNewMoneyTransfer(MoneyTransfer input);
    }
}
