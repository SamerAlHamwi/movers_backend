using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Mediator.Dto;
using Mofleet.Domain.Mediators;
using Mofleet.Domain.Mediators.Dto;
using Mofleet.Domain.Mediators.Mangers;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Localization.SourceFiles;
using Mofleet.Mediators.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Mediators
{

    /// <summary>
    /// 
    /// </summary>
    public class MediatorAppService : MofleetAsyncCrudAppService<Mediator, MediatorDetailsDto, int, MediatorDetailsDto,
        PagedMediatiorResultRequestDto, CreateMediatorDto, UpdateMediatorDto>,
        IMediatorAppService
    {
        private readonly IMediatorManager _mediatorManager;
        private readonly IRepository<Mediator, int> _mediatorRepository;
        private readonly ICityManager _cityManager;
        private readonly UserManager _userManager;
        private readonly IRequestForQuotationManager _requestForQuotationManager;

        public MediatorAppService(IRepository<Mediator, int> repository,
            IMediatorManager mediatorManager,
            ICityManager cityManager,
            UserManager userManager,
            IRequestForQuotationManager requestForQuotationManager)
            : base(repository)
        {
            _mediatorManager = mediatorManager;
            _mediatorRepository = repository;
            _cityManager = cityManager;
            _userManager = userManager;
            _requestForQuotationManager = requestForQuotationManager;
        }
        public override async Task<MediatorDetailsDto> GetAsync(EntityDto<int> input)
        {
            Mediator mediator = await _mediatorManager.GetEntityByIdAsync(input.Id);
            var mediatorDto = ObjectMapper.Map<MediatorDetailsDto>(mediator);
            mediatorDto.City = await _cityManager.GetEntityDtoByIdAsync(mediator.CityId.Value);
            var usersIds = await _userManager.Users.Where(x => x.MediatorCode == mediator.MediatorCode).Select(x => x.Id).ToListAsync();
            mediatorDto.CountRegisteredUsers = await _userManager.Users.Where(x => x.MediatorCode == mediator.MediatorCode).CountAsync();
            mediatorDto.NumberServiceUsers = await _requestForQuotationManager.GetCountOfFinishedRequestUsersIds(usersIds);
            return mediatorDto;
        }
        public override async Task<PagedResultDto<MediatorDetailsDto>> GetAllAsync(PagedMediatiorResultRequestDto input)
        {
            try
            {
                var result = await base.GetAllAsync(input);
                foreach (var item in result.Items)
                {
                    var usersIds = await _userManager.Users.Where(x => x.MediatorCode == item.MediatorCode).Select(x => x.Id).ToListAsync();
                    item.CountRegisteredUsers = await _userManager.Users.Where(x => x.MediatorCode == item.MediatorCode).CountAsync();
                    item.NumberServiceUsers = await _requestForQuotationManager.GetCountOfFinishedRequestUsersIds(usersIds);
                }
                return result;
            }
            catch (Exception ex) { throw; }
        }
        [AbpAuthorize(PermissionNames.Broker_FullControl)]
        public override async Task<MediatorDetailsDto> CreateAsync(CreateMediatorDto input)
        {
            try
            {
                CheckCreatePermission();
                if (!await _cityManager.CheckIfCityIsExist(input.CityId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.City);
                if (await _mediatorManager.CheckIfMediatorExist(input.DialCode, input.MediatorPhoneNumber))
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Mediator));
                if (await _mediatorManager.CheckIfMediatorCodeExist(input.MediatorCode))
                    throw new UserFriendlyException(string.Format(Exceptions.TheCodeIsAlreadyUsed, Tokens.Mediator));
                Mediator mediator = ObjectMapper.Map<Mediator>(input);
                mediator.IsActive = true;
                await _mediatorRepository.InsertAsync(mediator);

                var user = await _userManager.Users.Where(x => x.PhoneNumber == mediator.MediatorPhoneNumber && x.DialCode == mediator.DialCode).FirstOrDefaultAsync();
                if (user is not null && user.Type == UserType.BasicUser)
                {
                    user.Type = UserType.MediatorUser;
                    await _userManager.UpdateAsync(user);
                }
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return MapToEntityDto(mediator);
            }
            catch (Exception ex) { throw new UserFriendlyException(ex.Message + " " + ex.InnerException); }
        }
        [AbpAuthorize(PermissionNames.Broker_FullControl)]
        public override async Task<MediatorDetailsDto> UpdateAsync(UpdateMediatorDto input)
        {
            CheckUpdatePermission();
            Mediator mediator = await _mediatorManager.GetEntityByIdAsync(input.Id);
            if (mediator is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Mediator));
            if (mediator.MediatorCode != input.MediatorCode)
            {
                if (await _mediatorManager.CheckIfMediatorCodeExist(input.MediatorCode))
                    throw new UserFriendlyException(string.Format(Exceptions.TheCodeIsAlreadyUsed, Tokens.Mediator));
            }
            MapToEntity(input, mediator);
            mediator.LastModificationTime = DateTime.UtcNow;
            await _mediatorRepository.UpdateAsync(mediator);
            return MapToEntityDto(mediator);

        }
        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Mediator> ApplySorting(IQueryable<Mediator> query, PagedMediatiorResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }
        protected override IQueryable<Mediator> CreateFilteredQuery(PagedMediatiorResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.City).ThenInclude(x => x.Translations);

            if (!input.Keyword.IsNullOrEmpty())
            {

                var keyword = input.Keyword.ToLower();
                data = data.Where(x => x.CompanyName.Contains(keyword)
                || x.Email.ToLower().Contains(keyword)
                || x.FirstName.ToLower().Contains(keyword)
                || x.LastName.ToLower().Contains(keyword)
                || x.MediatorPhoneNumber.ToLower().Contains(keyword)
                || x.MediatorCode.ToLower().Contains(keyword)
                || x.DialCode.ToLower().Contains(keyword)
                || x.City.Translations.Any(x => x.Name.ToLower().Contains(keyword))
                );

            }
            if (input.IsActive.HasValue)
                data = data.Where(x => x.IsActive == input.IsActive.Value);
            return data;
        }
        [AbpAuthorize(PermissionNames.Broker_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var mediator = await _mediatorManager.GetEntityByIdAsync(input.Id);
            if (mediator is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Mediator));
            var user = await _userManager.Users.Where(x => x.PhoneNumber == mediator.MediatorPhoneNumber && x.DialCode == mediator.DialCode).FirstOrDefaultAsync();
            if (user is not null && user.Type == UserType.MediatorUser)
            {
                user.Type = UserType.BasicUser;
                await _userManager.UpdateAsync(user);
            }
            await _mediatorRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize]
        public async Task<InfoRegisteredUsersViaBrokerDto> GetInfoRegisteredUsersViaBroker(int mediatorId = 0)
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            if (user.Type != UserType.MediatorUser)
                throw new UserFriendlyException(string.Format(Exceptions.YouCannotDoThisAction, Tokens.Mediator));
            Mediator mediator = await _mediatorManager.GetEntityByPhoneNumberAsync(user.DialCode, user.PhoneNumber);
            var usersIds = await _userManager.Users.Where(x => x.MediatorCode == mediator.MediatorCode).Select(x => x.Id).ToListAsync();
            var result = new InfoRegisteredUsersViaBrokerDto();
            result.CountRegisteredUsers = await _userManager.Users.Where(x => x.MediatorCode == mediator.MediatorCode).CountAsync();
            result.NumberServiceUsers = await _requestForQuotationManager.GetCountOfFinishedRequestUsersIds(usersIds);
            result.BrokerCode = mediator.MediatorCode;
            result.MoneyOwed = mediator.MoneyOwed;
            return result;

        }
        [HttpGet]
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<List<StatisticsRegisteredUsersViaBrokers>> StatisticsRegisteredUsersViaBrokers()
        {
            return await _mediatorManager.StatisticsRegisteredUsersViaBrokers();
        }
    }
}
