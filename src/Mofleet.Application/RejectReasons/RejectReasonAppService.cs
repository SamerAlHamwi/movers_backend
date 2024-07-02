using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.RejectReasons.Dto;
using Mofleet.Localization.SourceFiles;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mofleet.RejectReasons
{
    public class RejectReasonAppService : MofleetAsyncCrudAppService<RejectReason, RejectReasonDetailsDto, int, LiteRejectReasonDto, PagedRejectReasonResultRequestDto, CreateRejectReasonDto, UpdateRejectReasonDto>, IRejectReasonAppService

    {
        private readonly IRepository<RejectReasonTranslation> _rejectReasonTranslationRepository;
        private readonly IRejectReasonManager _rejectReasonManager;

        public RejectReasonAppService(IRepository<RejectReason, int> repository,
            IRepository<RejectReasonTranslation> rejectReasonTranslationRepository,
            IRejectReasonManager rejectReasonManager) : base(repository)
        {
            _rejectReasonTranslationRepository = rejectReasonTranslationRepository;
            _rejectReasonManager = rejectReasonManager;
        }




        /// <summary>
        /// Get Reject Reason ByID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<RejectReasonDetailsDto> GetAsync(EntityDto<int> input)
        {
            var rejectReason = await _rejectReasonManager.GetEntityByIdAsync(input.Id);

            return MapToEntityDto(rejectReason);




        }
        [AbpAuthorize(PermissionNames.RejectReason_FullControl)]
        public override async Task<RejectReasonDetailsDto> CreateAsync(CreateRejectReasonDto input)
        {
            CheckCreatePermission();

            var rejectReason = ObjectMapper.Map<RejectReason>(input);
            rejectReason.CreationTime = DateTime.UtcNow;
            rejectReason.IsActive = true;


            Repository.InsertAndGetId(rejectReason);
            return MapToEntityDto(rejectReason);

        }
        [AbpAuthorize(PermissionNames.RejectReason_FullControl)]
        public override async Task<RejectReasonDetailsDto> UpdateAsync(UpdateRejectReasonDto input)
        {
            try
            {
                CheckUpdatePermission();
                var rejectReason = await _rejectReasonManager.GetEntityByIdAsync(input.Id);
                if (rejectReason is null)
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.RejectReason));
                rejectReason.Translations.Clear();
                bool isActive = rejectReason.IsActive;


                MapToEntity(input, rejectReason);

                rejectReason.IsActive = isActive;
                await Repository.UpdateAsync(rejectReason);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                return MapToEntityDto(rejectReason);
            }
            catch (Exception ex) { throw; }

        }
        public override async Task<PagedResultDto<LiteRejectReasonDto>> GetAllAsync(PagedRejectReasonResultRequestDto input)
        {
            try
            {
                var result = await base.GetAllAsync(input);

                return result;
            }
            catch (Exception ex) { throw; }
        }
        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<RejectReason> ApplySorting(IQueryable<RejectReason> query, PagedRejectReasonResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }
        protected override IQueryable<RejectReason> CreateFilteredQuery(PagedRejectReasonResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Translations);


            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Translations.Where(x => x.Description.Contains(input.Keyword)).Any());
            if (input.PossibilityPotentialClient.HasValue)
                data = data.Where(x => x.PossibilityPotentialClient == input.PossibilityPotentialClient.Value);
            return data;
        }


        /// <summary>
        /// Switch Activation of A RejectReason
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        [AbpAuthorize(PermissionNames.RejectReason_FullControl)]
        public async Task<RejectReasonDetailsDto> SwitchActivationAsync(SwitchActivationInputDto input)
        {
            CheckUpdatePermission();
            var rejectReason = await _rejectReasonManager.GetEntityByIdAsync(input.Id);
            rejectReason.IsActive = !rejectReason.IsActive;
            rejectReason.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(rejectReason);
            return MapToEntityDto(rejectReason);
        }


        /// <summary>
        /// Delete A RejectReason
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.RejectReason_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var rejectReason = await _rejectReasonManager.GetEntityByIdAsync(input.Id);
            if (rejectReason is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.RejectReason));

            foreach (var translation in rejectReason.Translations.ToList())
            {
                await _rejectReasonTranslationRepository.DeleteAsync(translation);
                rejectReason.Translations.Remove(translation);
            }

            await Repository.DeleteAsync(input.Id);
        }





    }
}
