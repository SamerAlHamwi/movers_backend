using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.Codes;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.Offers;
using Mofleet.Domain.Offers.Dto;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.Reviews;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.ServiceValues.Dto;
using Mofleet.Localization.SourceFiles;
using Mofleet.MoneyTransfers;
using Mofleet.NotificationSender;
using Mofleet.RequestForQuotations;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;
using Review = Mofleet.Domain.Reviews.Review;

namespace ClinicSystem.Offers
{
    public class OfferAppService : MofleetAsyncCrudAppService<Offer, OfferDetailsDto, Guid, LiteOfferDto,
        PagedOfferResultRequestDto, CreateOfferDto, UpdateOfferDto>,
        IOfferAppService
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager _userManager;
        private readonly IReviewManager _reviewManager;
        private readonly ISelectedCompaniesBySystemForRequestManager _selectedCompaniesForRequestManager;
        private readonly IOfferManager _offerManager;
        private readonly INotificationSender _notificationSender;
        private readonly IServiceValueForOfferManager _serviceValueForOfferManager;
        private readonly IMapper _mapper;
        private readonly IRejectReasonManager _rejectReasonManager;
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly IServiceValueManager _serviceValueManager;
        private readonly ICompanyManager _companyManager;
        private readonly IRequestForQuotationAppService _requestForQuotationAppService;
        private readonly ICompanyBranchManager _companyBranchManager;
        private readonly IMoneyTransferAppService _moneyTransferAppService;
        private readonly ICodeManager _codeManager;

