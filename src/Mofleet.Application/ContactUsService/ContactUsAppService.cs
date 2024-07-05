using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.ContactUsService.Dto;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.ContactUses;
using Mofleet.Localization.SourceFiles;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mofleet.ContactUsService
{
    /// <summary>
    /// 
    /// </summary>
    [ApiExplorerSettings(GroupName = "ch4")]
    public class ContactUsAppService : MofleetAsyncCrudAppService<ContactUs, ContactUsDetailsDto, int, ContactUsDto,
        PagedContactUsResultRequestDto, CreateContactUsDto, UpdateContactUsDto>,
        IContactUsAppService
    {
        private readonly IContactUsManager _ContactUsManager;
        private readonly IMapper _mapper;
        private readonly IAttachmentManager _attachmentManager;
        /// <summary>
        /// Countries AppService
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="ContactUsManager"></param>
        /// <param name="cityManager"></param>
        public ContactUsAppService(IRepository<ContactUs> repository, IContactUsManager ContactUsManager, IMapper mapper, IAttachmentManager attachmentManager)
         : base(repository)
        {
            _ContactUsManager = ContactUsManager;
            _mapper = mapper;
            _attachmentManager = attachmentManager;
        }
        /// <summary>
        /// Get ContactUs Details ById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ContactUsDetailsDto> GetAsync(EntityDto<int> input)
        {
            var ContactUs = await _ContactUsManager.GetContactUs();
            var ContactUsDto = ObjectMapper.Map<ContactUsDetailsDto>(ContactUs);
            var ContactUsAttachment = (await _attachmentManager.GetByRefTypeAsync(Enums.Enum.AttachmentRefType.ContactUs)).LastOrDefault();
            if (ContactUsAttachment is not null && ContactUs is not null)
            {
                ContactUsDto.Attachment = new LiteAttachmentDto
                {
                    Id = ContactUsAttachment.Id,
                    Url = _attachmentManager.GetUrl(ContactUsAttachment),
                    LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(ContactUsAttachment),
                };
            }
            return ContactUsDto;

        }
        /// <summary>
        /// Get All Countries Details 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<PagedResultDto<ContactUsDto>> GetAllAsync(PagedContactUsResultRequestDto input)
        {
            var countries = await base.GetAllAsync(input);
            foreach (var item in countries.Items)
            {
                var ContactUsAttachment = await _attachmentManager.GetByRefAsync(item.Id, Enums.Enum.AttachmentRefType.ContactUs);
                if (ContactUsAttachment.Any())
                {
                    item.Attachment = new LiteAttachmentDto
                    {
                        Id = ContactUsAttachment.LastOrDefault().Id,
                        Url = _attachmentManager.GetUrl(ContactUsAttachment.LastOrDefault()),
                        LowResolutionPhotoUrl = _attachmentManager.GetLowResolutionPhotoUrl(ContactUsAttachment.LastOrDefault()),
                    };
                }


            }
            return countries;
        }
        /// <summary>
        /// Add New ContactUs  Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<ContactUsDetailsDto> CreateAsync(CreateContactUsDto input)
        {
            CheckCreatePermission();
            if (await _ContactUsManager.IsAnyCotactUSFound())
            {
                throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Contact));
            }
            var ContactUs = ObjectMapper.Map<ContactUs>(input);

            await Repository.InsertAsync(ContactUs);
            await UnitOfWorkManager.Current.SaveChangesAsync();




            if (input.AttachmentId.HasValue)
                await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.ContactUs, ContactUs.Id);
            return MapToEntityDto(ContactUs);
        }
        /// <summary>
        /// Update ContactUs Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public override async Task<ContactUsDetailsDto> UpdateAsync(UpdateContactUsDto input)
        {
            CheckUpdatePermission();
            var ContactUs = await _ContactUsManager.GetContactUs();
            ContactUs.Translations.Clear();
            //  await _ContactUsManager.CheckContactUsExist(input.Name, input.Id);
            MapToEntity(input, ContactUs);
            ContactUs.LastModificationTime = DateTime.Now;
            await Repository.UpdateAsync(ContactUs);
            if (input.AttachmentId.HasValue)
            {
                var attach = await _attachmentManager.GetElementByRefAsync(input.Id, Enums.Enum.AttachmentRefType.ContactUs);
                if (attach is not null)
                {
                    if (attach.Id != input.AttachmentId.Value)
                    {
                        await _attachmentManager.DeleteRefIdAsync(attach);
                        await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.ContactUs, input.Id);
                    }
                }
                else
                {
                    await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.ContactUs, input.Id);
                }

            }

            var ContactUsDto = MapToEntityDto(ContactUs);
            return ContactUsDto;
        }
        /// <summary>
        /// Delete ContactUs Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [AbpAuthorize]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();


            var ContactUs = await _ContactUsManager.GetContactUs();
            ContactUs.Translations.Clear();

            ContactUs.IsDeleted = true;
            ContactUs.DeletionTime = DateTime.Now;
            await Repository.UpdateAsync(ContactUs);


            //await _ContactUsManager.DeactviateAllContactUsCity(input.Id);
        }

        /// <summary>
        /// Filter For A Group of Countries
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<ContactUs> CreateFilteredQuery(PagedContactUsResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Where(x => !x.IsDeleted);
            data = data.Include(x => x.Translations);
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Name.Contains(input.Keyword)).Any());
            return data;

        }
        /// <summary>
        /// Sorting Filtered Countries
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<ContactUs> ApplySorting(IQueryable<ContactUs> query, PagedContactUsResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }

    }
}
