using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.SubServices;
using Mofleet.Domain.SubServices.Dto;
using Mofleet.Domain.Toolss;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.AttributesForSourceType
{
    public class SubServiceAppService : MofleetAsyncCrudAppService<SubService, SubServiceDetailsDto, int, LiteSubServiceDto
        , PagedSubServiceResultRequestDto, CreateSubServiceDto, UpdateSubServiceDto>, ISubServiceAppService
    {
        private readonly SourceTypeManager _sourceTypeManager;
        private readonly SubServiceManager _SubServiceManager;
        private readonly AttachmentManager _attachmentManager;
        private readonly IServiceValueManager _serviceValueManager;
        private readonly IToolManger _toolManager;
        private readonly IServiceValueForOfferManager _serviceValueForOfferManager;
        private readonly IMapper _mapper;
        public SubServiceAppService(IRepository<SubService, int> repository,
            SourceTypeManager sourceTypeManager,
            IServiceValueManager serviceValueManager,
            IToolManger toolManager,
            IServiceValueForOfferManager serviceValueForOfferManager,
            SubServiceManager SubServiceManager, IMapper mapper, AttachmentManager attachmentManager) : base(repository)
        {
            _sourceTypeManager = sourceTypeManager;
            _SubServiceManager = SubServiceManager;
            _mapper = mapper;
            _serviceValueManager = serviceValueManager;
            _toolManager = toolManager;
            _serviceValueForOfferManager = serviceValueForOfferManager;
            _attachmentManager = attachmentManager;
        }
        [AbpAuthorize(PermissionNames.SubService_FullControl)]
        public override async Task<SubServiceDetailsDto> CreateAsync(CreateSubServiceDto input)
        {
            if (await _SubServiceManager.CheckIfSubServiceIsExist(input.Translations))
                throw new UserFriendlyException(404, Exceptions.ObjectIsAlreadyExist, Tokens.SubService);
            var subService = ObjectMapper.Map<SubService>(input);
            subService.ServiceId = input.ServiceId;
            await Repository.InsertAsync(subService);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, AttachmentRefType.SubService, subService.Id);
            return ObjectMapper.Map<SubServiceDetailsDto>(subService);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<SubServiceDetailsDto> GetAsync(EntityDto<int> input)
        {
            var subService = await _SubServiceManager.GetEntityByIdAsync(input.Id);
            var attachment = await _attachmentManager.GetElementByRefAsync(subService.Id, AttachmentRefType.SubService);
            if (attachment is not null)
            {
                subService.Attachment = new LiteAttachmentDto
                {
                    Id = attachment.Id,
                    Url = _attachmentManager.GetUrl(attachment),
                    LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment),
                };
            }
            return subService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteSubServiceDto>> GetAllAsync(PagedSubServiceResultRequestDto input)
        {

            var result = await base.GetAllAsync(input);
            var attachments = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.SubService);
            foreach (var item in result.Items)
            {
                var attachment = attachments.Where(x => x.RefId == item.Id).FirstOrDefault();
                if (attachment is not null)
                {
                    item.Attachment = new LiteAttachmentDto
                    {
                        Id = attachment.Id,
                        Url = _attachmentManager.GetUrl(attachment),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment),
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
        [AbpAuthorize(PermissionNames.SubService_FullControl)]
        public override async Task<SubServiceDetailsDto> UpdateAsync(UpdateSubServiceDto input)
        {
            var subService = await _SubServiceManager.GetFullEntityByIdAsync(input.Id);
            await _SubServiceManager.HardDeleteForEntityTranslation(subService.Translations.ToList());
            MapToEntity(input, subService);
            subService.ServiceId = input.ServiceId;
            var oldAttachment = await _attachmentManager.GetElementByRefAsync(input.Id, AttachmentRefType.SubService);
            if (oldAttachment is not null)
            {
                await _attachmentManager.DeleteRefIdAsync(oldAttachment);
            }
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, AttachmentRefType.SubService, input.Id);
            subService.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(subService);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(subService);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.SubService_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var subService = await _SubServiceManager.GetFullEntityByIdAsync(input.Id);

            if (subService is null)
                throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.SubService);

            if (await _serviceValueManager.CheckIfSubServiceBelongsToRequest(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);

            if (await _serviceValueManager.CheckIfSubServiceBelongsToCompanyOrCompanyBranch(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);

            if (await _serviceValueForOfferManager.CheckIfSubServiceBelongsToOffer(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);


            List<int> subServiceIds = new List<int> { subService.Id };
            await _toolManager.DeleteToolsForSubServices(subServiceIds);
            await _SubServiceManager.HardDeleteForEntityTranslation(subService.Translations.ToList());
            await Repository.DeleteAsync(subService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<SubService> CreateFilteredQuery(PagedSubServiceResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations).AsNoTracking();
            if (!input.KeyWord.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.KeyWord)).Any()).AsNoTracking();
            if (input.ServiceId.HasValue)
                data = data.Where(x => x.ServiceId == input.ServiceId.Value).AsNoTracking();
            if (input.ToolId.HasValue)
                data = data.Where(x => x.Tools.Select(x => x.Id).Contains(input.ToolId.Value)).AsNoTracking();
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<SubService> ApplySorting(IQueryable<SubService> query, PagedSubServiceResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime.Date);
        }

    }
}
