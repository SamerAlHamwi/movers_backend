using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.services;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.SubServices;
using Mofleet.Localization.SourceFiles;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.AttributesForSourceType
{
    public class ServiceAppService : MofleetAsyncCrudAppService<Service, ServiceDetailsDto, int, LiteServiceDto
        , PagedServiceResultRequestDto, CreateServiceDto, UpdateServiceDto>, IServiceAppService
    {
        private readonly ServiceManager _serviceManager;
        private readonly AttachmentManager _attachmentManager;
        private readonly IServiceValueManager _serviceValueManager;
        private readonly ISubServiceManager _subServiceManager;
        private readonly IServiceValueForOfferManager _serviceValueForOfferManager;
        private readonly IMapper _mapper;
        public ServiceAppService(IRepository<Service, int> repository,
            ServiceManager ServiceManager,
            AttachmentManager attachmentManager,
            IServiceValueManager serviceValueManager,
            ISubServiceManager subServiceManager,
            IServiceValueForOfferManager serviceValueForOfferManager,
            IMapper mapper) :
            base(repository)
        {
            _serviceManager = ServiceManager;
            _attachmentManager = attachmentManager;
            _serviceValueManager = serviceValueManager;
            _subServiceManager = subServiceManager;
            _serviceValueForOfferManager = serviceValueForOfferManager;
            _mapper = mapper;
        }
        [AbpAuthorize(PermissionNames.Service_FullControl)]
        public override async Task<ServiceDetailsDto> CreateAsync(CreateServiceDto input)
        {
            if (await _serviceManager.CheckServiceIfExict(input.Translations))
                throw new UserFriendlyException(404, Exceptions.ObjectIsAlreadyExist, Tokens.Service);
            Service service = ObjectMapper.Map<Service>(input);
            service.Active = true;
            var serviceId = await Repository.InsertAndGetIdAsync(service);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            await _attachmentManager.CheckAndUpdateRefIdAsync(
                 input.AttachmentId, AttachmentRefType.Service, serviceId);
            return ObjectMapper.Map<ServiceDetailsDto>(service);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ServiceDetailsDto> GetAsync(EntityDto<int> input)
        {
            var service = await _serviceManager.GetEntityByIdAsync(input.Id);
            var attachment = await _attachmentManager.GetElementByRefAsync(service.Id, AttachmentRefType.Service);

            if (attachment is not null)
            {
                service.Attachment = new LiteAttachmentDto
                {
                    Id = attachment.Id,
                    Url = _attachmentManager.GetUrl(attachment),
                    LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment)
                };
            }
            return service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteServiceDto>> GetAllAsync(PagedServiceResultRequestDto input)
        {

            var result = await base.GetAllAsync(input);
            var attachments = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.Service);
            var attachmentsForSubServices = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.SubService);
            var attachmentsForTools = await _attachmentManager.GetByRefTypeAsync(AttachmentRefType.Tool);
            foreach (var item in result.Items)
            {
                var attachment = attachments.Where(x => x.RefId == item.Id).FirstOrDefault();
                if (attachment is not null)
                {
                    item.Attachment = new LiteAttachmentDto
                    {
                        Id = attachment.Id,
                        Url = _attachmentManager.GetUrl(attachment),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachment)
                    };
                }
                foreach (var subService in item.SubServices)
                {
                    var attachmentForSub = attachmentsForSubServices.Where(x => x.RefId == subService.Id).FirstOrDefault();
                    if (attachmentForSub is not null)
                    {
                        subService.Attachment = new LiteAttachmentDto
                        {
                            Id = attachmentForSub.Id,
                            Url = _attachmentManager.GetUrl(attachmentForSub),
                            LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachmentForSub)
                        };
                    }
                    foreach (var tool in subService.Tools)
                    {

                        var attachmentForTool = attachmentsForTools.Where(x => x.RefId == tool.Id).FirstOrDefault();
                        if (attachmentForTool is not null)
                        {
                            tool.Attachment = new LiteAttachmentDto
                            {
                                Id = attachmentForTool.Id,
                                Url = _attachmentManager.GetUrl(attachmentForTool),
                                LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(attachmentForTool)
                            };
                        }

                    }
                }

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Service_FullControl)]
        public override async Task<ServiceDetailsDto> UpdateAsync(UpdateServiceDto input)
        {
            var service = await _serviceManager.GetFullEntityByIdAsync(input.Id);
            await _serviceManager.HardDeleteForEntityTranslation(service.Translations.ToList());
            MapToEntity(input, service);
            service.Active = true;
            var oldAttachment = await _attachmentManager.GetElementByRefAsync(input.Id, AttachmentRefType.Service);
            if (oldAttachment is not null)
            {
                await _attachmentManager.DeleteRefIdAsync(oldAttachment);
            }
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, AttachmentRefType.Service, input.Id);
            service.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(service);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(service);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Service_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var service = await _serviceManager.GetFullEntityByIdAsync(input.Id);
            if (service is null)
                throw new UserFriendlyException(404, Exceptions.ObjectWasNotFound, Tokens.Service);

            if (await _serviceValueManager.CheckIfServiceBelongsToRequest(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);

            if (await _serviceValueManager.CheckIfServiceBelongsToCompanyOrCompanyBranch(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);

            if (await _serviceValueForOfferManager.CheckIfServiceBelongsToOffer(input.Id))
                throw new UserFriendlyException(Exceptions.ObjectCantBeDelete, Tokens.Service);


            await _subServiceManager.DeleteSubServiceForServiceBySerivceId(service.Id);
            await _serviceManager.HardDeleteForEntityTranslation(service.Translations.ToList());
            await Repository.DeleteAsync(service);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Service> CreateFilteredQuery(PagedServiceResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.SubServices).ThenInclude(x => x.Translations).AsNoTracking();
            data = data.Include(x => x.SubServices).ThenInclude(x => x.Tools).ThenInclude(x => x.Translations).AsNoTracking();
            if (!input.KeyWord.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.KeyWord)).Any()).AsNoTracking();
            if (input.SubServiceId.HasValue)
                data = data.Where(x => x.SubServices.Select(x => x.Id).Contains(input.SubServiceId.Value)).AsNoTracking();
            if (input.IsForStorage.HasValue)
                data = data.Where(x => x.IsForStorage == input.IsForStorage).AsNoTracking();
            if (input.IsForTruck.HasValue)
                data = data.Where(x => x.IsForTruck == input.IsForTruck).AsNoTracking();
            if (input.Active.HasValue)
                data = data.Where(x => x.Active == input.Active.Value).AsNoTracking();
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Service> ApplySorting(IQueryable<Service> query, PagedServiceResultRequestDto input)
        {
            return query.OrderBy(x => x.Id);
        }
        [HttpPut]
        [AbpAuthorize(PermissionNames.Service_FullControl)]
        public async Task<OutPutBooleanStatuesDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            var service = await Repository.GetAsync(input.Id);
            service.Active = input.IsActive;
            await Repository.UpdateAsync(service);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new OutPutBooleanStatuesDto
            {
                BooleanStatues = true
            };
        }
    }
}
