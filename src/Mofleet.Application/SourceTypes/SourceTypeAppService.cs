using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.SourceTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.SourceTypes
{
    public class SourceTypeAppService : MofleetAsyncCrudAppService<SourceType, SourceTypeDetailsDto, int, LiteSourceTypeDto, PagedSourceTypeResultRequestDto,
        CreateSourceTypeDto, UpdateSourceTypeDto>
    {
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly ISourceTypeManager _sourceTypeManager;
        private readonly IRepository<SourceType> _sourceTypeRepository;
        private readonly IRepository<SourceTypeTranslation> _sourceTranslationRepository;
        private readonly IAttachmentManager _attachmentManager;
        private readonly IAttributeForSourceTypeManager _attributeForSourceTypeManager;
        public SourceTypeAppService(IRepository<SourceType, int> repository,
            IRequestForQuotationManager requestForQuotationManager, ISourceTypeManager sourceTypeManager,
            IRepository<SourceType> sourceTypeRepository, IRepository<SourceTypeTranslation> sourceTranslationRepository,
            IAttachmentManager attachmentManager, IAttributeForSourceTypeManager attributeForSourceTypeManager) : base(repository)
        {
            _requestForQuotationManager = requestForQuotationManager;
            _sourceTypeManager = sourceTypeManager;
            _sourceTypeRepository = sourceTypeRepository;
            _sourceTranslationRepository = sourceTranslationRepository;
            _attachmentManager = attachmentManager;
            _attributeForSourceTypeManager = attributeForSourceTypeManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<SourceTypeDetailsDto> GetAsync(EntityDto<int> input)
        {
            SourceType SourceType = await _sourceTypeManager.GetLiteEntityByIdAsync(input.Id);
            var icon = await _attachmentManager.GetElementByRefAsync(SourceType.Id, AttachmentRefType.SourceTypeIcon);
            SourceTypeDetailsDto SourceTypeDto = MapToEntityDto(SourceType);

            if (icon is not null)
            {
                SourceTypeDto.Icon = new LiteAttachmentDto
                {
                    Id = icon.Id,
                    Url = _attachmentManager.GetUrl(icon),
                    LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(icon)
                };
            }
            return SourceTypeDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteSourceTypeDto>> GetAllAsync(PagedSourceTypeResultRequestDto input)
        {

            var result = await base.GetAllAsync(input);
            foreach (LiteSourceTypeDto item in result.Items)
            {
                var icon = await _attachmentManager.GetElementByRefAsync(item.Id, AttachmentRefType.SourceTypeIcon);
                if (icon is not null)
                {
                    item.Icon = new LiteAttachmentDto
                    {
                        Id = icon.Id,
                        Url = _attachmentManager.GetUrl(icon),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(icon)
                    };
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.SourceType_FullControl)]
        public override async Task<SourceTypeDetailsDto> CreateAsync(CreateSourceTypeDto input)
        {
            CheckCreatePermission();
            var Translation = ObjectMapper.Map<List<SourceTypeTranslation>>(input.Translations);
            //if (await _SourceTypeManager.CheckIfSourceTypeIsExist(Translation))
            //    throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.SourceType));
            var SourceType = ObjectMapper.Map<SourceType>(input);
            SourceType.CreationTime = DateTime.UtcNow;
            SourceType.IsActive = true;
            var insertedSourceTypeId = await Repository.InsertAndGetIdAsync(SourceType);
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.IconId, AttachmentRefType.SourceTypeIcon, insertedSourceTypeId);
            UnitOfWorkManager.Current.SaveChanges();
            return MapToEntityDto(SourceType);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.SourceType_FullControl)]
        public override async Task<SourceTypeDetailsDto> UpdateAsync(UpdateSourceTypeDto input)
        {
            CheckUpdatePermission();
            var SourceType = await _sourceTypeManager.GetLiteEntityByIdAsync(input.Id);
            if (input.IsMainForPoints || input.PointsToBuyRequest > 0 || input.PointsToGiftToCompany > 0)
                await _attributeForSourceTypeManager.CheckIfAttribteChoiceHasPoints(SourceType.Id);
            SourceType.Translations.Clear();
            MapToEntity(input, SourceType);
            //if (!input.AttributesIds.IsNullOrEmpty())
            //{
            //    var AttributeidsForSourceType = await _attributeManger.GetAllAttrributeForSourceTypeWithinItsSourceTypeTypeAttributes(input.Id);
            //    var Attributes = await _attributeManger.GetAttributesAsync(input.AttributesIds);
            //    var AttributesToAdd = Attributes.Except(AttributeidsForSourceType).ToList();
            //    foreach (var attr in AttributesToAdd)
            //    {
            //        SourceType.Attributes.Add(attr);
            //    }
            //}
            SourceType.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(SourceType);

            var oldIcon = await _attachmentManager.GetElementByRefAsync(input.Id, AttachmentRefType.SourceTypeIcon);
            if (oldIcon is not null)
            {
                await _attachmentManager.DeleteRefIdAsync(oldIcon);
            }
            var newIcon = await _attachmentManager.GetByIdAsync(input.IconId);
            if (newIcon is not null)
            {
                await _attachmentManager.UpdateRefIdAsync(newIcon, input.Id);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var result = MapToEntityDto(SourceType);
            result.Icon = new LiteAttachmentDto
            {
                Id = newIcon.Id,
                Url = _attachmentManager.GetUrl(newIcon),
                LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(newIcon)
            };
            return result;

        }

        /// <summary>
        /// Delete Source Type By ID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.SourceType_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var SourceType = await _sourceTypeManager.GetLiteEntityByIdAsync(input.Id);
            await _requestForQuotationManager.CheckIfThereIsRequestBelongsToSourceType(SourceType.Id);
            if (SourceType.Attributes.Any())
                await _sourceTypeManager.DeleteAllAttributeInSourceTypeBySourceTypeId(SourceType.Id);
            await _sourceTypeManager.SoftDeleteForEntityTranslation(SourceType.Translations.ToList());
            SourceType.Attributes.Clear();
            UnitOfWorkManager.Current.SaveChanges();
            await _sourceTypeRepository.DeleteAsync(SourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<SourceType> CreateFilteredQuery(PagedSourceTypeResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Attributes).AsNoTracking();
            data = data.Include(x => x.Translations).AsNoTracking();
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any());
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
        protected override IQueryable<SourceType> ApplySorting(IQueryable<SourceType> query, PagedSourceTypeResultRequestDto input)
        {
            return query.OrderBy(x => x.Id);
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.SourceType_FullControl)]
        public async Task<SourceTypeDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
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
