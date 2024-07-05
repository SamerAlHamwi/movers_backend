using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.UI;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.AttributeAndAttachmentsForDrafts;
using Mofleet.Domain.AttributeForSourceTypeValuesForDrafts;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Drafts;
using Mofleet.Domain.Drafts.Dtos;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValuesForDrafts;
using Mofleet.Domain.SourceTypes;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Drafts
{
    public class DraftAppService : MofleetAsyncCrudAppService<Draft, DraftDetailsDto, int, LiteDraftDto, PagedDraftResultRequestDto,
        CreateDraftDto, UpdateDraftDto>, IDraftAppService
    {
        private readonly IAttachmentManager _attachmentManager;
        private readonly IMapper _mapper;
        private readonly UserManager _userManager;
        private readonly ServiceManager _serviceManager;
        private readonly CityManager _cityManager;
        private readonly ISourceTypeManager _sourceTypeManager;
        private readonly IRepository<AttributeAndAttachmentsForDraft> _attributeAndAttachmentForDraftRepository;
        private readonly IDraftManager _draftManager;
        private readonly IServiceValuesForDraftManager _serviceValuesForDraftManager;
        private readonly IAttributeForSourceTypeValuesForDraftManager _attributeForSourceTypeValueForDraftManager;

        public DraftAppService(IRepository<Draft, int> repository,
            IAttachmentManager attachmentManager,
            IMapper mapper,
            UserManager userManager,
            ServiceManager serviceManager,
            CityManager cityManager,
            ISourceTypeManager sourceTypeManager,
            IRepository<AttributeAndAttachmentsForDraft> attributeAndAttachmentForDraftRepository,
            IDraftManager draftManager,
            IServiceValuesForDraftManager serviceValuesForDraftManager,
            IAttributeForSourceTypeValuesForDraftManager attributeForSourceTypeValueForDraftManager)
            : base(repository)
        {
            _attachmentManager = attachmentManager;
            _mapper = mapper;
            _userManager = userManager;
            _serviceManager = serviceManager;
            _cityManager = cityManager;
            _sourceTypeManager = sourceTypeManager;
            _attributeAndAttachmentForDraftRepository = attributeAndAttachmentForDraftRepository;
            _draftManager = draftManager;
            _serviceValuesForDraftManager = serviceValuesForDraftManager;
            _attributeForSourceTypeValueForDraftManager = attributeForSourceTypeValueForDraftManager;
        }
        [AbpAuthorize]
        public override async Task<DraftDetailsDto> CreateAsync(CreateDraftDto input)
        {
            try
            {
                if (!await _draftManager.CheckIfUserCanAddNewDraft(AbpSession.UserId.Value))
                    throw new UserFriendlyException(Exceptions.YouCannotAddNewDraft);
                var draft = ObjectMapper.Map<Draft>(input);
                draft.UserId = AbpSession.UserId.Value;
                draft.AttributeChoiceAndAttachments = null;
                var draftId = await Repository.InsertAndGetIdAsync(draft);
                List<AttributeAndAttachmentsForDraft> attributeAndAttachmentsForDraft = new List<AttributeAndAttachmentsForDraft>();

                foreach (var item in input.AttributeChoiceAndAttachments)
                {
                    List<Attachment> attachments = new List<Attachment>();
                    if (item.AttachmentIds is not null && item.AttachmentIds.Count > 0)
                    {
                        foreach (var attachmentId in item.AttachmentIds)
                        {
                            var attachment = await _attachmentManager.CheckAndUpdateRefIdAsync(
                                     attachmentId, AttachmentRefType.RequestForQuotation, draftId, true, false);

                            attachments.Add(attachment);

                        }
                        if (item.AttributeChoiceId != null)
                            attributeAndAttachmentsForDraft.Add(new AttributeAndAttachmentsForDraft()
                            {
                                AttributeChoiceId = item.AttributeChoiceId.Value,
                                DraftId = draftId,
                                Attachments = attachments
                            });
                    }
                    else
                        continue;
                }
                if (attributeAndAttachmentsForDraft is not null && attributeAndAttachmentsForDraft.Count > 0)
                    await _attributeAndAttachmentForDraftRepository.InsertRangeAsync(attributeAndAttachmentsForDraft);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return _mapper.Map<DraftDetailsDto>(draft);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public override async Task<DraftDetailsDto> UpdateAsync(UpdateDraftDto input)
        {
            try
            {

                var draft = await _draftManager.GetEntityById(input.Id);
                var oldAttributeSourceTypeValues = draft.AttributeForSourceTypeValues.ToList();
                var oldSeviceValues = draft.Services.ToList();
                var userId = draft.UserId;
                _mapper.Map<UpdateDraftDto, Draft>(input, draft);
                draft.UserId = userId;
                draft.AttributeChoiceAndAttachments = null;
                await _draftManager.UpdateAttacmentForDraft(draft.Id, input.AttributeChoiceAndAttachments);
                await _attributeForSourceTypeValueForDraftManager.HardDeleteEntity(oldAttributeSourceTypeValues);
                await _serviceValuesForDraftManager.HardDeleteServiceValues(oldSeviceValues);
                draft.Services = null;
                await Repository.UpdateAsync(draft);
                if (input.Services is not null)
                    await _serviceValuesForDraftManager.InsertServiceValuesForDraft(input.Services, draft.Id);
                UnitOfWorkManager.Current.SaveChanges();
                return _mapper.Map<DraftDetailsDto>(draft);

            }
            catch (Exception e) { throw; }
        }

        public override async Task<DraftDetailsDto> GetAsync(EntityDto<int> input)
        {
            try
            {
                return await _draftManager.GetEntityDtoByIdAsync(input.Id);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message + " " + ex.InnerException);
            }
        }
        public override async Task<PagedResultDto<LiteDraftDto>> GetAllAsync(PagedDraftResultRequestDto input)
        {
            try
            {
                var userType = _userManager.GetUserByIdAsync(AbpSession.UserId.Value).GetAwaiter().GetResult().Type;
                if ((userType != UserType.Admin && userType != UserType.CustomerService) && input.UserId.HasValue)
                    input.UserId = AbpSession.UserId.Value;
                return await base.GetAllAsync(input);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var draft = await Repository.GetAsync(input.Id);
            if (draft is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Draft));
            await Repository.DeleteAsync(input.Id);
        }

        protected override IQueryable<Draft> CreateFilteredQuery(PagedDraftResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.AttributeForSourceTypeValues).AsNoTracking();
            data = data.Include(x => x.Services).AsNoTracking();
            data = data.Include(x => x.RequestForQuotationContacts).AsNoTracking();
            data = data.Include(x => x.AttributeChoiceAndAttachments).ThenInclude(x => x.Attachments).AsNoTracking();
            data = data.Include(x => x.User).AsNoTracking();
            if (input.UserId.HasValue) data = data.Where(x => x.UserId == input.UserId.Value); else data = data.Where(x => x.UserId == AbpSession.UserId.Value);


            return data;
        }

    }
}
