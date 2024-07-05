using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
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
using Mofleet.Configuration;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.CommissionGroups.Dtos;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.Offers;
using Mofleet.Domain.PaidRequestPossibles;
using Mofleet.Domain.Regions;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.TimeWorks.Dtos;
using Mofleet.Localization.SourceFiles;
using Mofleet.NotificationSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.CompanyBranches
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class CompanyBranchAppService : MofleetAsyncCrudAppService<CompanyBranch, CompanyBranchDetailsDto, int, LiteCompanyBranchDto,
        PagedCompanyBranchResultRequestDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>,
        ICompanyBranchAppService
    {
        private readonly CityManager _cityManager;
        private readonly ServiceValueManager _serviceValueManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly RegionManager _regionManager;
        private readonly CompanyManager _companyManager;
        private readonly UserManager _userManager;
        private readonly IPaidRequestPossibleManager _paidRequestPossibleManager;
        private readonly ISelectedCompaniesBySystemForRequestManager _selectedCompaniesBySystemForRequestManager;
        private readonly ICompanyBranchManager _companyBranchManager;
        private readonly IRepository<CompanyBranch> _companyBranchRepository;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly IOfferManager _offerManager;
        private readonly INotificationSender _notificationSender;
        private readonly ICommissionGroupManager _commissionGroupManager;

        public CompanyBranchAppService(IRepository<CompanyBranch, int> repository, CityManager cityManager,
            ServiceValueManager serviceValueManager,
            IMapper mapper,
            UserRegistrationManager userRegistrationManager,
            RegionManager regionManager,
            CompanyManager companyManager,
            UserManager userManager,
            IPaidRequestPossibleManager paidRequestPossibleManager,
            ISelectedCompaniesBySystemForRequestManager selectedCompaniesBySystemForRequestManager,
            ICompanyBranchManager companyBranchManager,
            IRepository<CompanyBranch> companyBranchRepository,
            IServiceManager serviceManager,
            IOfferManager offerManager, INotificationSender notificationSender, ICommissionGroupManager commissionGroupManager) : base(repository)
        {
            _cityManager = cityManager;
            _serviceValueManager = serviceValueManager;
            _userRegistrationManager = userRegistrationManager;
            _regionManager = regionManager;
            _companyManager = companyManager;
            _companyBranchManager = companyBranchManager;
            _userManager = userManager;
            _paidRequestPossibleManager = paidRequestPossibleManager;
            _selectedCompaniesBySystemForRequestManager = selectedCompaniesBySystemForRequestManager;
            _companyBranchRepository = companyBranchRepository;
            _mapper = mapper;
            _serviceManager = serviceManager;
            _offerManager = offerManager;
            _notificationSender = notificationSender;
            _commissionGroupManager = commissionGroupManager;
        }
        [HttpGet]
        [AbpAuthorize(PermissionNames.CompanyBranch_Get)]
        public override async Task<CompanyBranchDetailsDto> GetAsync(EntityDto<int> input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var companyBranchDto = await _companyBranchManager.GetCompanyBranchDtoById(input.Id);
                companyBranchDto.Services = await _serviceValueManager.GetFullServicesByCompanyOrRequestIdAsync(input.Id, false, true);
                companyBranchDto.Region = await _regionManager.GetEntityDtoByIdAsync(companyBranchDto.Region.Id);
                companyBranchDto.GeneralRating = await _companyBranchManager.GetGeneralRatingDtoForComapnyBranch(input.Id);
                companyBranchDto.TimeOfWorks = await _companyBranchManager.GetTimeWorksDtoForCompanyBranch(input.Id);
                companyBranchDto.TotalPoints = companyBranchDto.NumberOfGiftedPoints + companyBranchDto.NumberOfPaidPoints;
                return companyBranchDto;
            }

        }
        [HttpGet]
        [AbpAuthorize]
        public async Task<IList<ReviewDetailsDto>> GetReviewDetailsById(EntityDto<int> input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                return await _companyBranchManager.GetReviewsForCompanyBranch(input.Id);
            }
        }
        [AbpAuthorize(PermissionNames.CompanyBranch_List)]
        public override async Task<PagedResultDto<LiteCompanyBranchDto>> GetAllAsync(PagedCompanyBranchResultRequestDto input)
        {
            try
            {
                var result = await base.GetAllAsync(input);
                var serviceIdsAndCompanyBranchIdsDto = await _serviceValueManager.GetServicesIdsAndCompanyBranchIdsAsync(result.Items.Select(x => x.Id).ToList());
                var services = await _serviceManager.GetAllServicesDtosAsync();
                var companyIdsThatSentOffer = new List<int>();
                double commission = await SettingManager.GetSettingValueAsync<double>(AppSettingNames.CommissionForBranchesWithoutCompany);
                if (input.GetCompnyBranchesWithRequest)
                    companyIdsThatSentOffer = await _offerManager.GetCompanyBranchIdsThatSentOffer(input.RequestId);
                List<CommissionGroupWithCompanyIdsDto> commissionForCompanies = new List<CommissionGroupWithCompanyIdsDto>();
                if (!input.GetBranchesWithoutCompany && !input.IsForFilter)
                    commissionForCompanies = await _commissionGroupManager.GetCommissionGroupByCompanyIds(result.Items.Where(x => x.Company != null).Select(x => x.Company.Id).ToList());

                foreach (var item in result.Items)
                {
                    var servicesIds = serviceIdsAndCompanyBranchIdsDto.Where(x => x.CompanyBranchId == item.Id).SelectMany(x => x.ServiceIds).ToList();
                    item.Services = services.Where(x => servicesIds.Contains(x.Id)).ToList();
                    if (input.GetCompnyBranchesWithRequest && companyIdsThatSentOffer.Any(x => x == item.Id))
                        item.IsThisCompanyProvideOffer = true;
                    if (item.Company is not null)
                        item.Company.CommissionGroup = commissionForCompanies.Where(x => x.Companies.Exists(x => x == item.Company.Id)).Select(x => x.Name).FirstOrDefault();
                    else
                        item.CommissionForBranchWithOutCompany = commission;
                }
                //Sort Item If Need To Filter
                if (input.IsForFilter)
                {
                    var countServicesOfRequest = await _serviceValueManager.GetCountOfServicesForRequest(input.RequestId);
                    var companiesWithCountOfServices = await _serviceValueManager.GetCountOfServicesForCompanyBranches(input.CompanyBranchIds);
                    List<LiteCompanyBranchDto> newItems = new List<LiteCompanyBranchDto>();
                    foreach (var item in input.CompanyBranchIds)
                    {
                        newItems.Add(result.Items.Where(x => x.Id == item).FirstOrDefault());
                    }
                    newItems.RemoveAll(x => x is null);
                    foreach (var item in newItems)
                    {
                        int count = companiesWithCountOfServices.Where(x => x.CompanyBranchId == item.Id).Select(x => x.Count).FirstOrDefault();
                        if (count != 0)
                        {
                            if (count > countServicesOfRequest)
                                item.CompatibilityRate = 100;
                            else
                                item.CompatibilityRate = (double)count / countServicesOfRequest * 100;

                        }
                    }
                    var newResult = new PagedResultDto<LiteCompanyBranchDto>
                    {
                        Items = newItems.OrderByDescending(x => x.CompatibilityRate).ToList(),
                        TotalCount = result.TotalCount
                    };

                    return newResult;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

        }

        [AbpAuthorize(PermissionNames.CompanyBranch_Create)]
        public override async Task<CompanyBranchDetailsDto> CreateAsync(CreateCompanyBranchDto input)
        {
            CheckCreatePermission();
            if (input.IsWithCompany)
                await _companyManager.CheckIfCompanyExict(input.CompanyId);
            await _serviceManager.CheckServicesIsCorrect(input.services);
            var cities = new List<City>();
            if (input.AvailableCitiesIds.Count > 0 && input.AvailableCitiesIds is not null)
                cities = await _cityManager.CheckAndGetCitiesById(input.AvailableCitiesIds);
            CompanyBranch companyBranch = ObjectMapper.Map<CompanyBranch>(input);
            companyBranch.IsActive = true;
            companyBranch.AvailableCities = cities;
            companyBranch.NumberOfTransfers = 0;
            companyBranch.AcceptPossibleRequests = false;
            companyBranch.AcceptRequests = false;
            if (input.IsWithCompany)
                companyBranch.User = await _userRegistrationManager.RegisterAsyncForUserCompanyBranch(input.userDto.PhoneNumber, input.userDto.DialCode, input.userDto.Password, input.userDto.EmailAddress, UserType.CompanyBranchUser);
            else
            {
                companyBranch.UserId = input.UserCompanyBranchId.Value;
                companyBranch.Statues = CompanyBranchStatues.Checking;
            }
            if (input.CompanyId == 0)
                companyBranch.CompanyId = null;
            var companyBranchId = await _companyBranchRepository.InsertAndGetIdAsync(companyBranch);
            await _serviceValueManager.InsertServiceValuesForCompanyBranch(input.services, input.CompanyId, companyBranchId);
            await AddOrUpdateTimeWorkForCompanyBranch(new CreateTiemOfWorkDto
            {
                CompanyBranchId = companyBranchId,
                Timeworks = input.Timeworks
            });
            var companyBranchDto = _mapper.Map<CompanyBranchDetailsDto>(companyBranch);
            return companyBranchDto;

        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.CompanyBranch_ChangeStatus)]
        public async Task<OutPutBooleanStatuesDto> ConfirmCompanyBranchByAdmin(CompanyBranchStatuesDto input)
        {
            CompanyBranch companyBranch = await Repository.GetAsync(input.CompanyBranchId);
            if (companyBranch is null)
                throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.CompanyBranch);
            companyBranch.Statues = input.Statues;
            companyBranch.ReasonRefuse = input.ReasonRefuse;
            await Repository.UpdateAsync(companyBranch);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            if (input.Statues == CompanyBranchStatues.Approved)
            {
                await _notificationSender.SendNotificationForCompanyForApproved(new List<long> { companyBranch.UserId.Value });
            }
            return new OutPutBooleanStatuesDto
            {
                BooleanStatues = true
            };
        }
        [AbpAuthorize(PermissionNames.CompanyBranch_Update)]
        public override async Task<CompanyBranchDetailsDto> UpdateAsync(UpdateCompanyBranchDto input)
        {
            try
            {
                CheckUpdatePermission();
                var companyBranch = await _companyBranchManager.GetEntityByIdAsync(input.Id);
                if (companyBranch is null)
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.CompanyBranch));
                await _serviceManager.CheckServicesIsCorrect(input.services);
                if (companyBranch.CompanyId is not null)
                    if (companyBranch.CompanyId != input.CompanyId)
                        throw new UserFriendlyException(Exceptions.IncompatibleValue, Tokens.Company + Tokens.CompanyBranch);
                companyBranch.Translations.Clear();
                var oldServices = companyBranch.services.ToList();
                var newServices = input.services;
                input.services = null;
                companyBranch = _mapper.Map(input, companyBranch);
                await _serviceValueManager.DeleteServiceValuesForCompanyBranch(input.Id);
                await _serviceValueManager.InsertServiceValuesForCompanyBranch(newServices, input.CompanyId, input.Id);
                if (input.CompanyId == 0)
                {
                    companyBranch.CompanyId = null;
                    companyBranch.Statues = CompanyBranchStatues.Checking;
                }
                if (input.AvailableCitiesIds.Count > 0)
                    await _companyBranchManager.UpdateCitiesForCompanyBranchAsync(input.AvailableCitiesIds, companyBranch);
                await _companyBranchRepository.UpdateAsync(companyBranch);
                await CurrentUnitOfWork.SaveChangesAsync();
                await AddOrUpdateTimeWorkForCompanyBranch(new CreateTiemOfWorkDto
                {
                    CompanyBranchId = input.Id,
                    Timeworks = input.Timeworks
                });
                return _mapper.Map(companyBranch, new CompanyBranchDetailsDto());
            }
            catch (Exception ex) { throw; }

        }

        //[AbpAuthorize(PermissionNames.CompanyBranch_Delete)]
        //public override async Task DeleteAsync(EntityDto<int> input)
        //{
        //    var branch = await _companyBranchRepository.FirstOrDefaultAsync(b => b.Id == input.Id);
        //    if (branch != null)
        //    {
        //        await _serviceValueManager.DeleteServiceValuesForCompanyBranch(input.Id);
        //        await _companyBranchRepository.DeleteAsync(branch);
        //        var user = _userManager.GetUserById((long)branch.UserId);
        //        await _userManager.DeleteAsync(user);

        //    }

        //}

        protected override IQueryable<CompanyBranch> CreateFilteredQuery(PagedCompanyBranchResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.Company).ThenInclude(x => x.Translations).AsNoTracking();
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
            if (input.CompanyId.HasValue)
                data = data.Where(x => x.CompanyId == input.CompanyId.Value);
            if (input.IsForFilter)
            {
                var companyBranchIds = _serviceValueManager.FilterCompatibleCompaniesWithRequestByServices(input.RequestId, false).GetAwaiter().GetResult();
                data = data.Where(x => companyBranchIds.CompanyBranchIds.Contains(x.Id)
                && (x.Company.statues == CompanyStatues.Approved || (x.Statues.HasValue && x.Statues.Value == CompanyBranchStatues.Approved))
                );
                input.CompanyBranchIds = companyBranchIds.CompanyBranchIds;
            }
            if (input.GetCompnyBranchesWithRequest)
            {
                var compnayIds = _selectedCompaniesBySystemForRequestManager.GetCompanyBranchIdsWithThisRequestAsync(input.RequestId).GetAwaiter().GetResult();
                data = data.Where(x => compnayIds.Contains(x.Id));
            }
            if (input.Statues.HasValue)
                data = data.Where(x => x.Company.statues == input.Statues);
            if (input.BranchStatues.HasValue)
                data = data.Where(x => x.Statues == input.BranchStatues);

            if (input.ServiceType.HasValue)
            {
                data = data.Where(x => x.ServiceType == input.ServiceType.Value);
            }
            if (input.WhichBoughtInfoContact)
            {
                var companyBranchIds = _paidRequestPossibleManager.GetCompanyBranchesIdsWhichBoughtInfoUser(input.RequestId).GetAwaiter().GetResult();
                data = data.Where(x => companyBranchIds.Contains(x.Id));
                return data;
            }
            if (input.IsFeature.HasValue)
                data = data.Where(x => x.IsFeature == input.IsFeature.Value);
            if (input.GetBranchesWithoutCompany)
                data = data.Where(x => !x.CompanyId.HasValue);
            if (input.AcceptRequests.HasValue)
                data = data.Where(x => x.AcceptRequests == input.AcceptRequests.Value);
            if (input.AcceptPossibleRequests.HasValue)
                data = data.Where(x => x.AcceptRequests == input.AcceptPossibleRequests.Value);

            return data;
        }


        protected override IQueryable<CompanyBranch> ApplySorting(IQueryable<CompanyBranch> query, PagedCompanyBranchResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }
        [HttpDelete]
        [AbpAuthorize(PermissionNames.CompanyBranch_Delete)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var companyBranch = await Repository.GetAsync(input.Id);
            var checkOffer = await _offerManager.CheckIfCompanyBranchHasAnyOfferNeedToProcess(input.Id);
            if (checkOffer is (OfferStatues.Approved or OfferStatues.SelectedByUser))
                throw new UserFriendlyException(string.Format(Exceptions.CompanyBranchCannotDelete, L($"{checkOffer.Value}")));
            await _offerManager.DeleteAllUnNeededOffers(input.Id, false);
            await _companyManager.DeleteUserForCompanyOrBranchAsync(companyBranch.UserId.Value);
            await Repository.DeleteAsync(companyBranch);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        public async Task<CompanyStatuesDto> GetCompanyBranchStatuesAsync(int companyBrnchId)
        {
            var companyBranch = await _companyBranchRepository.GetAsync(companyBrnchId);
            if (companyBranch.CompanyId.HasValue)
                return await _companyManager.GetCompanyStatuesByCompanyBranchId(companyBrnchId);
            else
                return new CompanyStatuesDto()
                {
                    ReasonRefuse = companyBranch.ReasonRefuse,
                    Statues = (CompanyStatues)companyBranch.Statues.Value
                };
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.CompanyBranch_ChangeStatus)]
        public async Task<bool> ChangeAcceptRequestOrPossibleRequestForCompanyBranchAsync(AcceptRequestOrPossibleDto input)
        {
            var company = await _companyBranchManager.GetCompanyBranchEntityById(input.Id);
            company.AcceptRequests = input.AcceptRequests;
            company.AcceptPossibleRequests = input.AcceptPossibleRequests;
            await Repository.UpdateAsync(company);
            return true;
        }

        public async Task<PointStatuesDto> GetCompanyBranchPoints(int companyBranchId)
        {
            var companyBranch = await Repository.GetAsync(companyBranchId);
            return new PointStatuesDto
            {
                NumberOfGiftedPoints = companyBranch.NumberOfGiftedPoints,
                NumberOfPaidPoints = companyBranch.NumberOfPaidPoints,
                TotalPoints = companyBranch.NumberOfPaidPoints + companyBranch.NumberOfGiftedPoints
            };
        }
        [AbpAuthorize]
        [Tags("Dashboard")]
        public async Task<List<RequestsForCompanyBranchCountDto>> GetInfoAboutRequestsCount()
        {
            return await _selectedCompaniesBySystemForRequestManager.GetRequestsForCompanyBranchCountDto();
        }
        public async Task<FeatureStatuesDto> GetCompanyBranchFeatureStatues(int companyBranchId)
        {
            var company = await _companyBranchManager.GetSuperLiteEntityByIdAsync(companyBranchId);
            return new FeatureStatuesDto
            {
                IsFeature = company.IsFeature,
                StartFeatureSubscribtionDate = company.StartFeatureSubscribtionDate,
                EndFeatureSubscribtionDate = company.EndFeatureSubscribtionDate,
            };
        }
        public async Task<OutPutBooleanStatuesDto> AddOrUpdateTimeWorkForCompanyBranch(CreateTiemOfWorkDto input)
        {
            await _companyBranchManager.CheckIfCompanyBrachExict(input.CompanyBranchId.Value);
            await _companyBranchManager.CheckIfCompanyBranchHasTimeWorkThenDeleteIt(input.CompanyBranchId.Value);
            List<TimeWork> timeWorksToInsert = ObjectMapper.Map(input.Timeworks, new List<TimeWork>());
            timeWorksToInsert.ForEach(x => x.CompanyBranchId = input.CompanyBranchId.Value);
            await _companyBranchManager.InsertNewTimeWorksForCompanyBranch(timeWorksToInsert);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto { BooleanStatues = true };
        }


    }
}
