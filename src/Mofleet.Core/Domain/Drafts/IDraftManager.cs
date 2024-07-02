﻿using Abp.Domain.Services;
using Mofleet.Domain.Drafts.Dtos;
using Mofleet.Domain.RequestForQuotationContacts.Dto;
using Mofleet.Domain.RequestForQuotations.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.Drafts
{
    public interface IDraftManager : IDomainService
    {
        Task<DraftDetailsDto> GetEntityDtoByIdAsync(int id);
        Task DeleteAllOldDrafts();
        Task<bool> CheckIfUserCanAddNewDraft(long userId);
        Task UpdateAttacmentForDraft(int draftId, List<AttributeChoiceAndAttachmentDto> AttributeChoiceAndAttachments);
        Task UpdateContcatForDraft(List<CreateRequestForQuotationContactDto> draftContacts, Draft draft);
        Task<Draft> GetEntityById(int id);
        Task HardDeleteDraftById(int draftId);





    }
}
