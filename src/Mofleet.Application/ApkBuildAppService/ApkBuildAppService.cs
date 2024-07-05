﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.ApkBuilds;
using Mofleet.Domain.ApkBuilds.Dtos;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Localization.SourceFiles;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.ApkBuildAppService
{
    public class ApkBuildAppService : MofleetAsyncCrudAppService<ApkBuild, ApkBuildDetailsDto, int, LiteApkBuildDto, PagedApkBuildResultRequestDto,
        CreateApkBuildDto, UpdateApkBuildDto>, IApkBuildAppService
    {
        private readonly IMapper _mapper;
        public ApkBuildAppService(IRepository<ApkBuild, int> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task<PagedResultDto<LiteApkBuildDto>> GetAllAsync(PagedApkBuildResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }
        [AbpAuthorize(PermissionNames.ApkBuild_FullControl)]
        public override async Task<ApkBuildDetailsDto> CreateAsync(CreateApkBuildDto input)
        {
            var apkBuildToInsert = ObjectMapper.Map<ApkBuild>(input);
            apkBuildToInsert.UpdateOptions = UpdateOptions.Nothing;
            await Repository.InsertAsync(apkBuildToInsert);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new ApkBuildDetailsDto()
            {
                Id = apkBuildToInsert.Id
            };
        }
        [AbpAuthorize(PermissionNames.ApkBuild_FullControl)]
        public override async Task<ApkBuildDetailsDto> UpdateAsync(UpdateApkBuildDto input)
        {
            try
            {
                var apk = await Repository.GetAsync(input.Id);
                if (apk is null)
                    throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.Entity);
                var updated = apk.UpdateOptions;
                _mapper.Map(input, apk);
                apk.UpdateOptions = updated;
                await Repository.UpdateAsync(apk);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return new ApkBuildDetailsDto();
            }
            catch (Exception ex) { throw; }
        }
        [AbpAuthorize(PermissionNames.ApkBuild_FullControl)]
        public async Task<OutPutBooleanStatuesDto> ChangeUpdateOptionsForApk(InputApkNuildStatuesDto input)
        {
            var apk = await Repository.GetAsync(input.Id);
            if (apk is null)
                throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.Entity);
            apk.UpdateOptions = input.UpdateOptions;
            await Repository.UpdateAsync(apk);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto()
            {
                BooleanStatues = true
            };

        }
        protected override IQueryable<ApkBuild> CreateFilteredQuery(PagedApkBuildResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            if (input.AppType.HasValue)
                data = data.Where(x => x.AppType == input.AppType.Value);
            if (input.SystemType.HasValue)
                data = data.Where(x => x.SystemType == input.SystemType.Value);
            if (input.UpdateOptions.HasValue)
                data = data.Where(x => x.UpdateOptions == input.UpdateOptions.Value);
            return data;
        }
        protected override IQueryable<ApkBuild> ApplySorting(IQueryable<ApkBuild> query, PagedApkBuildResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime);
        }
        public async Task<OutputApkBuildStatuesDto> CheckIfAppNeedToUpdate(InputApkBuildStatuesDto input)
        {
            var apk = await Repository.GetAll()
                .Where(x => x.AppType == input.AppType && x.VersionCode == input.VersionCode && x.SystemType == input.SystemType)
                .FirstOrDefaultAsync();
            if (apk is not null)
                return new OutputApkBuildStatuesDto()
                {
                    UpdateOptions = apk.UpdateOptions,
                    ApkIsNotFound = false
                };
            else
                return new OutputApkBuildStatuesDto()
                {
                    UpdateOptions = UpdateOptions.Nothing,
                    ApkIsNotFound = true
                };
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<ApkBuildDetailsDto> GetAsync(EntityDto<int> input)
        {
            return new ApkBuildDetailsDto();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task DeleteAsync(EntityDto<int> input)
        { }
    }
}
