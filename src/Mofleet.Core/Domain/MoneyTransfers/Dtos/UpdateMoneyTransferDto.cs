using Abp.Application.Services.Dto;

namespace Mofleet.Domain.MoneyTransfers.Dtos
{
    public class UpdateMoneyTransferDto : CreateMoneyTransferDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
