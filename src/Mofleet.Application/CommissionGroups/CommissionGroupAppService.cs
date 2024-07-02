using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Configuration;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.CommissionGroups.Dtos;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.RequestForQuotations.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.CommissionGroups
{
    public class CommissionGroupAppService : MofleetAsyncCrudAppService<CommissionGroup, CommissionGroupDetailsDto, int, LiteCommissionGroupDto, PagedCommissionGroupResultRequestDto,
        CreateCommissionGroupDto, UpdateCommissionGroupDto>, ICommissionGroupAppService
    {
        private readonly ICompanyManager _companyManager;
        private readonly ICommissionGroupManager _commissionGroupManager;
        private readonly IMapper _mapper;
        private readonly IRepository<CommissionGroup> _commissionGroupRepository;
        private readonly ICompanyBranchManager _companyBranchManager;
        public CommissionGroupAppService(IRepository<CommissionGroup, int> repository,
            ICompanyManager companyManager,
            ICommissionGroupManager commissionGroupManager,
            IMapper mapper,
            IRepository<CommissionGroup> commissionGroupRepository,
            ICompanyBranchManager companyBranchManager) : base(repository)
        {
            _companyManager = companyManager;
            _commissionGroupManager = commissionGroupManager;
            _mapper = mapper;
            _commissionGroupRepository = commissionGroupRepository;
            _companyBranchManager = companyBranchManager;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<CommissionGroupDetailsDto> GetAsync(EntityDto<int> input)
        {
            return new CommissionGroupDetailsDto();
        }
        [AbpAuthorize(PermissionNames.CommissionGroup_FullControl)]
        public override async Task<CommissionGroupDetailsDto> CreateAsync(CreateCommissionGroupDto input)
        {
            await _commissionGroupManager.CheckIfNameWasExisted(input.Name);
            var commissionGroup = ObjectMapper.Map<CommissionGroup>(input);
            if (input.CompanyIds is not null)
            {
                var companies = await _companyManager.GetListOfCompany(input.CompanyIds);
                if (companies.Count() > 0)
                    commissionGroup.Companies = companies;
            }
            await Repository.InsertAsync(commissionGroup);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new CommissionGroupDetailsDto() { Id = commissionGroup.Id };
        }
        public override async Task<PagedResultDto<LiteCommissionGroupDto>> GetAllAsync(PagedCommissionGroupResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }
        [AbpAuthorize(PermissionNames.CommissionGroup_FullControl)]
        public async Task<bool> SwipeCompanyFromGroupToAnother(SwipedInputDto input)
        {
            await _commissionGroupManager.CheckIfGroupContainCompanyAsync(input.OldGroupId, input.CompanyId);
            var oldGroup = await _commissionGroupManager.GetCommissionGroupAsync(input.OldGroupId);
            var newGroup = await _commissionGroupManager.GetCommissionGroupAsync(input.NewGroupId);
            var companyAtOldGroup = oldGroup.Companies.Where(x => x.Id == input.CompanyId).FirstOrDefault();
            oldGroup.Companies.Remove(companyAtOldGroup);
            newGroup.Companies.Add(companyAtOldGroup);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }
        [AbpAuthorize(PermissionNames.CommissionGroup_FullControl)]
        public override async Task<CommissionGroupDetailsDto> UpdateAsync(UpdateCommissionGroupDto input)
        {
            try
            {
                CommissionGroup group = await _commissionGroupManager.GetCommissionGroupAsync(input.Id);
                group.Name = input.Name;

                if (input.CompanyIds.Count() > 0)
                {
                    if (group.Companies is not null)
                    {
                        foreach (var companyId in input.CompanyIds)
                        {
                            var company = group.Companies.Where(x => x.Id == companyId).FirstOrDefault();
                            if (company is not null)
                                group.Companies.Remove(company);
                        }
                    }
                    var companies = await _companyManager.GetListOfCompany(input.CompanyIds);
                    group.Companies = companies;
                }
                await _commissionGroupRepository.UpdateAsync(group);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return new CommissionGroupDetailsDto() { Id = input.Id };
            }
            catch (Exception ex) { throw; }
        }
        public async Task<CommissionGroupDto> GetCommissionForCompanyOrCompanyBranch(TinySelectedCompanyDto input)
        {
            if (input.Provider == Provider.Company)
            {
                return await _commissionGroupManager.GetCommissionByCompanyIdAsync(input.Id);
            }
            else
            {
                var companyBranch = await _companyBranchManager.GetSuperLiteEntityByIdAsync(input.Id);
                if (!companyBranch.CompanyId.HasValue)
                    return new CommissionGroupDto
                    {
                        Commission = await SettingManager.GetSettingValueAsync<double>(AppSettingNames.CommissionForBranchesWithoutCompany),
                    };
                return await _commissionGroupManager.GetCommissionByCompanyIdAsync(companyBranch.CompanyId.Value);
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [AbpAuthorize(PermissionNames.CommissionGroup_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {

        }
        protected override IQueryable<CommissionGroup> CreateFilteredQuery(PagedCommissionGroupResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Companies).ThenInclude(x => x.Translations).AsNoTracking();
            return data;
        }
        protected override IQueryable<CommissionGroup> ApplySorting(IQueryable<CommissionGroup> query, PagedCommissionGroupResultRequestDto input)
        {
            return query.OrderBy(r => r.Name);
        }

    }
}
