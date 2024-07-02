using AutoMapper;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.MoneyTransfers.Dtos;

namespace Mofleet.MoneyTransfers.Mapper
{
    public class MoneyTransferMapProfile : Profile
    {
        public MoneyTransferMapProfile()
        {
            CreateMap<MoneyTransferDto, MoneyTransfer>();
            CreateMap<MoneyTransfer, MoneyTransferDto>();
        }
    }
}
