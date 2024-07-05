using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.Points;
using Mofleet.Domain.Points.Dto;
using Mofleet.Domain.PointsValues;
using Mofleet.Localization.SourceFiles;
using Mofleet.MoneyTransfers;
using Mofleet.Points.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Points
{
    public class PointAppService : MofleetAsyncCrudAppService<Point, PointDetailsDto, int, LitePointDto, PagedPointResultRequestDto,
        CreatePointDto, UpdatePointDto>
    {
        private readonly IPointManager _pointManager;
        private readonly UserManager _userManager;
        private readonly ICompanyManager _companyManager;
        private readonly ICompanyBranchManager _companyBranchManager;
        private readonly IPointsValueManager _pointsValueManager;
        private readonly IRepository<PointTranslation> _pointTranslationRepository;
        private readonly IMoneyTransferAppService _moneyTransferAppService;
        public PointAppService(IRepository<Point, int> repository, IPointManager pointManager, UserManager userManager,
            ICompanyManager companyManager,
            ICompanyBranchManager companyBranchManager,
            IPointsValueManager pointsValueManager,
            IRepository<PointTranslation> pointTranslationRepository, IMoneyTransferAppService moneyTransferAppService)
            : base(repository)
        {
            _pointManager = pointManager;
            _userManager = userManager;
            _companyManager = companyManager;
            _companyBranchManager = companyBranchManager;
            _pointsValueManager = pointsValueManager;
            _pointTranslationRepository = pointTranslationRepository;
            _moneyTransferAppService = moneyTransferAppService;
        }

        public override async Task<PagedResultDto<LitePointDto>> GetAllAsync(PagedPointResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }

        public override async Task<PointDetailsDto> GetAsync(EntityDto<int> input)
        {
            CheckCreatePermission();
            var point = await _pointManager.GetEntityByIdAsync(input.Id);
            return MapToEntityDto(point);
        }
        [AbpAuthorize(PermissionNames.Points_FullControl)]
        public override async Task<PointDetailsDto> CreateAsync(CreatePointDto input)
        {
            CheckCreatePermission();
            var point = ObjectMapper.Map<Point>(input);
            point.CreationTime = DateTime.UtcNow;
            point.IsActive = true;
            await Repository.InsertAsync(point);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(point);
        }
        /// <summary>
        /// Update Point Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Points_FullControl)]
        public override async Task<PointDetailsDto> UpdateAsync(UpdatePointDto input)
        {
            CheckUpdatePermission();
            var Point = await _pointManager.GetEntityByIdAsync(input.Id);
            if (Point is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Entity));
            Point.Translations.Clear();
            MapToEntity(input, Point);
            await Repository.UpdateAsync(Point);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return MapToEntityDto(Point);
        }


        /// <summary>
        /// Delete A Point 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Points_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var Point = await _pointManager.GetEntityByIdAsync(input.Id);
            if (Point is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Entity));

            foreach (var translation in Point.Translations.ToList())
            {
                await _pointTranslationRepository.DeleteAsync(translation);
            }

            await Repository.DeleteAsync(input.Id);
        }
        [HttpPost]
        [AbpAuthorize]
        public async Task<bool> BuyBundles(BuyPointsInputDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                var bundle = await _pointManager.GetEntityByIdAsync(input.Id);
                if (bundle.IsForFeature)
                    throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);
                /*
                //payment
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.CreateAsync(
                        new PaymentIntentCreateOptions
                        {
                            Amount = (long)bundle.Price * 100,
                            Currency = "AED",
                            PaymentMethod = input.PaymentMethodId,
                            PaymentMethodTypes = new List<string> { "card", }
                        });
                await paymentIntentService.ConfirmAsync(paymentIntent.Id);
                */
                var moneyTransfer = new MoneyTransfer()
                {
                    Amount = bundle.Price,
                    PaidStatues = PaidStatues.Finish,
                    ReasonOfPaid = ReasonOfPaid.BuyBundle,
                    PaidDestination = PaidDestination.OnHim
                };

                PointsValue pointsValue = new PointsValue()
                {
                    PointId = input.Id,

                };
                if (user.Type == UserType.CompanyUser)
                {
                    var companyId = await _companyManager.GetCompnayIdByUserId(user.Id);
                    pointsValue.CompanyId = companyId;
                    moneyTransfer.CompanyId = companyId;
                    moneyTransfer.PaidProvider = PaidProvider.CompanyUser;
                    await _companyManager.AddPaidPointsToCompany(bundle.NumberOfPoint, companyId);
                }
                else if (user.Type == UserType.CompanyBranchUser)
                {
                    var companyBranchId = await _companyBranchManager.GetCompnayBranchIdByUserId(user.Id);
                    pointsValue.CompanyBranchId = companyBranchId;
                    moneyTransfer.CompanyBranchId = companyBranchId;
                    moneyTransfer.PaidProvider = PaidProvider.CompanyBranchUser;
                    await _companyBranchManager.AddPaidPointsToCompanyBrnach(bundle.NumberOfPoint, companyBranchId);
                }
                else
                    throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);
                await _pointsValueManager.InsertPointsValue(pointsValue);
                await _moneyTransferAppService.InsertNewMoneyTransfer(moneyTransfer);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return true;
            }
        }
        [HttpPost]
        [AbpAuthorize]
        public async Task<bool> BuyFeatureBundles(BuyPointsInputDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                try
                {
                    var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                    if (user.Type is not (UserType.CompanyBranchUser or UserType.CompanyUser))
                        throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);

                    var bundle = await _pointManager.GetEntityByIdAsync(input.Id);
                    if (!bundle.IsForFeature)
                        throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);
                    /*
                    //payment
                    var paymentIntentService = new PaymentIntentService();
                    var paymentIntent = await paymentIntentService.CreateAsync(
                            new PaymentIntentCreateOptions
                            {
                                Amount = (long)bundle.Price,
                                Currency = "AED",
                                PaymentMethod = input.PaymentMethodId,
                                PaymentMethodTypes = new List<string> { "card", }
                            });
                    await paymentIntentService.ConfirmAsync(paymentIntent.Id);
                    */
                    var moneyTransfer = new MoneyTransfer()
                    {
                        Amount = bundle.Price,
                        PaidStatues = PaidStatues.Finish,
                        ReasonOfPaid = ReasonOfPaid.BuyFeatureBundle,
                        PaidDestination = PaidDestination.OnHim
                    };
                    PointsValue pointsValue = new PointsValue()
                    {
                        PointId = input.Id,
                        IsForFeature = true,

                    };
                    if (user.Type == UserType.CompanyUser)
                    {
                        var companyId = await _companyManager.GetCompnayIdByUserId(user.Id);
                        pointsValue.CompanyId = companyId;
                        moneyTransfer.CompanyId = companyId;
                        moneyTransfer.PaidProvider = PaidProvider.CompanyUser;
                        await _companyManager.CheckIfCompanyIsFeature(pointsValue.CompanyId.Value);
                        await _companyManager.MakeCompanyAsFeature(bundle.NumberInMonths, companyId);
                    }
                    else
                    {
                        var companyBranchId = await _companyBranchManager.GetCompnayBranchIdByUserId(user.Id);
                        pointsValue.CompanyBranchId = companyBranchId;
                        moneyTransfer.CompanyBranchId = companyBranchId;
                        moneyTransfer.PaidProvider = PaidProvider.CompanyBranchUser;
                        await _companyBranchManager.CheckIfCompanyBranchIsFeature(pointsValue.CompanyBranchId.Value);
                        await _companyBranchManager.MakeCompanyBranchAsFeature(bundle.NumberInMonths, companyBranchId);
                    }
                    await _pointsValueManager.InsertPointsValue(pointsValue);
                    await _moneyTransferAppService.InsertNewMoneyTransfer(moneyTransfer);
                    await UnitOfWorkManager.Current.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex) { throw; }
            }

        }
        /// <summary>
        /// Filter for  A Group of Point
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Point> CreateFilteredQuery(PagedPointResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations);
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any());
            if (input.IsActive.HasValue)
                data = data.Where(x => x.IsActive == input.IsActive.Value);
            data = data.Where(x => x.IsForFeature == input.IsForFeature);

            return data;
        }
        /// <summary>
        /// Sort Filtered Cities
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Point> ApplySorting(IQueryable<Point> query, PagedPointResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }
        /// <summary>
        /// Switch Activation of A Point
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        [AbpAuthorize(PermissionNames.Points_FullControl)]
        public async Task<PointDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            CheckUpdatePermission();
            var Point = await Repository.GetAsync(input.Id);
            Point.IsActive = input.IsActive;
            Point.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(Point);
            return MapToEntityDto(Point);
        }

    }
}
