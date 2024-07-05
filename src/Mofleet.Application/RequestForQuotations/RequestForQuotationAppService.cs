using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.Configuration;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.AskForHelps;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.AttributeAndAttachments;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributeForSourceTypeValues;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.Drafts;
using Mofleet.Domain.Mediators.Mangers;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.Offers;
using Mofleet.Domain.PaidRequestPossibles;
using Mofleet.Domain.PaidRequestPossibles.Dto;
using Mofleet.Domain.Partners;
using Mofleet.Domain.RequestForQuotationContacts;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.SearchedPlacesByUsers.Dtos;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.ServiceValues.Dto;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.UserVerficationCodes;
using Mofleet.Localization.SourceFiles;
using Mofleet.NotificationSender;
//using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.RequestForQuotations
{
    public class RequestForQuotationAppService : MofleetAsyncCrudAppService<RequestForQuotation, RequestForQuotationDetailsDto, long, LiteRequestForQuotationDto
        , PagedRequestForQuotationResultRequestDto, CreateRequestForQuotationDto, UpdateRequestForQuotationDto>, IRequestForQuotationAppService
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentManager _attachmentManager;
        private readonly ServiceManager _serviceManager;
        private readonly IAttributeForSourceTypeManager _attributeForSourceTypeManager;
        private readonly AttributeForSourceTypeValueManager _attributeForSourceTypeValueManager;
        private readonly IRequestForQuotationContactManager _requestForQuotationContactManager;
        private readonly CityManager _cityManager;
        private readonly ServiceValueManager _serviceValueManager;
        private readonly IRepository<AttributeChoiceAndAttachment> _attributeChoiceAndAttachmentsRepository;
        private readonly AttributeChoiceAndAttachmentManager _attributeChoiceAndAttachmentsManager;
        private readonly ISourceTypeManager _sourceTypeManager;
        private readonly ICompanyManager _companyManager;
        private readonly IDraftManager _draftManager;
        private readonly IMediatorManager _mediatorManager;
        private readonly IPartnerManager _partnerManager;
        private readonly IRepository<AskForHelp> _askForHelpRepository;
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly IRepository<SelectedCompaniesBySystemForRequest, Guid> _selectedCompaniesBySystemForRequestRepository;
        private readonly ISelectedCompaniesBySystemForRequestManager _selectedCompaniesBySystemRequestManager;
        private readonly INotificationSender _notificationSender;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICompanyBranchManager _companyBranchManager;
        private readonly IPaidRequestPossibleManager _paidRequestPossibleManager;
        private readonly IOfferManager _offerManager;
        private readonly IMoneyTransferManager _moneyTransferManager;
        private readonly ICommissionGroupManager _commissionGroupManager;
        private readonly IAttributeChoiceManger _attributeChoiceManger;
        public RequestForQuotationAppService(IRepository<RequestForQuotation, long> repository,
            IMapper mapper,
            IAttachmentManager attachmentManager,
            ServiceManager serviceManager,
            IAttributeForSourceTypeManager attributeForSourceTypeManager,
            AttributeForSourceTypeValueManager attributeForSourceTypeValueManager,
            IRequestForQuotationContactManager requestForQuotationContactManager,
            CityManager cityManager,
            ServiceValueManager serviceValueManager,
            IRepository<AttributeChoiceAndAttachment> attributeAndChoiceAndAttachmentsRepository,
            AttributeChoiceAndAttachmentManager attributeChoiceAndAttachmentsManager,
            ISourceTypeManager sourceTypeManager,
            ICompanyManager companyManager,
            IDraftManager draftManager,
            IMediatorManager mediatorManager,
            IPartnerManager partnerManager,
            IRepository<AskForHelp> askForHelpRepository,
            IRequestForQuotationManager requestForQuotationManager,
            IRepository<SelectedCompaniesBySystemForRequest, Guid> selectedCompaniesBySystemForRequestRepository,
            ISelectedCompaniesBySystemForRequestManager selectedCompaniesBySystemRequestManager,
            INotificationSender notificationSender,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICompanyBranchManager companyBranchManager,
            IPaidRequestPossibleManager paidRequestPossibleManager,
            IOfferManager offerManager
,
            IMoneyTransferManager moneyTransferManager,
            ICommissionGroupManager commissionGroupManager,
            IAttributeChoiceManger attributeChoiceManger)
            : base(repository)
        {
            _mapper = mapper;
            _attachmentManager = attachmentManager;
            _serviceManager = serviceManager;
            _attributeForSourceTypeManager = attributeForSourceTypeManager;
            _attributeForSourceTypeValueManager = attributeForSourceTypeValueManager;
            _requestForQuotationContactManager = requestForQuotationContactManager;
            _cityManager = cityManager;
            _serviceValueManager = serviceValueManager;
            _attributeChoiceAndAttachmentsRepository = attributeAndChoiceAndAttachmentsRepository;
            _attributeChoiceAndAttachmentsManager = attributeChoiceAndAttachmentsManager;
            _sourceTypeManager = sourceTypeManager;
            _companyManager = companyManager;
            _draftManager = draftManager;
            _mediatorManager = mediatorManager;
            _partnerManager = partnerManager;
            _askForHelpRepository = askForHelpRepository;
            _requestForQuotationManager = requestForQuotationManager;
            _selectedCompaniesBySystemForRequestRepository = selectedCompaniesBySystemForRequestRepository;
            _selectedCompaniesBySystemRequestManager = selectedCompaniesBySystemRequestManager;
            _notificationSender = notificationSender;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _companyBranchManager = companyBranchManager;
            _paidRequestPossibleManager = paidRequestPossibleManager;
            _offerManager = offerManager;
            _moneyTransferManager = moneyTransferManager;
            _commissionGroupManager = commissionGroupManager;
            _attributeChoiceManger = attributeChoiceManger;
        }
        /// <summary>
        /// Try To Push
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [AbpAuthorize(PermissionNames.Request_Get)]
        public override async Task<RequestForQuotationDetailsDto> GetAsync(EntityDto<long> input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                try
                {
                    RequestForQuotation requestForQuotation = await Repository.GetAsync(input.Id);
                    if (requestForQuotation is null)
                        throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.RequestForQuotation);
                    var requestForQuotationDetailsDto = ObjectMapper.Map<RequestForQuotationDetailsDto>(requestForQuotation);
                    requestForQuotationDetailsDto.AttributeForSourceTypeValues = await _attributeForSourceTypeValueManager.GetAllAttributeForSourceTypeValuesByRequestForQuotationId(requestForQuotation.Id);
                    requestForQuotationDetailsDto.SourceType = await _sourceTypeManager.GetEntityDtoByIdAsync(requestForQuotation.SourceTypeId);
                    requestForQuotationDetailsDto.RequestForQuotationContacts = await _requestForQuotationContactManager.GetAllContactsByRequestForQuotationId(requestForQuotation.Id);
                    requestForQuotationDetailsDto.Services = await _serviceValueManager.GetFullServicesByCompanyOrRequestIdAsync(requestForQuotation.Id, false);
                    requestForQuotationDetailsDto.AttributeChoiceAndAttachments = await _attributeChoiceAndAttachmentsManager.GetAttributeChoiceAndAttachmentDetailsAsyncByRequestId(input.Id);
                    requestForQuotationDetailsDto.SourceCity = await _cityManager.GetEntityDtoByIdAsync(requestForQuotation.SourceCityId.Value);
                    requestForQuotationDetailsDto.DestinationCity = await _cityManager.GetEntityDtoByIdAsync(requestForQuotation.DestinationCityId.Value);
                    var ids = requestForQuotationDetailsDto.AttributeChoiceAndAttachments.SelectMany(x => x.Attachments.Select(x => x.Id)).ToList();
                    requestForQuotationDetailsDto.Attachments = await _requestForQuotationManager.GetAttachmentThatOnlyForRequest(input.Id, ids);
                    requestForQuotationDetailsDto.User = ObjectMapper.Map(await _userManager.FindByIdAsync(requestForQuotation.UserId.ToString()), new LiteUserDto());
                    if (requestForQuotation.Statues is (RequestForQuotationStatues.FinishByCompany or RequestForQuotationStatues.Finished or RequestForQuotationStatues.NotFinishByUser))
                    {
                        requestForQuotationDetailsDto.FinishedRequestAttachmentByCompany = await _requestForQuotationManager.GetFinishedAttachmentForRequestByCompany(input.Id);
                    }
                    var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                    var showContact = false;
                    if (requestForQuotation.Statues is (RequestForQuotationStatues.Possible or RequestForQuotationStatues.InProcess) && user.Type is (UserType.CompanyUser or UserType.CompanyBranchUser))
                    {
                        if (user.Type == UserType.CompanyUser)
                            if (await _paidRequestPossibleManager.CheckIfCompanyPaidForRequestContact((await _companyManager.GetCompnayIdByUserId(AbpSession.UserId.Value)), input.Id))
                                showContact = true;
                        if (user.Type == UserType.CompanyBranchUser)
                            if (await _paidRequestPossibleManager.CheckIfCompanyBranchPaidForRequestContact((await _companyBranchManager.GetCompnayBranchIdByUserId(AbpSession.UserId.Value)), input.Id))
                                showContact = true;
                        if (requestForQuotation.Statues is RequestForQuotationStatues.InProcess)
                            if (DateTime.UtcNow.AddHours(48) >= requestForQuotation.MoveAtUtc)
                                showContact = true;

                    }
                    if (!showContact && user.Type
                        is (UserType.CompanyUser or UserType.CompanyBranchUser)
                        && requestForQuotation.Statues is not (RequestForQuotationStatues.Finished
                        or RequestForQuotationStatues.FinishByCompany
                        or RequestForQuotationStatues.NotFinishByUser))
                    {
                        requestForQuotationDetailsDto.User = null;
                        requestForQuotationDetailsDto.RequestForQuotationContacts = null;
                    }
                    if (requestForQuotation.Statues is (RequestForQuotationStatues.InProcess
                        or RequestForQuotationStatues.FinishByCompany
                        or RequestForQuotationStatues.FinishByUser
                        or RequestForQuotationStatues.NotFinishByUser
                        or RequestForQuotationStatues.Finished
                        or RequestForQuotationStatues.CanceledAfterInProcess))
                    {
                        requestForQuotationDetailsDto.SelectedOfferIdAndStatus = await _offerManager.GetOfferIdByRequestId(input.Id);
                        //if (requestForQuotationDetailsDto.SelectedOfferId is not null)
                        //    requestForQuotationDetailsDto.SelectedCompany = await _offerManager.GetSelectedCompanyByOfferId(requestForQuotationDetailsDto.SelectedOfferId.Value);

                    }
                    if (requestForQuotation.Statues is not (RequestForQuotationStatues.Checking or RequestForQuotationStatues.Rejected) && user.Type is (UserType.CompanyUser or UserType.CompanyBranchUser))
                    {
                        var offers = await _offerManager.GetAllSelectedCompaniesWithThisRequest(new List<long> { requestForQuotation.Id });
                        switch (user.Type)
                        {

                            case UserType.CompanyUser:
                                var companyId = await _companyManager.GetCompnayIdByUserId(AbpSession.UserId.Value);
                                if (await _offerManager.CheckIfCompanyOfferHasApprovedWithThisRequest(companyId, requestForQuotation.Id))
                                    requestForQuotationDetailsDto.IsThisRequestOfferWithThisCompany = true;
                                if (offers.Any(x => x.CompanyId == companyId && x.RequestId == requestForQuotation.Id))
                                    requestForQuotationDetailsDto.IsThisCompnayProvideOfferWithThisRequest = true;
                                break;
                            case UserType.CompanyBranchUser:
                                var companyBranchId = await _companyBranchManager.GetCompnayBranchIdByUserId(AbpSession.UserId.Value);
                                if (await _offerManager.CheckIfCompanyBranchOfferHasApprovedWithThisRequest(companyBranchId, requestForQuotation.Id))
                                    requestForQuotationDetailsDto.IsThisRequestOfferWithThisCompany = true;
                                if (offers.Any(x => x.CompanyBranchId == companyBranchId && x.RequestId == requestForQuotation.Id))
                                    requestForQuotationDetailsDto.IsThisCompnayProvideOfferWithThisRequest = true;
                                break;
                        }
                        var providerOffer = await _offerManager.GetOfferIdByUserIdAndRequstIdAsync(input.Id, AbpSession.UserId.Value, user.Type);
                        if (providerOffer is not null)
                        {
                            requestForQuotationDetailsDto.ProviderOfferId = providerOffer.SelectedOfferId;
                            requestForQuotationDetailsDto.OfferStatues = providerOffer.OfferStatues;
                        }

                    }
                    requestForQuotationDetailsDto.DiscountPercentageIfUserCancelHisRequest = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.DiscountPercentageIfUserCancelHisRequest);
                    return requestForQuotationDetailsDto;
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException(ex.Message);
                }
            }
        }
        [AbpAuthorize(PermissionNames.Request_Create)]
        [HttpPost]
        public override async Task<RequestForQuotationDetailsDto> CreateAsync(CreateRequestForQuotationDto input)
        {
            try
            {
                if (!await _cityManager.CheckIfCityIsExist(input.SourceCityId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.City + " " + "SourceCity");
                if (!await _cityManager.CheckIfCityIsExist(input.DestinationCityId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.City + " " + "DestinationCity");
                if (!await _sourceTypeManager.CheckIfSourceTypeIsExist(input.SourceTypeId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.SourceType);
                await _serviceManager.CheckServicesIsCorrect(input.Services);
                await _attributeForSourceTypeManager.CheckAttributeForSourceTypeIsCorrect(input.AttributeForSourceTypeValues);
                var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                if (input.RequestForQuotationContacts.Count() == 1)
                {
                    input.RequestForQuotationContacts.Add(new CreateRequestForQuotationContactDto
                    {
                        DailCode = input.RequestForQuotationContacts.FirstOrDefault().DailCode,
                        FullName = input.RequestForQuotationContacts.FirstOrDefault().FullName,
                        PhoneNumber = input.RequestForQuotationContacts.FirstOrDefault().PhoneNumber,
                        RequestForQuotationContactType = RequestForQuotationContactType.Destination
                    });
                }
                RequestForQuotation requestForQuotation = ObjectMapper.Map<RequestForQuotation>(input);
                if ((user.Type == UserType.Admin || user.Type == UserType.CustomerService))
                {
                    if (!input.UserId.HasValue)
                        throw new UserFriendlyException(Exceptions.UserIdIsRequird);
                    else requestForQuotation.UserId = input.UserId.Value;
                }
                else requestForQuotation.UserId = AbpSession.UserId.Value;

                requestForQuotation.Services = null;
                requestForQuotation.AttributeChoiceAndAttachments = null;
                requestForQuotation.Statues = RequestForQuotationStatues.Checking;
                var requestForQuotationId = await Repository.InsertAndGetIdAsync(requestForQuotation);
                List<AttributeChoiceAndAttachment> attributesAndAttachments = new List<AttributeChoiceAndAttachment>();
                foreach (var item in input.AttributeChoiceAndAttachments)
                {
                    List<Attachment> attachments = new List<Attachment>();
                    if (item.AttachmentIds is not null && item.AttachmentIds.Count > 0)
                    {
                        foreach (var attachmentId in item.AttachmentIds)
                        {
                            var attachment = await _attachmentManager.CheckAndUpdateRefIdAsync(
                                     attachmentId, AttachmentRefType.RequestForQuotation, requestForQuotationId, true, true);

                            attachments.Add(attachment);

                        }
                        if (item.AttributeChoiceId != null)
                            attributesAndAttachments.Add(new AttributeChoiceAndAttachment()
                            {
                                AttributeChoiceId = item.AttributeChoiceId.Value,
                                RequestForQuotationId = requestForQuotationId,
                                Attachments = attachments
                            });
                    }
                    else
                        continue;
                }
                await _serviceValueManager.InsertServiceValuesForUser(input.Services, requestForQuotationId);
                if (attributesAndAttachments is not null && attributesAndAttachments.Count > 0)
                    await _attributeChoiceAndAttachmentsRepository.InsertRangeAsync(attributesAndAttachments);

                if (input.DraftId.HasValue)
                    await _draftManager.HardDeleteDraftById(input.DraftId.Value);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return ObjectMapper.Map<RequestForQuotationDetailsDto>(requestForQuotation);
            }
            catch (Exception ex) { throw new UserFriendlyException(ex.Message + " " + ex.InnerException); }
        }
        [AbpAuthorize(PermissionNames.Request_Update)]
        public override async Task<RequestForQuotationDetailsDto> UpdateAsync(UpdateRequestForQuotationDto input)
        {
            try
            {
                RequestForQuotation request = await _requestForQuotationManager.GetEntityById(input.Id);
                if (!await _cityManager.CheckIfCityIsExist(input.SourceCityId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.City + " " + "SourceCity");
                if (!await _cityManager.CheckIfCityIsExist(input.DestinationCityId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.City + " " + "DestinationCity");
                if (!await _sourceTypeManager.CheckIfSourceTypeIsExist(input.SourceTypeId))
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.SourceType);
                await _serviceManager.CheckServicesIsCorrect(input.Services);

                //await _requestForQuotationManager.UpdateContcatRequestForQuotation(input.RequestForQuotationContacts, request);
                //input.RequestForQuotationContacts = null;
                var oldAttributeSourceTypeValues = request.AttributeForSourceTypeValues.ToList();
                var oldSeviceValues = request.Services.ToList();
                var userId = request.UserId;
                if (input.RequestForQuotationContacts.Count() == 1)
                {
                    input.RequestForQuotationContacts.Add(new CreateRequestForQuotationContactDto
                    {
                        DailCode = input.RequestForQuotationContacts.FirstOrDefault().DailCode,
                        FullName = input.RequestForQuotationContacts.FirstOrDefault().FullName,
                        PhoneNumber = input.RequestForQuotationContacts.FirstOrDefault().PhoneNumber,
                        RequestForQuotationContactType = RequestForQuotationContactType.Destination
                    });
                }
                _mapper.Map<UpdateRequestForQuotationDto, RequestForQuotation>(input, request);
                request.UserId = userId;
                request.Statues = RequestForQuotationStatues.Checking;
                request.AttributeChoiceAndAttachments = null;
                await _requestForQuotationManager.UpdateAttacmentForRequestForQuotation(request.Id, input.AttributeChoiceAndAttachments);
                await _attributeForSourceTypeValueManager.HardDeleteEntity(oldAttributeSourceTypeValues);
                await _serviceValueManager.HardDeleteServiceValues(oldSeviceValues);
                request.Services = null;
                await Repository.UpdateAsync(request);
                await _serviceValueManager.InsertServiceValuesForUser(input.Services, request.Id);

                UnitOfWorkManager.Current.SaveChanges();
                return MapToEntityDto(request);

            }
            catch (Exception e) { throw; }
        }

        [AbpAuthorize(PermissionNames.Request_List)]
        public override async Task<PagedResultDto<LiteRequestForQuotationDto>> GetAllAsync(PagedRequestForQuotationResultRequestDto input)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                try
                {
                    //var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                    if (input.CompanyId.HasValue && input.GetPossibleRequest)
                    {
                        var company = await _companyManager.GetSuperLiteEntityByIdAsync(input.CompanyId.Value);
                        input.AcceptPossibleRequest = company.AcceptPossibleRequests;
                    }
                    if (input.CompanyBranchId.HasValue && input.GetPossibleRequest)
                    {
                        var companyBranch = await _companyBranchManager.GetSuperLiteEntityByIdAsync(input.CompanyBranchId.Value);
                        input.AcceptPossibleRequest = companyBranch.AcceptPossibleRequests;
                    }
                    var result = await base.GetAllAsync(input);
                    var sourceTypes = await _sourceTypeManager.GetAllSourceTypesIntoDto();
                    var requestForQuotationIds = result.Items.Select(x => x.Id).ToList();
                    var serviceIDsAndRequestIdsDto = await _serviceValueManager.GetServicesIdsAndRequestIdsAsync(requestForQuotationIds);
                    var services = await _serviceManager.GetAllServicesDtosAsync();
                    var attachmentsForSourceTypes = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.SourceTypeIcon);
                    List<RequestCompanyCompanyBranchIdsDto> offers = new List<RequestCompanyCompanyBranchIdsDto>();
                    if (input.CompanyBranchId.HasValue || input.CompanyId.HasValue)
                        offers = await _offerManager.GetAllSelectedCompaniesWithThisRequest(result.Items
                            .Where(x => x.Statues is not (RequestForQuotationStatues.Rejected or RequestForQuotationStatues.Checking))
                            .Select(x => x.Id).ToList());
                    var discount = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.DiscountPercentageIfUserCancelHisRequest);
                    foreach (var item in result.Items)
                    {
                        item.SourceType = sourceTypes.Where(x => x.Id == item.SourceTypeId).FirstOrDefault();
                        if (item.SourceType is not null)
                        {
                            var attachment = attachmentsForSourceTypes.Where(x => x.RefId == item.SourceType.Id).FirstOrDefault();
                            if (attachment is not null)
                            {
                                item.SourceType.Attachment = new LiteAttachmentDto()
                                {
                                    Id = attachment.Id,
                                    Url = _attachmentManager.GetUrl(attachment),
                                    LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment)
                                };
                            }
                        }
                        var ids = serviceIDsAndRequestIdsDto.Where(x => x.RequestId == item.Id).SelectMany(x => x.ServiceIds).ToList();
                        item.Services = services.Where(x => ids.Contains(x.Id)).ToList();
                        if (input.Statues == RequestForQuotationStatues.Possible)
                        {
                            if (input.CompanyId.HasValue)
                                if (await _paidRequestPossibleManager.CheckIfCompanyPaidForRequestContact(input.CompanyId.Value, item.Id))
                                    item.IsPaid = true;
                            if (input.CompanyBranchId.HasValue)
                                if (await _paidRequestPossibleManager.CheckIfCompanyBranchPaidForRequestContact(input.CompanyBranchId.Value, item.Id))
                                    item.IsPaid = true;

                        }
                        if (item.Statues is (RequestForQuotationStatues.InProcess or RequestForQuotationStatues.FinishByCompany or RequestForQuotationStatues.FinishByUser or RequestForQuotationStatues.NotFinishByUser or RequestForQuotationStatues.Finished))
                        {
                            if (input.CompanyId.HasValue)
                                if (await _offerManager.CheckIfCompanyOfferHasApprovedWithThisRequest(input.CompanyId.Value, item.Id))
                                    item.IsThisRequestOfferWithThisCompany = true;
                            if (input.CompanyBranchId.HasValue)
                                if (await _offerManager.CheckIfCompanyBranchOfferHasApprovedWithThisRequest(input.CompanyBranchId.Value, item.Id))
                                    item.IsThisRequestOfferWithThisCompany = true;

                        }
                        if (input.CompanyId.HasValue)
                            if (offers.Any(x => x.CompanyId == input.CompanyId && x.RequestId == item.Id))
                            {
                                item.IsThisCompnayProvideOfferWithThisRequest = true;
                                item.OfferStatues = offers.Where(x => x.CompanyId == input.CompanyId && x.RequestId == item.Id).Select(x => x.OfferStatues).FirstOrDefault();
                            }
                        if (input.CompanyBranchId.HasValue)
                            if (offers.Any(x => x.CompanyBranchId == input.CompanyBranchId && x.RequestId == item.Id))
                            {
                                item.IsThisCompnayProvideOfferWithThisRequest = true;
                                item.OfferStatues = offers.Where(x => x.CompanyBranchId == input.CompanyBranchId && x.RequestId == item.Id).Select(x => x.OfferStatues).FirstOrDefault();
                            }
                        item.DiscountPercentageIfUserCancelHisRequest = discount;

                    }
                    return result;
                }
                catch (Exception ex) { throw; /*new UserFriendlyException(ex.Message + " " + ex.InnerException);*/ }
            }
        }

        //public override async Task DeleteAsync(EntityDto<int> input)
        //{ }

        protected override IQueryable<RequestForQuotation> CreateFilteredQuery(PagedRequestForQuotationResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.SourceCity).ThenInclude(x => x.Translations).Where(x => x.SourceCity.IsDeleted == false).AsNoTracking();
            data = data.Include(x => x.DestinationCity).ThenInclude(x => x.Translations).Where(x => x.SourceCity.IsDeleted == false).AsNoTracking();
            data = data.Include(x => x.User).Where(x => x.User.IsDeleted == false).AsNoTracking();
            if (input.OnlyMyRequests)
                data = data.Where(x => x.UserId == AbpSession.UserId.Value);
            if (!input.Keyword.IsNullOrEmpty())
            {
                data = data.Include(x => x.Services).ThenInclude(x => x.Service)
                    .ThenInclude(x => x.Translations).Where(x => x.Services.Any(x => x.Service.IsDeleted == false)).AsNoTracking();
                var keyword = input.Keyword.ToLower();
                data = data.Where(x => x.SourceAddress.Contains(keyword)
                || x.DestinationAddress.ToLower().Contains(keyword)
                || x.SourceCity.Translations.Any(x => x.Name.ToLower().Contains(keyword))
                || x.DestinationCity.Translations.Any(x => x.Name.ToLower().Contains(keyword))
                || x.User.PhoneNumber.ToLower().Contains(keyword)
                || x.User.UserName.ToLower().Contains(keyword)
                || x.Services.Any(x => x.Service.Translations.Any(x => x.Name.ToLower().Contains(keyword)))
                );

            }
            if (input.Statues.HasValue)
            {
                data = data.Where(x => x.Statues == input.Statues.Value);
            }
            if (input.CompanyId.HasValue && !input.GetPossibleRequest && !input.GetPaidRequestForThisCompanyOrBranch)
            {
                var requestIds = _selectedCompaniesBySystemRequestManager.GetRequestIdsByCompanyId(input.CompanyId.Value).GetAwaiter().GetResult();
                data = data.Where(x => requestIds.Contains(x.Id));
                //data = data.Where(x => x.Statues != RequestForQuotationStatues.Possible);
            }
            if (input.CompanyBranchId.HasValue && !input.GetPossibleRequest && !input.GetPaidRequestForThisCompanyOrBranch)
            {
                var requestIds = _selectedCompaniesBySystemRequestManager.GetRequestIdsByCompanyBranchId(input.CompanyBranchId.Value).GetAwaiter().GetResult();
                data = data.Where(x => requestIds.Contains(x.Id));
                //data = data.Where(x => x.Statues != RequestForQuotationStatues.Possible);
            }
            if (input.CompanyId.HasValue && input.GetPossibleRequest)
            {
                var requestIds = _offerManager.GetRequestIdsWhichConnectedWithCompanyAndBeenRejectedFromUserForCompany(input.CompanyId.Value).GetAwaiter().GetResult();
                data = data.Where(x => !requestIds.Contains(x.Id));
                if (!input.AcceptPossibleRequest)
                    data = data.Where(x => x.Statues != RequestForQuotationStatues.Possible);
            }
            if (input.CompanyBranchId.HasValue && input.GetPossibleRequest)
            {
                var requestIds = _offerManager.GetRequestIdsWhichConnectedWithCompanyAndBeenRejectedFromUserForCompanyBranch(input.CompanyBranchId.Value).GetAwaiter().GetResult();
                data = data.Where(x => !requestIds.Contains(x.Id));
                if (!input.AcceptPossibleRequest)
                    data = data.Where(x => x.Statues != RequestForQuotationStatues.Possible);
            }
            if (input.ForBroker && input.BrokerId is null)
            {
                var requestIds = _requestForQuotationManager.GetRequestIdsWhichSubmitededByRigesteredUserViaBroker(AbpSession.UserId.Value).GetAwaiter().GetResult();
                data = data.Where(x => requestIds.Contains(x.Id));
            }
            if (input.ForBroker && input.BrokerId.HasValue)
            {
                var requestIds = _requestForQuotationManager.GetRequestIdsForUsersByBrokerId(input.BrokerId.Value).GetAwaiter().GetResult();
                data = data.Where(x => requestIds.Contains(x.Id));
            }

            if (input.ServiceType.HasValue)
            {
                data = data.Where(x => x.ServiceType == input.ServiceType.Value);
            }
            if (input.SourceTypeId.HasValue)
            {
                data = data.Where(x => x.SourceTypeId == input.SourceTypeId.Value);
            }
            if (input.GetPaidRequestForThisCompanyOrBranch && input.CompanyId.HasValue)
            {
                var paidRequestIds = _paidRequestPossibleManager.GetAllPaidRequestIdsWithThisCompany(input.CompanyId.Value, true).GetAwaiter().GetResult();
                data = data.Where(x => paidRequestIds.Contains(x.Id));
            }
            if (input.GetPaidRequestForThisCompanyOrBranch && input.CompanyBranchId.HasValue)
            {
                var paidRequestIds = _paidRequestPossibleManager.GetAllPaidRequestIdsWithThisCompany(input.CompanyBranchId.Value, false).GetAwaiter().GetResult();
                data = data.Where(x => paidRequestIds.Contains(x.Id));
            }
            if (input.CreationTime.HasValue)
            {
                data = data.Where(x => x.CreationTime.Date == input.CreationTime.Value.Date).AsNoTracking();
            }
            if (input.UserId.HasValue)
                data = data.Where(x => x.UserId == input.UserId.Value).AsNoTracking();
            if (input.GetRequestsThatHasStorage)
                data = data.Where(x => x.ArrivalAtUtc.HasValue).AsNoTracking();
            return data;
        }
        protected override IQueryable<RequestForQuotation> ApplySorting(IQueryable<RequestForQuotation> query, PagedRequestForQuotationResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }
        [AbpAuthorize(PermissionNames.Request_ChangeStatus)]
        public async Task<bool> ChangeStatuesForRequest(SwitchStatuesForRequestDto input)
        {
            RequestForQuotation request = await Repository.GetAsync(input.RequestId);
            if (request is null)
                throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.RequestForQuotation);
            if (input.Statues == RequestForQuotationStatues.Rejected || input.Statues == RequestForQuotationStatues.RejectedNeedToEdit)
            {
                if (input.ReasonRefuse.IsNullOrWhiteSpace())
                    throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.RejectReason + " ReasonRefuse Is Required");

                request.ReasonRefuse = input.ReasonRefuse;
            }
            request.Statues = input.Statues;
            await Repository.UpdateAsync(request);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }

        public async Task<List<long>> InsertAndNoticFilteredCompanies(CompanyAndCompanyBranchIdsDto input, long requestId, bool forPossibleRequest = false)
        {
            try
            {
                CompanyAndBranchDto allCompaniesWithallBranches = await _companyManager.GetSelectedCompanyAndBranch(input);
                var selectedCompanies = new List<SelectedCompaniesBySystemForRequest>();
                var userIds = new List<long>();
                if (input.CompanyIds is not null && input.CompanyIds.Count > 0)
                {
                    foreach (var id in input.CompanyIds)
                    {
                        var company = allCompaniesWithallBranches.Companies.Where(c => c.Id == id).FirstOrDefault();
                        if (company is not null)
                        {
                            userIds.Add(company.UserId.Value);
                            if (forPossibleRequest)
                                continue;
                            selectedCompanies.Add(new SelectedCompaniesBySystemForRequest
                            {
                                CompanyId = company.Id,
                                RequestForQuotationId = requestId
                            }
                            );
                        }
                    }
                }
                if (input.CompanyBranchIds is not null && input.CompanyBranchIds.Count > 0)
                {
                    foreach (var id in input.CompanyBranchIds)
                    {
                        var companyBranch = allCompaniesWithallBranches.CompanyBranches.Where(c => c.Id == id).FirstOrDefault();
                        if (companyBranch is not null)
                        {
                            userIds.Add(companyBranch.UserId.Value);
                            if (forPossibleRequest)
                                continue;
                            selectedCompanies.Add(new SelectedCompaniesBySystemForRequest
                            {
                                CompanyBranchId = companyBranch.Id,
                                RequestForQuotationId = requestId
                            });
                        }
                    }
                }
                if (forPossibleRequest)
                    return userIds;
                await _selectedCompaniesBySystemForRequestRepository.InsertRangeAsync(selectedCompanies);
                await ChangeStatuesForRequest(new SwitchStatuesForRequestDto { RequestId = requestId, Statues = RequestForQuotationStatues.Approved });
                await _notificationSender.SendNotificationForSelectedCompanies(userIds, requestId);
                return new List<long>();
            }
            catch (Exception ex)
            {
                throw /*new UserFriendlyException(ex.Message, ex.InnerException)*/;
            }
        }

        public async Task<bool> GetRequestContactAsync(CreateAskForContactDto input)
        {
            var request = await _requestForQuotationManager.GetRequestWithAttributeValues(input.RequestId);
            var pointsToGetRequest = await _sourceTypeManager.GetPointsToGetRequestBySourceTypeIdAsync(request.SourceTypeId);
            if (pointsToGetRequest == 222222222)
                pointsToGetRequest = await _attributeChoiceManger.GetPointsToBuyRequestByAttributeChoices(request.AttributeForSourceTypeValues.Where(x => x.AttributeChoiceId.HasValue).Select(x => x.AttributeChoiceId.Value).ToList());
            if (input.Provider == Provider.Company)
            {
                await _companyManager.GetPointFromCompanyForGettingContactRequest(input.Id, pointsToGetRequest);
                await _paidRequestPossibleManager.InsertNewEntity(
                           new PaidRequestPossible
                           {
                               CompanyId = input.Id,
                               NumberOfPaidPoints = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.NumberOfPointsToGetFromCompanyToGetRequestContact),
                               RequestId = input.RequestId,

                           });
            }
            else
            {
                await _companyBranchManager.GetPointFromCompanyBranchForGettingContactRequest(input.Id, pointsToGetRequest);
                await _paidRequestPossibleManager.InsertNewEntity(
                            new PaidRequestPossible
                            {
                                CompanyBranchId = input.Id,
                                NumberOfPaidPoints = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.NumberOfPointsToGetFromCompanyToGetRequestContact),
                                RequestId = input.RequestId,

                            });
            }
            return true;
        }
        [AbpAuthorize]
        public async Task<bool> ConfirmFinishRequestForQuotationByCompany(FinishRequestByCompanyDto input)
        {
            await _requestForQuotationManager.CheckIfEntityExict(input.RequestId);
            switch (input.Provider)
            {
                case Provider.Company:
                    if (!await _offerManager.CheckIfCompanyOfferHasApprovedWithThisRequest(input.CompanyOrCompanyBranchId, input.RequestId))
                        throw new UserFriendlyException(400, Exceptions.IncompatibleValue, Tokens.RequestForQuotationWithCompany);

                    break;
                case Provider.CompanyBranch:
                    if (!await _offerManager.CheckIfCompanyBranchOfferHasApprovedWithThisRequest(input.CompanyOrCompanyBranchId, input.RequestId))
                        throw new UserFriendlyException(400, Exceptions.IncompatibleValue, Tokens.RequestForQuotationWithCompany);
                    break;
            }
            bool updateOffer = false;
            RequestForQuotation request = await Repository.GetAsync(input.RequestId);
            request.ConfirmFinishDateByCompany = DateTime.UtcNow;
            if (request.Statues == RequestForQuotationStatues.FinishByUser)
            {
                request.Statues = RequestForQuotationStatues.Finished;
                request.FinishedDate = DateTime.UtcNow;
                updateOffer = true;
            }
            else
            {
                request.Statues = RequestForQuotationStatues.FinishByCompany;
            }
            await Repository.UpdateAsync(request);
            if (updateOffer)
            {
                await _offerManager.MakeOfferFinishByRequestId(input.RequestId);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            foreach (var id in input.AttachmentIdsForFinishedRequest)
            {
                await _attachmentManager.CheckAndUpdateRefIdAsync(id, AttachmentRefType.FinishedRequestByCompany, request.Id);
            }
            return true;
        }
        [AbpAuthorize]
        public async Task<bool> ConfirmFinishOrNotFinishRequestForQuotationByUser(FinishRequestByUserDto input)
        {
            await _requestForQuotationManager.CheckIfEntityExict(input.RequestId);
            bool updateOffer = false;
            RequestForQuotation request = await _requestForQuotationManager.GetRequestWithAttributeValues(input.RequestId);
            if (request.UserId != AbpSession.UserId.Value)
                throw new UserFriendlyException(403, Exceptions.IncompatibleValue, Tokens.User);
            request.ConfirmFinishDateByUser = DateTime.UtcNow;
            if (request.Statues == RequestForQuotationStatues.FinishByCompany && input.Statues == RequestForQuotationStatues.FinishByUser)
            {
                request.Statues = RequestForQuotationStatues.Finished;
                request.FinishedDate = DateTime.UtcNow;
                updateOffer = true;
            }
            else
            {
                request.Statues = input.Statues;
                request.ReasonOfNotFinish = input.ReasonOfNotFinish;

            }
            await Repository.UpdateAsync(request);
            if (updateOffer)
            {
                await _offerManager.MakeOfferFinishByRequestId(input.RequestId);
                TinySelectedCompanyDto selectedCompany = await _offerManager.GetSelectedCompanyByOfferId(input.OfferId.Value);
                await GiftCompanyThatProvideOfferBySourceTypePoints(selectedCompany, request.SourceTypeId, request.AttributeForSourceTypeValues.Where(x => x.AttributeChoiceId.HasValue).Select(x => x.AttributeChoiceId.Value).ToList());
                var code = (await _userManager.GetUserByIdAsync(request.UserId)).MediatorCode;
                if (!string.IsNullOrWhiteSpace(code))
                    await GiveMediatorCommissionForFinishRequest(code, selectedCompany, input.OfferId.Value);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task GiveMediatorCommissionForFinishRequest(string code, TinySelectedCompanyDto selectedCompany, Guid offerId)
        {
            double commission = 0;
            if (selectedCompany.Provider == Provider.Company)
                commission = (await _commissionGroupManager.GetCommissionByCompanyIdAsync(selectedCompany.Id)).Commission;
            else
            {
                var branch = await _companyBranchManager.GetSuperLiteEntityByIdAsync(selectedCompany.Id);
                if (branch.CompanyId.HasValue)
                    commission = (await _commissionGroupManager.GetCommissionByCompanyIdAsync(selectedCompany.Id)).Commission;
                else
                    commission = await SettingManager.GetSettingValueAsync<double>(AppSettingNames.CommissionForBranchesWithoutCompany);

            }
            var paidMoneyForThisRequest = await _moneyTransferManager.ReturnAmountByOfferId(offerId);
            await _mediatorManager.UpdateCommissionForMediator(paidMoneyForThisRequest, commission, code);

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task GiftCompanyThatProvideOfferBySourceTypePoints(TinySelectedCompanyDto selectedCompany, int sourceTypeId, List<int> choiceIds)
        {
            if (selectedCompany.Provider == Provider.Company)
                await _companyManager.GiftPointToCompany(selectedCompany.Id, sourceTypeId, choiceIds);
            else
                await _companyBranchManager.GiftPointToCompanyBranch(selectedCompany.Id, sourceTypeId, choiceIds);

        }
        [HttpPut]
        [AbpAuthorize]
        public async Task<OutPutBooleanStatuesDto> CancelRequestByUserAsync(long requestId)
        {
            var request = await Repository.GetAsync(requestId);
            if (request.Statues is RequestForQuotationStatues.InProcess)
            {
                var discountpercentage = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.DiscountPercentageIfUserCancelHisRequest);
                var offerId = (await _offerManager.GetOfferIdByRequestId(requestId)).SelectedOfferId.Value;
                var paidMoneyForThisOffer = await _moneyTransferManager.ReturnAmountByOfferId(offerId);
                if (DateTime.UtcNow.AddHours(48) >= request.MoveAtUtc)
                {
                    var amount = paidMoneyForThisOffer - ((paidMoneyForThisOffer * (double)discountpercentage) / 100);
                    var moneyTransfer = new MoneyTransfer()
                    {
                        Amount = amount,
                        UserId = AbpSession.UserId.Value,
                        PaidProvider = PaidProvider.User,
                        PaidStatues = PaidStatues.Finish,
                        PaidDestination = PaidDestination.ForHim,
                        ReasonOfPaid = ReasonOfPaid.ReturnMoneyAfterDiscount,
                    };
                    await _moneyTransferManager.InsertNewMoneyTransfer(moneyTransfer);
                }
                else
                {
                    var moneyTransfer = new MoneyTransfer()
                    {
                        Amount = paidMoneyForThisOffer,
                        UserId = AbpSession.UserId.Value,
                        PaidProvider = PaidProvider.User,
                        PaidStatues = PaidStatues.Finish,
                        PaidDestination = PaidDestination.ForHim,
                        ReasonOfPaid = ReasonOfPaid.ReturnMoneyWithoutDiscount,
                    };
                    await _moneyTransferManager.InsertNewMoneyTransfer(moneyTransfer);
                }
                request.Statues = RequestForQuotationStatues.CanceledAfterInProcess;
            }
            else
                request.Statues = RequestForQuotationStatues.Canceled;
            await Repository.UpdateAsync(request);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto() { BooleanStatues = true };
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.Request_Update)]
        public async Task<OutPutBooleanStatuesDto> PayForStorage(ChangeDateForRequestDto input)
        {
            /*
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(
                    new PaymentIntentCreateOptions
                    {
                        Amount = (long)input.Amount * 100,
                        Currency = "AED",
                        PaymentMethod = input.PaymentMethodId,
                        PaymentMethodTypes = new List<string> { "card", }
                    });
            await paymentIntentService.ConfirmAsync(paymentIntent.Id);
            */
            var moneyTransfer = new MoneyTransfer()
            {
                Amount = input.Amount,
                PaidStatues = PaidStatues.Finish,
                ReasonOfPaid = ReasonOfPaid.PayForExtendStorage,
                PaidProvider = PaidProvider.User,
                UserId = AbpSession.UserId.Value,
                PaidDestination = PaidDestination.OnHim,
            };
            await ChangeFinalDateForRequest(input);
            await _moneyTransferManager.InsertNewMoneyTransfer(moneyTransfer);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto()
            {
                BooleanStatues = true
            };
        }

        private async Task ChangeFinalDateForRequest(ChangeDateForRequestDto input)
        {
            var request = await Repository.GetAsync(input.RequestId);
            request.ArrivalAtUtc = input.MoveArriveAt;
            await Repository.UpdateAsync(request);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        [HttpPost]
        [AbpAuthorize]
        public async Task<bool> InserNewPlaceForUser(SearchedPlacesByUserDto input)
        {
            await _requestForQuotationManager.InserNewSearchedPlaceForUser(input, AbpSession.UserId.Value);
            return true;
        }
        [HttpGet]
        [AbpAuthorize]
        public async Task<List<SearchedPlacesByUserDto>> GetAllSearchedPlacesByUserAsync()
        {
            return await _requestForQuotationManager.GetAllSearchedPlacesByUser(AbpSession.UserId.Value);
        }
        [AbpAuthorize]
        public async Task<bool> DeleteSearchedPlaceByPlaceId(string placeId)
        {
            await _requestForQuotationManager.CheckIfPlaceIdExistForThisUserAndDeleteIt(AbpSession.UserId.Value, placeId);
            return true;
        }
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<RequestsStatisticalNumbersDto> GetStatisticalNumbers(RequestStatisticalInputDto input)
        {
            return await _requestForQuotationManager.GetCountNumberAboutRequestsForQuotation(input);
        }
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<List<CitiesStatisticsForRequestsDto>> GetCitiesStatistics(InputCitiesStatisticsForRequestsDto input)
        {
            return await _requestForQuotationManager.GetCitiesStatisticsForRequestsDto(input);
        }
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<List<ServiceStatisticsForRequestsDto>> GetServiceStatistics()
        {
            return await _serviceValueManager.GetServicesStatisticsForRequestsDto();
        }
        [Route("api/services/app/Statictics/GetStatisticsNumbers")]
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<GeneralStatisticsNumbersDto> GetStatisticsNumbers()
        {
            var result = new GeneralStatisticsNumbersDto()
            {
                CompaniesCount = await _companyManager.GetCompaniesCount(),
                CompanyBranchCount = await _companyBranchManager.GetCompanyBranchesCount(),
                BrokersCount = await _mediatorManager.GetBrokersCount(),
                PartnersCount = await _partnerManager.GetPartnersCount(),
                BasicUsersCount = await _userManager.Users.Where(x => x.Type == UserType.BasicUser && x.IsDeleted == false).CountAsync(),
                AskForHelpCount = await _askForHelpRepository.GetAll().Where(x => x.IsDeleted == false).CountAsync(),
                RequestFQCount = await Repository.GetAll().Where(x => x.IsDeleted == false).CountAsync()
            };
            if (result is null)
                return new GeneralStatisticsNumbersDto();
            return result;
        }
    }
}
