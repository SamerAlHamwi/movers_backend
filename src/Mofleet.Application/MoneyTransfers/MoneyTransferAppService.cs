using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.MoneyTransfers.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Mofleet.MoneyTransfers
{
    public class MoneyTransferAppService : AsyncCrudAppService<MoneyTransfer, MoneyTransferDto, int, PagedMoneyTransferResultRequestDto, CreateMoneyTransferDto, UpdateMoneyTransferDto, MoneyTransferDto>, IMoneyTransferAppService
    {
        public MoneyTransferAppService(IRepository<MoneyTransfer, int> repository)
            : base(repository)
        {
        }
        public override async Task<PagedResultDto<MoneyTransferDto>> GetAllAsync(PagedMoneyTransferResultRequestDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                return await base.GetAllAsync(input);
            }
        }
        protected override IQueryable<MoneyTransfer> CreateFilteredQuery(PagedMoneyTransferResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Company).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.Company).ThenInclude(x => x.CompanyContact).AsNoTracking();
            data = data.Include(x => x.CompanyBranch).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.CompanyBranch).ThenInclude(x => x.CompanyContact).AsNoTracking();
            data = data.Include(x => x.User).AsNoTracking();
            if (input.ReasonOfPaid.HasValue)
                data = data.Where(x => x.ReasonOfPaid == input.ReasonOfPaid.Value);
            if (input.PaidProvider.HasValue)
                data = data.Where(x => x.PaidProvider == input.PaidProvider.Value);
            if (input.PaidStatues.HasValue)
                data = data.Where(x => x.PaidStatues == input.PaidStatues.Value);
            return data;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task DeleteAsync(EntityDto<int> input)
        { }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<MoneyTransferDto> UpdateAsync(UpdateMoneyTransferDto input)
        {
            return new MoneyTransferDto();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<MoneyTransferDto> CreateAsync(CreateMoneyTransferDto input)
        {
            return new MoneyTransferDto();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task InsertNewMoneyTransfer(MoneyTransfer input)
        {
            await Repository.InsertAsync(input);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

    }
}
