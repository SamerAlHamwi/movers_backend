using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributeChoices.Dto;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.SourceTypes;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofleet.AttributeChoices
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class AttributeChoiceAppService : MofleetAsyncCrudAppService<AttributeChoice, AttributeChoiceDetailsDto, int, LiteAttributeChoiceDto,
        PagedAttributeChoiceResultRequestDto, CreateAttributeChoiceDto, UpdateAttributeChoiceDto>,
        IAttributeChoiceAppService
    {
        private readonly IAttributeChoiceManger _attributeChoiceManager;
        private readonly IRepository<AttributeChoiceTranslation> _attributeChoiceTranslationRepository;
        private readonly IRepository<AttributeChoice> _attributeChoiceRepository;
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly ISourceTypeManager _sourceTypeManager;


        /// <summary>
        /// Attribute Choice AppService
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="attributeChoiceManager"></param>
        /// <param name="attributeChoiceTranslationRepository"></param>
        public AttributeChoiceAppService(IRepository<AttributeChoice> attributeChoiceRepository,
            IRequestForQuotationManager requestForQuotationManager,
            IAttributeChoiceManger attributeChoiceManager,
            IRepository<AttributeChoiceTranslation> attributeChoiceTranslationRepository,
            ISourceTypeManager sourceTypeManager)
         : base(attributeChoiceRepository)
        {
            _attributeChoiceManager = attributeChoiceManager;
            _attributeChoiceTranslationRepository = attributeChoiceTranslationRepository;
            _attributeChoiceRepository = attributeChoiceRepository;
            _requestForQuotationManager = requestForQuotationManager;
            _sourceTypeManager = sourceTypeManager;
        }
        /// <summary>
        /// Get Attribute Choice Details ById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AttributeChoiceDetailsDto> GetAsync(EntityDto<int> input)
        {
            var attributeChoice = await _attributeChoiceManager.GetEntityByIdAsync(input.Id);
            if (attributeChoice is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.AttributeChoice));
            return MapToEntityDto(attributeChoice);

        }
        /// <summary>
        /// Get All AttributeChoice Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteAttributeChoiceDto>> GetAllAsync(PagedAttributeChoiceResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }
        /// <summary>
        /// Add New Attribute Choice Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.AttributeChoice_FullControl)]
        public override async Task<AttributeChoiceDetailsDto> CreateAsync(CreateAttributeChoiceDto input)
        {
            CheckCreatePermission();
            var Translation = ObjectMapper.Map<List<AttributeChoiceTranslation>>(input.Translations);
            //if (await _attributeChoiceManager.CheckIfAttributeChoiceIsExist(Translation))
            //    throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.AttributeChoice));

            AttributeChoice AttributeChoice = ObjectMapper.Map<AttributeChoice>(input);
            AttributeChoice.IsActive = true;
            if (input.AttributeId.HasValue)
                AttributeChoice.AttributeForSourceTypeId = input.AttributeId;
            if (input.PointsToGiftToCompany > 0 || input.PointsToBuyRequest > 0)
                await _sourceTypeManager.CheckIfSourceTypeWithMainPointsToGiftOrNotByAttributeId(input.AttributeId.Value);
            await Repository.InsertAsync(AttributeChoice);
            UnitOfWorkManager.Current.SaveChanges();
            return MapToEntityDto(AttributeChoice);

        }
        /// <summary>
        /// Update Attribute Choice Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.AttributeChoice_FullControl)]
        public override async Task<AttributeChoiceDetailsDto> UpdateAsync(UpdateAttributeChoiceDto input)
        {
            CheckUpdatePermission();
            var attributeChoice = await _attributeChoiceManager.GetEntityByIdAsync(input.Id);
            if (attributeChoice is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.AttributeChoice));
            attributeChoice.Translations.Clear();
            MapToEntity(input, attributeChoice);
            if (input.AttributeId.HasValue)
                attributeChoice.AttributeForSourceTypeId = input.AttributeId;
            if (input.PointsToGiftToCompany > 0 || input.PointsToBuyRequest > 0)
                await _sourceTypeManager.CheckIfSourceTypeWithMainPointsToGiftOrNotByAttributeId(input.AttributeId.Value);
            attributeChoice.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(attributeChoice);
            return MapToEntityDto(attributeChoice);

        }

        /// <summary>
        /// Delete Attribute Choice Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.AttributeChoice_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var attributeChoice = await _attributeChoiceManager.GetLiteEntityByIdAsync(input.Id);
            await _requestForQuotationManager.CheckIfThereIsRequestBelongsToAttributeChoice(attributeChoice);
            if (attributeChoice.IsAttributeChoiceParent)
                await _attributeChoiceManager.DeleteChildrenOfAttributeChoice(attributeChoice.Id);
            await _attributeChoiceManager.SoftDeleteForEntityTranslation(attributeChoice.Translations.ToList());
            await Repository.DeleteAsync(attributeChoice);
        }

        /// <summary>
        /// Filter For A Group Of Attribute Choice
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<AttributeChoice> CreateFilteredQuery(PagedAttributeChoiceResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.Attribute).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.ParentChoice).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Where(x => x.IsDeleted == false).AsNoTracking();
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any()).AsNoTracking();
            if (input.AttributeId.HasValue)
                data = data.Where(x => x.AttributeForSourceTypeId == input.AttributeId.Value).AsNoTracking();
            if (input.IsParent.HasValue)
                data = data.Where(x => x.IsAttributeChoiceParent == input.IsParent.Value).AsNoTracking();
            if (input.ParentId != null)
            {
                data = data.Where(x => x.AttributeChociceParentId.HasValue).AsNoTracking();
                data = data.Where(x => x.AttributeChociceParentId == input.ParentId).AsNoTracking();
            }
            return data;

        }
        /// <summary>
        /// Sorting Filtered Attribute Choice
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<AttributeChoice> ApplySorting(IQueryable<AttributeChoice> query, PagedAttributeChoiceResultRequestDto input)
        {
            return query.OrderBy(r => r.CreationTime);
        }
        /// <summary>
        /// Switch Activation Of An Attribute Choice
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        [AbpAuthorize(PermissionNames.AttributeChoice_FullControl)]
        public async Task<AttributeChoiceDetailsDto> SwitchActivationAsync(AttributeChoiceSwitchActivationDto input)
        {
            CheckUpdatePermission();
            var attributeChoice = await _attributeChoiceManager.GetLiteEntityByIdAsync(input.Id);
            if (attributeChoice is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.AttributeChoice));
            attributeChoice.IsActive = !attributeChoice.IsActive;
            attributeChoice.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(attributeChoice);
            return MapToEntityDto(attributeChoice);
        }

        //[HttpPost]
        //public async Task<AttributeChoiceDetailsDto> CreateAttributeChoiceWithAttributeIdAsync(CreateAttributeChoiceWithAttributeIdDto input)
        //{
        //    CheckCreatePermission();
        //    var Translation = ObjectMapper.Map<List<AttributeChoiceTranslation>>(input.Translations);
        //    if (await _attributeChoiceManager.CheckIfAttributeChoiceIsExist(Translation))
        //        throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.AttributeChoice));
        //    var attributeChoice = ObjectMapper.Map<AttributeChoice>(input);
        //    attributeChoice.IsActive = true;
        //    attributeChoice.Value = attributeChoice.Translations.Where(x => x.Language == "en").Select(x => x.Name).FirstOrDefault();
        //    await _attributeChoiceRepository.InsertAsync(attributeChoice);
        //    return MapToEntityDto(attributeChoice);
        //}
    }
}
