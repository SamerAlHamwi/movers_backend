using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.AttributesForSourceType.Dto;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.SourceTypes;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofleet.AttributesForSourceType
{
    public class AttributeForSourceTypeAppService : MofleetAsyncCrudAppService<AttributeForSourceType, AttributeForSourceTypeDetailsDto, int, LiteAttributeForSourceTypeDto
        , PagedAttributeForSourceTypeResultRequestDto, CreateAttributeForSourceTypeDto, UpdateAttributeForSourceTypeDto>, IAttributeForSourceTypeAppService
    {
        private readonly AttributeChoiceManger _attributeChoiceManger;
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly SourceTypeManager _sourceTypeManager;
        private readonly AttributeForSourceTypeManager _attributeForSourceTypeManager;
        public AttributeForSourceTypeAppService(IRepository<AttributeForSourceType, int> repository,
            AttributeChoiceManger attributeChoiceManger, IRequestForQuotationManager requestForQuotationManager,
            SourceTypeManager sourceTypeManager, AttributeForSourceTypeManager attributeForSourceTypeManager) : base(repository)
        {
            _attributeChoiceManger = attributeChoiceManger;
            _requestForQuotationManager = requestForQuotationManager;
            _sourceTypeManager = sourceTypeManager;
            _attributeForSourceTypeManager = attributeForSourceTypeManager;
        }
        [AbpAuthorize(PermissionNames.AttributeForSourceType_FullControl)]
        public override async Task<AttributeForSourceTypeDetailsDto> CreateAsync(CreateAttributeForSourceTypeDto input)
        {
            var sourceTypes = new List<SourceType>();
            foreach (var id in input.SourceTypeIds)
            {
                var sourceType = await _sourceTypeManager.GetLiteEntityByIdAsync(id);
                sourceTypes.Add(sourceType);
            }
            AttributeForSourceType attributeForSourceType = ObjectMapper.Map<AttributeForSourceType>(input);
            attributeForSourceType.SourceTypes = sourceTypes;
            attributeForSourceType.IsActive = true;
            await Repository.InsertAsync(attributeForSourceType);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return ObjectMapper.Map<AttributeForSourceTypeDetailsDto>(attributeForSourceType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AttributeForSourceTypeDetailsDto> GetAsync(EntityDto<int> input)
        {
            var AttributeForSourceType = await _attributeForSourceTypeManager.GetEntityByIdAsync(input.Id) ??
                throw new UserFriendlyException(Exceptions.ObjectWasNotFound, Tokens.AttributeForSourceType);
            AttributeForSourceTypeDetailsDto AttributeForSourceTypeDto = MapToEntityDto(AttributeForSourceType);
            return AttributeForSourceTypeDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteAttributeForSourceTypeDto>> GetAllAsync(PagedAttributeForSourceTypeResultRequestDto input)
        {

            var result = await base.GetAllAsync(input);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.AttributeForSourceType_FullControl)]
        public override async Task<AttributeForSourceTypeDetailsDto> UpdateAsync(UpdateAttributeForSourceTypeDto input)
        {
            CheckUpdatePermission();
            var attribute = await _attributeForSourceTypeManager.GetEntityByIdAsync(input.Id);
            await _attributeForSourceTypeManager.SoftDeleteForEntityTranslation(attribute.Translations.ToList());
            MapToEntity(input, attribute);
            var oldSourceTypes = attribute.SourceTypes.ToList();
            var newSourceTypes = new List<SourceType>();
            foreach (var i in input.SourceTypeIds)
            {
                newSourceTypes.Add(await _sourceTypeManager.GetLiteEntityByIdAsync(i));
            }
            var sourceTypesToDelete = oldSourceTypes.Except(newSourceTypes).ToList();
            foreach (var item in sourceTypesToDelete)
            {
                attribute.SourceTypes.Remove(item);
            }
            foreach (var item in newSourceTypes)
            {
                if (!attribute.SourceTypes.Contains(item))
                    attribute.SourceTypes.Add(item);
            }
            await Repository.UpdateAsync(attribute);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(attribute);

        }

        /// <summary>
        /// Delete Attribute For Source Type Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.AttributeForSourceType_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var AttributeForSourceType = await _attributeForSourceTypeManager.GetEntityByIdAsync(input.Id);
            await _requestForQuotationManager.CheckIfThereIsRequestBelongsToAttribute(input.Id);
            if (AttributeForSourceType.AttributeChoices.Count > 0 || AttributeForSourceType.AttributeChoices != null)
                await _attributeChoiceManger.DeleteAllAttributeChoiceInAttribute(AttributeForSourceType.Id);
            await _attributeForSourceTypeManager.SoftDeleteForEntityTranslation(AttributeForSourceType.Translations.ToList());
            await Repository.DeleteAsync(AttributeForSourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<AttributeForSourceType> CreateFilteredQuery(PagedAttributeForSourceTypeResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.SourceTypes).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.AttributeChoices.Where(x => x.IsAttributeChoiceParent)).ThenInclude(x => x.Translations).AsNoTracking();
            if (!input.KeyWord.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.KeyWord)).Any()).AsNoTracking();
            if (input.SourceTypeId.HasValue)
                data = data.Where(x => x.SourceTypes.Select(x => x.Id).Contains(input.SourceTypeId.Value)).AsNoTracking();
            if (input.IsActive.HasValue)
                data = data.Where(x => x.IsActive == input.IsActive.Value).AsNoTracking();
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<AttributeForSourceType> ApplySorting(IQueryable<AttributeForSourceType> query, PagedAttributeForSourceTypeResultRequestDto input)
        {
            return query.OrderBy(r => r.CreationTime);
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.AttributeForSourceType_FullControl)]
        public async Task<AttributeForSourceTypeDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            CheckUpdatePermission();
            var entity = await Repository.GetAsync(input.Id);
            entity.IsActive = input.IsActive;
            entity.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(entity);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return MapToEntityDto(entity);
        }

    }
}
