using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.Offers;
using Mofleet.Domain.PaidRequestPossibles;
using Mofleet.Domain.Regions;
using Mofleet.Domain.RequestForQuotations.Dto;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.TimeWorks.Dtos;
using Mofleet.Domain.UserVerficationCodes;
using Mofleet.Localization.SourceFiles;
using Mofleet.NotificationSender;
using Mofleet.SmsSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Cities
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class CompanyAppService : MofleetAsyncCrudAppService<Company, CompanyDetailsDto, int, LiteCompanyDto,
        PagedCompanyResultRequestDto, CreateCompanyDto, UpdateCompanyDto>,
        ICompanyAppService
    {
        private readonly CityManager _cityManager;
        private readonly ServiceValueManager _serviceValueManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly AttachmentManager _attachmentManager;
        private readonly RegionManager _regionManager;
        private readonly ICompanyManager _companyManager;
        private readonly ICompanyBranchManager _companyBranchManager;
        private readonly UserManager _userManager;
        private readonly IPaidRequestPossibleManager _paidRequestPossibleManager;
        private readonly ISelectedCompaniesBySystemForRequestManager _selectedCompaniesBySystemForRequestManager;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly INotificationSender _notificationSender;
        private readonly IOfferManager _offerManager;
        private readonly ICommissionGroupManager _commissionGroupManager;
        private readonly ISmsSenderAppService _smsSenderAppService;

        public CompanyAppService(IRepository<Company, int> repository, CityManager cityManager,
            ServiceValueManager serviceValueManager,
            IMapper mapper,
            UserRegistrationManager userRegistrationManager,
            AttachmentManager attachmentManager,
            RegionManager regionManager,
            ICompanyManager companyManager,
            UserManager userManager,
            IPaidRequestPossibleManager paidRequestPossibleManager,
            ISelectedCompaniesBySystemForRequestManager selectedCompaniesBySystemForRequestManager,
            ICompanyBranchManager companyBranchManager,
            IServiceManager serviceManager, INotificationSender notificationSender,
            IOfferManager offerManager, ICommissionGroupManager commissionGroupManager, ISmsSenderAppService smsSenderAppService) : base(repository)
        {
            _cityManager = cityManager;
            _serviceValueManager = serviceValueManager;
            _userRegistrationManager = userRegistrationManager;
            _attachmentManager = attachmentManager;
            _regionManager = regionManager;
            _companyManager = companyManager;
            _userManager = userManager;
            _paidRequestPossibleManager = paidRequestPossibleManager;
            _selectedCompaniesBySystemForRequestManager = selectedCompaniesBySystemForRequestManager;
            _mapper = mapper;
            _companyBranchManager = companyBranchManager;
            _serviceManager = serviceManager;
            _notificationSender = notificationSender;
            _offerManager = offerManager;
            _commissionGroupManager = commissionGroupManager;
            _smsSenderAppService = smsSenderAppService;
        }

        [AbpAuthorize(PermissionNames.Company_Get)]
        public override async Task<CompanyDetailsDto> GetAsync(EntityDto<int> input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var company = await _companyManager.GetLiteEntityByIdAsync(input.Id);
                if (company is null)
                    throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.Company);
                var companyDto = ObjectMapper.Map<CompanyDetailsDto>(company);
                companyDto.Region = await _regionManager.GetEntityDtoByIdAsync(company.RegionId.Value);
                companyDto.CompanyContact = await _companyManager.GetCompanyContactDtoByCompanyId(input.Id);
                companyDto.CompanyBranches = await _companyBranchManager.GetCompanyBranchesDtoByCompanyId(input.Id);
                companyDto.Services = await _serviceValueManager.GetFullServicesByCompanyOrRequestIdAsync(input.Id, true, false);
                if (!company.ParentCompanyId.HasValue)
                    companyDto.User = ObjectMapper.Map<LiteUserDto>(await _userManager.GetUserByIdAsync(company.UserId.Value));
                else
                    companyDto.User = ObjectMapper.Map<LiteUserDto>(await _userManager.GetUserByIdAsync(await _companyManager.GetUserIdByCompanyIdAsync(company.ParentCompanyId.Value)));
                companyDto.GeneralRating = await _companyManager.GetGeneralRatingDtoForComapny(company.Id);
                companyDto.TimeOfWorks = await _companyManager.GetTimeWorksDtoForCompany(input.Id);
                var fullAttachmentsForCompany = await _attachmentManager.GetByRefIdAsync(input.Id);
                var attachmentForProfile = fullAttachmentsForCompany.Where(x => x.RefType == AttachmentRefType.CompanyProfile).FirstOrDefault();
                if (attachmentForProfile is not null)
                    companyDto.CompanyProfile = new LiteAttachmentDto
                    {
                        Id = attachmentForProfile.Id,
                        Url = _attachmentManager.GetUrl(attachmentForProfile),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachmentForProfile),
                    };
                var attachmentsForOwnerIdentity = fullAttachmentsForCompany.Where(x => x.RefType == AttachmentRefType.CompanyOwnerIdentity).ToList();
                foreach (var attachment in attachmentsForOwnerIdentity)
                {
                    if (attachment is not null)
                    {
                        companyDto.CompanyOwnerIdentity.Add(new LiteAttachmentDto
                        {
                            Id = attachment.Id,
                            Url = _attachmentManager.GetUrl(attachment),
                            LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment),
                        });
                    }
                }
                var attachmentsForCompanyCommercialRegister = fullAttachmentsForCompany.Where(x => x.RefType == AttachmentRefType.CompanyCommercialRegister).ToList();
                foreach (var attachment in attachmentsForCompanyCommercialRegister)
                {
                    if (attachment is not null)
                    {
                        companyDto.CompanyCommercialRegister.Add(new LiteAttachmentDto
                        {
                            Id = attachment.Id,
                            Url = _attachmentManager.GetUrl(attachment),
                            LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment),
                        });
                    }
                }
                var attachmentsForAdditionalAttachment = fullAttachmentsForCompany.Where(x => x.RefType == AttachmentRefType.AdditionalAttachment).ToList();
                foreach (var attachment in attachmentsForAdditionalAttachment)
                {
                    if (attachment is not null)
                    {
                        companyDto.AdditionalAttachment.Add(new LiteAttachmentDto
                        {
                            Id = attachment.Id,
                            Url = _attachmentManager.GetUrl(attachment),
                            LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment),
                        });
                    }
                }
                companyDto.TotalPoints = companyDto.NumberOfGiftedPoints + companyDto.NumberOfPaidPoints;
                companyDto.ChildCompanyStatuesDto = await _companyManager.GetChildCompanyStatuesIfFound(companyDto.Id);
                return companyDto;
            }
        }
        [AbpAuthorize]
        public async Task<IList<ReviewDetailsDto>> GetReviewDetailsById(EntityDto<int> input)
        {
            return await _companyManager.GetReviewsForCompany(input.Id);
        }

        [AbpAuthorize(PermissionNames.Company_List)]
        public override async Task<PagedResultDto<LiteCompanyDto>> GetAllAsync(PagedCompanyResultRequestDto input)
        {
            PagedResultDto<LiteCompanyDto> result = await base.GetAllAsync(input);
            var attchments = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.CompanyProfile);
            var serviceIdsAndCompanyIdsDto = await _serviceValueManager.GetServicesIdsAndCompanyIdsAsync(result.Items.Select(x => x.Id).ToList());
            var services = await _serviceManager.GetAllServicesDtosAsync();
            var companyIdsThatSentOffer = new List<int>();
            if (input.GetCompaniesWithRequest)
                companyIdsThatSentOffer = await _offerManager.GetCompanyIdsThatSentOffer(input.RequestId);
            var commissionForCompanies = await _commissionGroupManager.GetCommissionGroupByCompanyIds(result.Items.Select(x => x.Id).ToList());
            foreach (var item in result.Items)
            {
                var attchment = attchments.Where(x => x.RefId == item.Id).FirstOrDefault();
                if (attchment is not null)
                {
                    item.CompanyProfile = new LiteAttachmentDto
                    {
                        Id = attchment.Id,
                        Url = _attachmentManager.GetUrl(attchment),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attchment)

                    };
                }
                var servicesIds = serviceIdsAndCompanyIdsDto.Where(x => x.CompanyId == item.Id).SelectMany(x => x.ServiceIds).ToList();
                item.Services = services.Where(x => servicesIds.Contains(x.Id)).ToList();
                item.TotalPoints = item.NumberOfGiftedPoints + item.NumberOfPaidPoints;
                if (input.GetCompaniesWithRequest && companyIdsThatSentOffer.Any(x => x == item.Id))
                    item.IsThisCompanyProvideOffer = true;
                item.CommissionGroup = commissionForCompanies.Where(x => x.Companies.Any(x => x == item.Id)).Select(x => x.Name).FirstOrDefault();
            }
            //Sort Items If Need To Filter
            if (input.IsForFilter)
            {
                var countServicesOfRequest = await _serviceValueManager.GetCountOfServicesForRequest(input.RequestId);
                var companiesWithCountOfServices = await _serviceValueManager.GetCountOfServicesForCompanies(input.CompanyIds);
                List<LiteCompanyDto> newItems = new List<LiteCompanyDto>();
                foreach (var item in input.CompanyIds)
                {
                    newItems.Add(result.Items.Where(x => x.Id == item).FirstOrDefault());
                }
                newItems.RemoveAll(x => x is null);
                foreach (var item in newItems)
                {
                    int count = companiesWithCountOfServices.Where(x => x.CompanyId == item.Id).Select(x => x.Count).FirstOrDefault();
                    if (count != 0)
                    {
                        if (count > countServicesOfRequest)
                            item.CompatibilityRate = 100;
                        else
                            item.CompatibilityRate = (double)count / countServicesOfRequest * 100;

                    }
                }
                var newResult = new PagedResultDto<LiteCompanyDto>
                {
                    Items = newItems.OrderByDescending(x => x.CompatibilityRate).ToList(),
                    TotalCount = result.TotalCount
                };

                return newResult;
            }
            return result;

        }

        [AbpAuthorize(PermissionNames.Company_Create)]
        public override async Task<CompanyDetailsDto> CreateAsync(CreateCompanyDto input)
        {
            try
            {
                CheckCreatePermission();
                bool isForUpdate = false;
                if (input.OldCompanyId.HasValue)
                    isForUpdate = true;
                if (!await _regionManager.CheckIfRegionIsExist(input.RegionId))
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Region));
                await _serviceManager.CheckServicesIsCorrect(input.services);
                var cities = new List<City>();
                if (input.AvailableCitiesIds.Count > 0 && input.AvailableCitiesIds is not null)
                    cities = await _cityManager.CheckAndGetCitiesById(input.AvailableCitiesIds);
                Company company = ObjectMapper.Map<Company>(input);
                company.Code = await _companyManager.CreateCodeForCompany();
                company.CreationTime = DateTime.UtcNow;
                if (AbpSession.UserId.HasValue && !isForUpdate)
                {
                    var userCheck = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                    if (userCheck.Type == UserType.Admin)
                    {
                        User user = await _userRegistrationManager.RegisterAsyncForUserCompanyByAdmin(input.userDto.EmailAddress, input.userDto.PhoneNumber, input.userDto.DialCode, input.userDto.Password, UserType.CompanyUser);
                        company.User = user;
                        company.statues = CompanyStatues.Approved;
                    }
                    else if (userCheck.Type == UserType.CompanyUser)
                    {
                        if (await _companyManager.CheckIfUserHasCompany(userCheck.Id))
                            throw new UserFriendlyException(Exceptions.ObjectIsAlreadyExist, Tokens.User + " User Already Has Company");
                        company.User = userCheck;
                        company.statues = CompanyStatues.Checking;
                    }

                    else
                        throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);
                }
                else if (!AbpSession.UserId.HasValue)
                    throw new UserFriendlyException(Exceptions.YouCannotDoThisAction);
                company.AvailableCities = cities;
                company.services = null;
                company.IsActive = true;
                if (isForUpdate)
                {
                    company.statues = CompanyStatues.Checking;
                    company.ParentCompanyId = input.OldCompanyId.Value;
                }
                var companyId = await Repository.InsertAndGetIdAsync(company);
                if (isForUpdate)
                {
                    var userIds = await _userManager.Users.Where(x => x.Type == UserType.Admin).Select(x => x.Id).ToListAsync();
                    await _notificationSender.SendNotificationForAdminToNoticHimAboutRequestToUpdateCompany(userIds, company.ParentCompanyId.Value);
                }
                await _serviceValueManager.InsertServiceValuesForCompany(input.services, companyId);
                await CurrentUnitOfWork.SaveChangesAsync();
                if (!isForUpdate)
                    await _attachmentManager.CheckAndUpdateRefIdAsync(
                                     input.CompanyProfilePhotoId, AttachmentRefType.CompanyProfile, companyId);
                else
                {
                    var profileAttachment = await _attachmentManager.GetElementByRefAsync(company.ParentCompanyId.Value, AttachmentRefType.CompanyProfile);
                    if (input.CompanyProfilePhotoId != profileAttachment.Id || profileAttachment is null)
                        await _attachmentManager.CheckAndUpdateRefIdAsync(
                               input.CompanyProfilePhotoId, AttachmentRefType.CompanyProfile, companyId);
                    else
                        await _attachmentManager.CopyNewAttachmentForCompany(input.CompanyProfilePhotoId, companyId, AttachmentRefType.CompanyProfile);
                }
                var attachments = new List<Attachment>();
                if (isForUpdate)
                    attachments = await _attachmentManager.GetByRefAsync(company.ParentCompanyId.Value, AttachmentRefType.CompanyOwnerIdentity);
                foreach (var attachmentId in input.CompanyOwnerIdentityIds)
                {
                    if (isForUpdate && attachments.Exists(x => x.Id == attachmentId))
                        await _attachmentManager.CopyNewAttachmentForCompany(attachmentId, companyId, AttachmentRefType.CompanyOwnerIdentity);
                    else
                        await _attachmentManager.CheckAndUpdateRefIdAsync(
                                        attachmentId, AttachmentRefType.CompanyOwnerIdentity, companyId);
                }
                if (isForUpdate)
                    attachments = await _attachmentManager.GetByRefAsync(company.ParentCompanyId.Value, AttachmentRefType.CompanyCommercialRegister);
                foreach (var attachmentId in input.CompanyCommercialRegisterIds)
                {
                    if (isForUpdate && attachments.Exists(x => x.Id == attachmentId))
                        await _attachmentManager.CopyNewAttachmentForCompany(attachmentId, companyId, AttachmentRefType.CompanyCommercialRegister);
                    else
                        await _attachmentManager.CheckAndUpdateRefIdAsync(
                                    attachmentId, AttachmentRefType.CompanyCommercialRegister, companyId);
                }
                if (isForUpdate)
                    attachments = await _attachmentManager.GetByRefAsync(company.ParentCompanyId.Value, AttachmentRefType.AdditionalAttachment);
                foreach (var attachmentId in input.AdditionalAttachmentIds)
                {
                    if (isForUpdate && attachments.Exists(x => x.Id == attachmentId))
                        await _attachmentManager.CopyNewAttachmentForCompany(attachmentId, companyId, AttachmentRefType.AdditionalAttachment);
                    else
                        await _attachmentManager.CheckAndUpdateRefIdAsync(
                                        attachmentId, AttachmentRefType.AdditionalAttachment, companyId);
                }
                if (!isForUpdate)
                    await _commissionGroupManager.AddNewCompanyToDefaultGroup(companyId);
                await AddOrUpdateTimeWorkForCompany(new CreateTiemOfWorkDto
                {
                    Timeworks = input.Timeworks,
                    CompanyId = companyId
                });
                return MapToEntityDto(company);
            }
            catch (Exception ex) { throw; }

        }

        [AbpAuthorize(PermissionNames.Company_Update)]
        public override async Task<CompanyDetailsDto> UpdateAsync([FromBody] UpdateCompanyDto input)
        {
            if (input.IsForRequestUpdate)
                return await CreateAsync(ObjectMapper.Map(input, new CreateCompanyDto()));
            var company = await _companyManager.GetEntityByIdAsync(input.Id);

            if (company is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Company));
            if (!await _regionManager.CheckIfRegionIsExist(input.RegionId))
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Region));
            await _serviceManager.CheckServicesIsCorrect(input.services);

            try
            {
                var oldSeviceValue = company.services.ToList();
                var oldTrnsaltions = company.Translations.ToList();
                var newServices = input.services;
                input.services = null;
                _mapper.Map(input, company);
                company.CompanyContact = _mapper.Map<CompanyContact>(input.CompanyContact);
                await _companyManager.HardDeleteCompanyTranslation(oldTrnsaltions);
                await _serviceValueManager.DeleteServiceValuesForCompanyAsync(input.Id);
                await _serviceValueManager.InsertServiceValuesForCompany(newServices, company.Id);
                var cities = new List<City>();
                if (input.AvailableCitiesIds.Count > 0 && input.AvailableCitiesIds is not null)
                    company.AvailableCities = await _cityManager.CheckAndGetCitiesById(input.AvailableCitiesIds);
                await AddOrUpdateTimeWorkForCompany(new CreateTiemOfWorkDto
                {
                    Timeworks = input.Timeworks,
                    CompanyId = input.Id
                });
                int commissionGroupId = await _commissionGroupManager.GetCurrentCommissionGroupIdByCompanyId(input.Id);
                if (!input.DeleteAllOldAttachments)
                {
                    var attachment = await _attachmentManager.GetElementByRefAsync(input.Id, AttachmentRefType.CompanyProfile);
                    if (attachment is not null && attachment.Id != input.CompanyProfilePhotoId)
                    {
                        await _attachmentManager.DeleteRefIdAsync(attachment);
                        await _attachmentManager.CheckAndUpdateRefIdAsync(input.CompanyProfilePhotoId, AttachmentRefType.CompanyProfile, input.Id);
                    }
                    if (input.CompanyOwnerIdentityIds.Count > 0)
                        await _companyManager.UpdateAttachmentTypeListAsync(input.CompanyOwnerIdentityIds, AttachmentRefType.CompanyOwnerIdentity, company.Id);
                    if (input.CompanyCommercialRegisterIds.Count > 0)
                        await _companyManager.UpdateAttachmentTypeListAsync(input.CompanyCommercialRegisterIds, AttachmentRefType.CompanyCommercialRegister, company.Id);
                    await _companyManager.UpdateAttachmentTypeListAsync(input.AdditionalAttachmentIds, AttachmentRefType.AdditionalAttachment, company.Id);
                    if (input.AvailableCitiesIds.Count > 0)
                        await _companyManager.UpdateCitiesByCompanyIdAsync(input.AvailableCitiesIds, company);
                    if (AbpSession.TenantId.Value == 1)
                        company.statues = CompanyStatues.Approved;
                    else
                        company.statues = CompanyStatues.Checking;

                }
                else
                {
                    await _attachmentManager.DeleteAllRefIdForCompanyAsync(input.Id);
                    input.AdditionalAttachmentIds.AddRange(input.CompanyCommercialRegisterIds);
                    input.AdditionalAttachmentIds.AddRange(input.CompanyOwnerIdentityIds);
                    input.AdditionalAttachmentIds.Add(input.CompanyProfilePhotoId);
                    await _attachmentManager.UpdteAllRefIdAsync(input.Id, input.AdditionalAttachmentIds);
                    await _notificationSender.SendNotificationForCompanyForApproveUpdate(new List<long> { company.UserId.Value });

                }
                await Repository.UpdateAsync(company);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                if (company.ParentCompanyId is null)
                    await _commissionGroupManager.ReAddCompanyInToOwnsGroup(company, commissionGroupId);
                return MapToEntityDto(company);
            }
            catch (Exception ex) { throw; }

        }
        [AbpAuthorize(PermissionNames.Company_ChangeStatus)]
        public async Task<CompareCompaniesDto> GetCompaniesToCompare(int newCompanyId)
        {
            var compare = new CompareCompaniesDto();
            Company newCompany = await Repository.GetAsync(newCompanyId);
            if (!newCompany.ParentCompanyId.HasValue)
                throw new UserFriendlyException(Exceptions.IncompatibleValue, "There Is No Company To Compare With");
            var oldCompany = await Repository.GetAsync(newCompany.ParentCompanyId.Value);
            compare.NewCompany = await GetAsync(MapToEntityDto(newCompany));
            compare.OldCompany = await GetAsync(MapToEntityDto(oldCompany));
            return compare;
        }
        [AbpAuthorize(PermissionNames.Company_ChangeStatus)]
        public async Task<CompanyDetailsDto> ApproveUpdateCompany(CompanyDetailsDto input)
        {
            try
            {
                input.Services = null;
                var updateCompanyDto = ObjectMapper.Map(input, new UpdateCompanyDto());
                updateCompanyDto.services = await _serviceValueManager.GetAllServiceValuesForCompany(input.Id);
                updateCompanyDto.DeleteAllOldAttachments = true;
                updateCompanyDto.IsForRequestUpdate = false;
                updateCompanyDto.Id = input.ParentCompanyId.Value;
                updateCompanyDto.CompanyProfilePhotoId = input.CompanyProfile.Id;
                updateCompanyDto.CompanyOwnerIdentityIds = input.CompanyOwnerIdentity.Select(x => x.Id).ToList();
                updateCompanyDto.CompanyCommercialRegisterIds = input.CompanyCommercialRegister.Select(x => x.Id).ToList();
                updateCompanyDto.AdditionalAttachmentIds = input.AdditionalAttachment.Select(x => x.Id).ToList();
                updateCompanyDto.AvailableCitiesIds = input.AvailableCities.Select(x => x.Id).ToList();
                updateCompanyDto.Timeworks = input.TimeOfWorks;
                await UpdateAsync(updateCompanyDto);
                await _companyManager.DeleteUpdatedCompanyInstance(input.Id);
                return new CompanyDetailsDto();
            }
            catch (Exception ex) { throw; }
        }
        protected override IQueryable<Company> CreateFilteredQuery(PagedCompanyResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.CompanyBranches).Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.Region)
                       .ThenInclude(r => r.Translations)
                       .Include(x => x.Region)
                       .ThenInclude(r => r.City)
                       .ThenInclude(c => c.Translations).AsNoTracking();
            data = data.Include(x => x.CompanyContact).AsNoTracking();
            data = data.Include(x => x.User).AsNoTracking();
            if (!input.Keyword.IsNullOrEmpty())
            {
                var keyword = input.Keyword.ToLower();
                data = data.Where(x => x.Translations.Any(x => x.Name.ToLower().Contains(keyword)
                || x.Bio.ToLower().Contains(keyword)
                || x.Address.ToLower().Contains(keyword))
                || x.Region.Translations.Any(t => t.Name.ToLower().Contains(keyword))
                || x.Region.City.Translations.Any(t => t.Name.ToLower().Contains(keyword)));
            }
            if (input.IsForFilter)
            {
                var companyIds = _serviceValueManager.FilterCompatibleCompaniesWithRequestByServices(input.RequestId, true).GetAwaiter().GetResult();
                data = data.Where(x => companyIds.CompanyIds.Contains(x.Id));
                input.CompanyIds = companyIds.CompanyIds;
                //return data;
            }

            if (input.GetCompaniesWithRequest)
            {
                var compnayIds = _selectedCompaniesBySystemForRequestManager.GetCompanyIdsWithThisRequestAsync(input.RequestId).GetAwaiter().GetResult();
                data = data.Where(x => compnayIds.Contains(x.Id));
            }
            if (input.statues.HasValue)
            {
                data = data.Where(x => x.statues == input.statues.Value);
            }
            if (input.ServiceType.HasValue)
            {
                data = data.Where(x => x.ServiceType == input.ServiceType.Value);
            }
            if (input.WhichBoughtInfoContact)
            {
                var companyIds = _paidRequestPossibleManager.GetCompaniesIdsWhichBoughtInfoUser(input.RequestId).GetAwaiter().GetResult();
                data = data.Where(x => companyIds.Contains(x.Id));
            }
            if (input.AcceptRequests.HasValue)
                data = data.Where(x => x.AcceptRequests == input.AcceptRequests.Value);
            if (input.AcceptPossibleRequests.HasValue)
                data = data.Where(x => x.AcceptRequests == input.AcceptPossibleRequests.Value);
            if (input.IsFeature.HasValue)
                data = data.Where(x => x.IsFeature == input.IsFeature.Value);
            if (input.GetCompaniesThatNeedToUpdate)
                data = data.Where(x => x.ParentCompanyId.HasValue);
            else
                data = data.Where(x => !x.ParentCompanyId.HasValue);
            return data;
        }


        protected override IQueryable<Company> ApplySorting(IQueryable<Company> query, PagedCompanyResultRequestDto input)
        {
            if (input.IsForFilter)
                return query;
            return query.OrderByDescending(x => x.CreationTime);
        }
        [AbpAuthorize(PermissionNames.Company_ChangeStatus)]
        public async Task<bool> ConfirmCompanyByAdmin(CompanyStatuesDto input)
        {
            Company company = await Repository.GetAsync(input.CompanyId);
            if (company is null)
                throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.Company);
            company.statues = input.Statues;
            company.ReasonRefuse = input.ReasonRefuse;
            await Repository.UpdateAsync(company);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            if (input.Statues == CompanyStatues.Approved)
            {
                await _notificationSender.SendNotificationForCompanyForApproved(new List<long> { company.UserId.Value });
            }
            if (input.Statues == CompanyStatues.Rejected && company.ParentCompanyId is null)
            {
                await _commissionGroupManager.RemoveCompanyFromCommission(company);
            }
            return true;
        }
        public async Task<CompanyStatuesDto> GetCompanyStatuesAsync(int companyId)
        {
            var company = await Repository.GetAsync(companyId);
            return new CompanyStatuesDto
            {
                Statues = company.statues,
                ReasonRefuse = company.ReasonRefuse,
            };
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.Company_ChangeStatus)]
        public async Task<bool> ChangeAcceptRequestOrPossibleRequestForCompanyAsync(AcceptRequestOrPossibleDto input)
        {
            var company = await _companyManager.GetCompanyEntityById(input.Id);
            company.AcceptRequests = input.AcceptRequests;
            company.AcceptPossibleRequests = input.AcceptPossibleRequests;
            await Repository.UpdateAsync(company);
            return true;
        }


        public async Task<PointStatuesDto> GetCompanyPoints(int companyId)
        {
            var company = await Repository.GetAsync(companyId);
            return new PointStatuesDto
            {
                NumberOfGiftedPoints = company.NumberOfGiftedPoints,
                NumberOfPaidPoints = company.NumberOfPaidPoints,
                TotalPoints = company.NumberOfPaidPoints + company.NumberOfGiftedPoints
            };
        }
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<List<RequestsForCompanyCountDto>> GetInfoAboutRequestsCount()
        {
            return await _selectedCompaniesBySystemForRequestManager.GetRequestsForCompanyCountDto();
        }
        [AbpAuthorize]
        public async Task<BooleanOutputDto> CheckIfCompanyOrBranchCanBeFeature(TinySelectedCompanyDto input)
        {
            if (input.Provider == Provider.Company)
            {
                var company = await _companyManager.GetSuperLiteEntityByIdAsync(input.Id);
                if (company.AcceptPossibleRequests && company.AcceptRequests)
                    return new BooleanOutputDto { CanBeFeature = true };
                return new BooleanOutputDto { CanBeFeature = false };

            }
            var companyBracnh = await _companyBranchManager.GetSuperLiteEntityByIdAsync(input.Id);
            if (companyBracnh.AcceptPossibleRequests && companyBracnh.AcceptRequests)
                return new BooleanOutputDto { CanBeFeature = true };
            return new BooleanOutputDto { CanBeFeature = false };
        }
        [HttpGet]
        [AbpAuthorize]
        public async Task<FeatureStatuesDto> GetCompanyFeatureStatues(int companyId)
        {
            var company = await _companyManager.GetSuperLiteEntityByIdAsync(companyId);
            return new FeatureStatuesDto
            {
                IsFeature = company.IsFeature,
                StartFeatureSubscribtionDate = company.StartFeatureSubscribtionDate,
                EndFeatureSubscribtionDate = company.EndFeatureSubscribtionDate,
            };
        }
        public async Task<OutPutBooleanStatuesDto> AddOrUpdateTimeWorkForCompany(CreateTiemOfWorkDto input)
        {
            await _companyManager.CheckIfCompanyExict(input.CompanyId.Value);
            await _companyManager.CheckIfCompanyHasTimeWorkThenDeleteIt(input.CompanyId.Value);
            List<TimeWork> timeWorksToInsert = ObjectMapper.Map(input.Timeworks, new List<TimeWork>());
            timeWorksToInsert.ForEach(x => x.CompanyId = input.CompanyId.Value);
            await _companyManager.InsertNewTimeWorksForCompany(timeWorksToInsert);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto { BooleanStatues = true };
        }
        [HttpPost]
        [AbpAuthorize(PermissionNames.Company_Including)]
        public async Task<RequestIncludeBranchDto> RequestToIncludeBranchToMainCompany(RequestIncludeBranchDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var user = await _userManager.Users.Where(x => x.DialCode == input.DialCode && x.UserName == input.PhoneNumber && x.PIN == input.PinCode && x.Type == UserType.CompanyBranchUser)
                    .FirstOrDefaultAsync();
                if (user == null)
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.User);
                var companyBranch = await _companyBranchManager.GetCompanyBranchByUserIdAsync(user.Id);
                if (companyBranch.CompanyId.HasValue)
                    throw new UserFriendlyException(403, Exceptions.IncompatibleValue, Tokens.CompanyBranch + " Already Had Company");
                await _smsSenderAppService.SendOTPVerificationSMS($"{input.DialCode}{input.PhoneNumber}");
                return input;

            }
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.Company_Including)]
        public async Task<OutPutBooleanStatuesDto> IncludeBranchToMainCompany(SureIncludeBranchDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await _companyManager.CheckIfCompanyExict(input.CompanyId);
                var response = await _smsSenderAppService.VerificationCheckOTP(input.Code, $"{input.DialCode}{input.PhoneNumber}");
                if (response?.status == "approved" || input.Code == "151997")
                {
                    var user = await _userManager.Users.Where(x => x.DialCode == input.DialCode && x.UserName == input.PhoneNumber)
                                   .FirstOrDefaultAsync();
                    var companyBranch = await _companyBranchManager.GetCompanyBranchByUserIdAsync(user.Id);
                    companyBranch.CompanyId = input.CompanyId;
                    companyBranch.Statues = null;
                    await _companyBranchManager.UpdateBranchAsync(companyBranch);
                    return new OutPutBooleanStatuesDto
                    {
                        BooleanStatues = true
                    };
                }
                else
                    throw new UserFriendlyException(string.Format(Exceptions.VerificationCodeIsnotCorrect));
            }
        }
        [HttpDelete]
        [AbpAuthorize(PermissionNames.Company_Delete)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var company = await Repository.GetAsync(input.Id);
            if (await _companyBranchManager.CheckIfCompanyHasBranches(input.Id))
                throw new UserFriendlyException(string.Format(Exceptions.CompanyCannotDeleteBecauseHasingChilds));
            var checkOffer = await _offerManager.CheckIfCompanyHasAnyOfferNeedToProcess(input.Id);
            if (checkOffer is (OfferStatues.Approved or OfferStatues.SelectedByUser))
                throw new UserFriendlyException(string.Format(Exceptions.CompanyCannotDelete, L($"{checkOffer.Value}")));
            await _offerManager.DeleteAllUnNeededOffers(input.Id);
            await _companyManager.DeleteUserForCompanyOrBranchAsync(company.UserId.Value);
            await Repository.DeleteAsync(company);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        //[HttpPut]
        //public async Task<CompanyDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        //{
        //    CheckUpdatePermission();
        //    var Company = await _CompanyManager.GetLiteEntityByIdAsync(input.Id);
        //    Company.IsActive = !Company.IsActive;
        //    Company.LastModificationTime = DateTime.UtcNow;
        //    await _CompanyRepository.UpdateAsync(Company);
        //    return MapToEntityDto(Company);
        //}

    }
}