        public OfferAppService(IRepository<Offer, Guid> repository,
            IServiceManager serviceManager,
            UserManager userManager,
            IReviewManager reviewManager,
            ISelectedCompaniesBySystemForRequestManager selectedCompaniesForRequestManager,
            IServiceValueForOfferManager serviceValueForOfferManager,
            IMapper mapper,
            IRejectReasonManager rejectReasonManager,
            IOfferManager offerManager, INotificationSender notificationSender,
            IRequestForQuotationManager requestForQuotationManager,
            IServiceValueManager serviceValueManager,
            ICompanyManager companyManager, ICodeManager codeManager,
            IRequestForQuotationAppService requestForQuotationAppService,
            ICompanyBranchManager companyBranchManager, IMoneyTransferAppService moneyTransferAppService)
            : base(repository)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
            _reviewManager = reviewManager;
            _selectedCompaniesForRequestManager = selectedCompaniesForRequestManager;
            _serviceValueForOfferManager = serviceValueForOfferManager;
            _mapper = mapper;
            _rejectReasonManager = rejectReasonManager;
            _offerManager = offerManager;
            _notificationSender = notificationSender;
            _requestForQuotationManager = requestForQuotationManager;
            _serviceValueManager = serviceValueManager;
            _companyManager = companyManager;
            _requestForQuotationAppService = requestForQuotationAppService;
            _companyBranchManager = companyBranchManager;
            _moneyTransferAppService = moneyTransferAppService;
            _codeManager = codeManager;
        }

        /// <summary>
        /// Get Offer Details ById
        /// </summary>
        /// /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<OfferDetailsDto> GetAsync(EntityDto<Guid> input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                return await _offerManager.GetOfferDetailsDtoById(input.Id, AbpSession.UserId.Value);
            }
        }
        //}
        /// <summary>
        /// Get All Offers Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteOfferDto>> GetAllAsync(PagedOfferResultRequestDto input)
        {

            try
            {
                return await base.GetAllAsync(input);
            }
            catch (Exception ex)
            { throw new UserFriendlyException(ex.Message + " " + ex.InnerException); }
        }
        /// <summary>
        /// 
        /// Add New Offer Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Offer_Create)]
        public override async Task<OfferDetailsDto> CreateAsync(CreateOfferDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                CheckCreatePermission();
                await _selectedCompaniesForRequestManager.CheckIfEntityExictByRequestId(input.RequestId);
                await _serviceManager.CheckServicesIsCorrect(ObjectMapper.Map(input.ServiceValueForOffers, new List<ServiceValuesDto>()));
                var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                //if (user.Type is not (UserType.CompanyUser or UserType.CompanyBranchUser))
                //    throw new UserFriendlyException(403, Exceptions.YouCannotDoThisAction);
                Offer offer = _mapper.Map<Offer>(input);
                offer.SelectedCompaniesId = await _selectedCompaniesForRequestManager.GetEntityIdByRequestId(input.RequestId, user);
                offer.Statues = OfferStatues.Checking;
                if (user.Type == UserType.CompanyUser)
                    offer.Provider = OfferProvider.Company;
                else
                    offer.Provider = OfferProvider.BranchCompany;
                await Repository.InsertAsync(offer);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                var result = MapToEntityDto(offer);
                return result;
            }
        }
        /// <summary>
        /// Update Offer Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Offer_Update)]
        public override async Task<OfferDetailsDto> UpdateAsync(UpdateOfferDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                try
                {
                    CheckUpdatePermission();
                    var offer = await _offerManager.GetEntityByIdAsync(input.Id);
                    var user = await _userManager.GetUserByIdAsync(offer.CreatorUserId.Value);
                    //if (user.Id != offer.CreatorUserId || (user.Type is not UserType.CompanyUser or UserType.CompanyBranchUser))
                    //    throw new UserFriendlyException(403, Exceptions.YouCannotDoThisAction);
                    await _selectedCompaniesForRequestManager.CheckIfEntityExictByRequestId(input.RequestId);
                    await _serviceManager.CheckServicesIsCorrect(ObjectMapper.Map(input.ServiceValueForOffers, new List<ServiceValuesDto>()));
                    await _serviceValueForOfferManager.HardDeleteServiceValuesForOffer(offer.ServiceValueForOffers.ToList());
                    offer = _mapper.Map<UpdateOfferDto, Offer>(input, offer);
                    offer.SelectedCompaniesId = await _selectedCompaniesForRequestManager.GetEntityIdByRequestId(input.RequestId, user);
                    offer.Statues = OfferStatues.Checking;
                    await Repository.UpdateAsync(offer);
                    await UnitOfWorkManager.Current.SaveChangesAsync();
                    var result = MapToEntityDto(offer);
                    result.ServiceValueForOffers = await _serviceManager.GetFullServicesForOffer(offer.ServiceValueForOffers.ToList());
                    return result;

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Delete Offer Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public override async Task DeleteAsync(EntityDto<int> input)
        //{
        //    CheckDeletePermission();
        //    var Offer = await _OfferManager.GetEntityByIdAsync(input.Id);
        //    if (Offer is null)
        //        throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Offer));
        //    foreach (var translation in Offer.Translations.ToList())
        //    {
        //        await _OfferTranslationRepository.DeleteAsync(translation);
        //        Offer.Translations.Remove(translation);
        //    }
        //    await _OfferRepository.DeleteAsync(Offer);
        //}

        /// <summary>
        /// Filter For A Group Of Offers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Offer> CreateFilteredQuery(PagedOfferResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.SelectedCompanies).ThenInclude(x => x.RequestForQuotation)
                .ThenInclude(x => x.User)
                .AsNoTracking();
            data = data.Include(x => x.SelectedCompanies).ThenInclude(x => x.Company).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.SelectedCompanies).ThenInclude(x => x.CompanyBranch).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.SelectedCompanies).ThenInclude(x => x.Company).ThenInclude(x => x.CompanyContact).AsNoTracking();
            data = data.Include(x => x.SelectedCompanies).ThenInclude(x => x.CompanyBranch).ThenInclude(x => x.CompanyContact).AsNoTracking();
            if (input.Statues.HasValue)
                data = data.Where(x => x.Statues == input.Statues.Value);
            if (input.CompanyId.HasValue)
                data = data.Where(x => x.SelectedCompanies.CompanyId == input.CompanyId.Value);
            if (input.CompanyBranchId.HasValue)
                data = data.Where(x => x.SelectedCompanies.CompanyBranchId == input.CompanyBranchId.Value);
            if (input.MyOffers)
                data = data.Where(x => x.SelectedCompanies.RequestForQuotation.UserId == AbpSession.UserId.Value);
            if (input.RequestId.HasValue)
                data = data.Where(x => x.SelectedCompanies.RequestForQuotationId == input.RequestId.Value);
            return data;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Offer> ApplySorting(IQueryable<Offer> query, PagedOfferResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }

        /// <summary>
        /// Approve Offer From Admin
        /// </summary>
        /// <param name="offerIds"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Offer_ChangeStatus)]
        public async Task<bool> ApproveOfferToSendItToUser(List<Guid> offerIds)
        {
            var offers = await _offerManager.GetFullEntitiesByIdAsync(offerIds);
            var request = offers.Select(r => r.SelectedCompanies.RequestForQuotation).FirstOrDefault();
            long userId = 0;
            foreach (var offer in offers)
            {
                if (userId is 0)
                    userId = request.UserId;
                offer.Statues = OfferStatues.Approved;
                offer.LastModificationTime = DateTime.Now;
                await Repository.UpdateAsync(offer);
            }
            await _requestForQuotationManager.MakeRequestHasOffers(request);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            await _notificationSender.SendNotificationForNotifyUserForNewOffer(new List<long> { userId });
            return true;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        [HttpPut]
        [AbpAuthorize(PermissionNames.Offer_Take)]
        public async Task<bool> TakeOfferFromUserAsync(Guid offerId, string paymentMethodId, string rSMcode)
        {
            var offer = await _offerManager.GetEntityByIdAsync(offerId);
            double price = offer.Price;

            if (!string.IsNullOrWhiteSpace(rSMcode))
            {
                var code = await _codeManager.GetCodeByRSMCode(rSMcode);
                if (code.CodeType == CodeType.DiscountPercentageValue)
                    price = offer.Price - ((offer.Price * (double)code.DiscountPercentage) / 100);
                else
                    price = offer.Price - (double)code.DiscountPercentage;
            }
            //Payment
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(
                    new PaymentIntentCreateOptions
                    {
                        Amount = (long)price * 100,
                        Currency = "AED",
                        PaymentMethod = paymentMethodId,
                        PaymentMethodTypes = new List<string> { "card", }
                    });
            await paymentIntentService.ConfirmAsync(paymentIntent.Id);
            var moneyTransfer = new MoneyTransfer()
            {
                Amount = price,
                PaidStatues = PaidStatues.Finish,
                ReasonOfPaid = ReasonOfPaid.PayForOffer,
                PaidProvider = PaidProvider.User,
                UserId = AbpSession.UserId.Value,
                PaidDestination = PaidDestination.OnHim,
                OfferId = offer.Id.ToString(),
            };
            offer.Statues = OfferStatues.SelectedByUser;
            offer.LastModificationTime = DateTime.Now;
            await Repository.UpdateAsync(offer);
            long userId;
            if (offer.Provider == OfferProvider.Company)
            {
                userId = await _companyManager.GetUserIdByCompanyIdAsync(offer.SelectedCompanies.CompanyId.Value);
            }
            else
            {
                var companyBranch = await _companyBranchManager.GetSuperLiteEntityByIdAsync(offer.SelectedCompanies.CompanyBranchId.Value);
                userId = companyBranch.UserId.Value;
            }
            await _notificationSender.SendNotificationToCompanyOrCompanyBranchForSelectedOfferByUser(new List<long> { userId }, offerId);
            await _requestForQuotationManager.MakeRequestInProcessAfterUserTakeOffer(offer.SelectedCompanies.RequestForQuotation);
            await _offerManager.MakeOtherOffersRejectedWhenUserTakeOffer(offerId, offer.SelectedCompanies.RequestForQuotationId);
            await _moneyTransferAppService.InsertNewMoneyTransfer(moneyTransfer);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }
        [AbpAuthorize(PermissionNames.Offer_ChangeStatus)]
        public async Task<OutPutBooleanStatuesDto> RejectOfferToEditItByAdmin(RejectOfferToEditItInputDto input)
        {
            Offer offer = await Repository.GetAsync(input.OfferId);
            offer.Statues = OfferStatues.RejectedNeedToEdit;
            offer.ReasonRefuse = input.ReasonRefuse;
            await Repository.UpdateAsync(offer);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto()
            {
                BooleanStatues = true
            };
        }

        /// <summary>
        /// Reject Offers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [AbpAuthorize]
        public async Task<bool> RejectOffersByUser(RejectOffersInputDto input)
        {
            await _rejectReasonManager.CheckIfRejectReasonIsExist(input.RejectReasonId);
            var offers = await _offerManager.GetOffersWhichSentToUser(input.OffersIds, AbpSession.UserId.Value);
            if (offers is null || offers.Count() == 0)
                throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.Offer);
            foreach (var offer in offers)
            {
                offer.RejectReasonDescription = input.RejectReasonDescription;
                offer.RejectReasonId = input.RejectReasonId;
                offer.Statues = OfferStatues.RejectedByUser;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            if (await _rejectReasonManager.GetRejectResonTypeByIdAsync(input.RejectReasonId) == PossibilityPotentialClient.PotentialClient)
            {
                await NoticOtherCompaniesForPossibleRequest(offers);
                await _requestForQuotationManager.MakeRequestAsPossible(offers.Select(x => x.SelectedCompanies.RequestForQuotation).FirstOrDefault());
            }
            else
                await _requestForQuotationManager.MakeRequestCancelledByUserAfterRekectedAllOffers(offers.Select(x => x.SelectedCompanies.RequestForQuotation).FirstOrDefault());
            return true;
        }
        [AbpAuthorize]
        public async Task<AmountOutPutDto> CalucateCostOfStorage(CalucateStorageInputDto input)
        {
            var offer = await Repository.GetAsync(input.OfferId);
            if (!offer.IsExtendStorage)
                throw new UserFriendlyException(Exceptions.IncompatibleValue, Tokens.Offer);
            var numberOfDays = (input.RequestedMoveArriveAt.Date - input.CurrentMoveArriveAt.Date).TotalDays;
            return new AmountOutPutDto
            {
                Amount = offer.PriceForOnDayStorage.Value * numberOfDays,
            };
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task NoticOtherCompaniesForPossibleRequest(List<Offer> offers)
        {
            var companyIds = offers.Where(x => x.SelectedCompanies.CompanyId.HasValue).Select(x => x.SelectedCompanies.CompanyId.Value).Distinct().ToList();
            var companyBranchIds = offers.Where(x => x.SelectedCompanies.CompanyBranchId.HasValue).Select(x => x.SelectedCompanies.CompanyBranchId.Value).Distinct().ToList();
            var requestId = offers.Select(x => x.SelectedCompanies.RequestForQuotationId).FirstOrDefault();
            var companyIdsCompleteWithRequest = await _serviceValueManager.FilterCompatibleCompaniesWithRequestByServices(requestId, true, true);
            CompanyAndCompanyBranchIdsDto companyBranchIdsCompleteWithRequest = await _serviceValueManager.FilterCompatibleCompaniesWithRequestByServices(requestId, false, true);
            var companyAndBranchToNotic = new CompanyAndCompanyBranchIdsDto
            {
                CompanyIds = companyIdsCompleteWithRequest.CompanyIds.Where(x => !companyIds.Contains(x)).ToList(),
                CompanyBranchIds = companyBranchIdsCompleteWithRequest.CompanyIds.Where(x => !companyBranchIds.Contains(x)).ToList()
            };
            var userIds = await _requestForQuotationAppService.InsertAndNoticFilteredCompanies(companyAndBranchToNotic, requestId, true);
            await _notificationSender.SendNotificationForOtherCompaniesForPossibleRequest(userIds, requestId);

        }

        [Tags("Rate")]
        [AbpAuthorize]
        [HttpPost]
        public async Task<ReviewDetailsDto> RateCompanyOrCompanyBranch(CreateReviewDto input)
        {
            var offer = await _offerManager.GetEntityByIdAsync(input.OfferId);
            Review review = _mapper.Map<Review>(input);

            review.UserId = AbpSession.UserId.Value;

            await _reviewManager.InserReviewToCompanyOrCompanyBranch(review);
            return ObjectMapper.Map<ReviewDetailsDto>(review);
        }
        [Tags("Rate")]
        [AbpAuthorize]
        public async Task<OutPutBooleanStatuesDto> CheckIfUserRateThisOfferOrNot(Guid offerId)
        {
            return await _reviewManager.CheckIfUserRateOfferOrNot(AbpSession.UserId.Value, offerId);
        }
    }
}

