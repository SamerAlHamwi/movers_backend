using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.Authorization.Users;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Trucks;
using Mofleet.Domain.Trucks.Dto;
using Mofleet.Localization.SourceFiles;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Mofleet.Enums.Enum;

namespace ClinicSystem.Trucks
{
    public class TruckAppService : MofleetAsyncCrudAppService<Truck, TruckDetailsDto, int, LiteTruckDto,
        PagedTruckResultRequestDto, CreateTruckDto, UpdateTruckDto>,
        ITruckAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly ITruckManager _TruckManager;
        private readonly ICompanyManager _CompanyManager;
        private readonly IRepository<Truck> _TruckRepository;



        public TruckAppService(IRepository<Truck> repository,
            UserRegistrationManager userRegistrationManager,
            TruckManager TruckManager,
            ICompanyManager CompanyManager,
          
            IRepository<Truck> TruckRepository)
            : base(repository)
        {
            _userRegistrationManager = userRegistrationManager;
            _TruckManager = TruckManager;
            _CompanyManager = CompanyManager;
            _TruckRepository = TruckRepository;
        }
        /// <summary>
        /// Get Truck Details ById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<TruckDetailsDto> GetAsync(EntityDto<int> input)
        {
            var Truck = await _TruckManager.GetEntityByIdAsync(input.Id);
            if (Truck is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
            return MapToEntityDto(Truck);
        }
        /// <summary>
        /// Get All Trucks Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteTruckDto>> GetAllAsync(PagedTruckResultRequestDto input)
        {

            return await base.GetAllAsync(input);
        }
        /// <summary>
        /// Add New Truck Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<TruckDetailsDto> CreateAsync(CreateTruckDto input)
        {
            var Truck = ObjectMapper.Map<Truck>(input);
            await _CompanyManager.GetCompanyEntityById(input.CompanyId);
            Truck.CreationTime = DateTime.UtcNow;
            await _TruckRepository.InsertAsync(Truck);
            return MapToEntityDto(Truck);
        }
        /// <summary>
        /// Update Truck Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      /*  public override async Task<TruckDetailsDto> UpdateAsync(UpdateTruckDto input)
        {
            CheckUpdatePermission();
            var Truck = await _TruckManager.GetEntityByIdAsync(input.Id);
            if (Truck is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Truck));
            await _TruckTranslationRepository.GetAll().Where(x => x.CoreId == input.Id).
                          ExecuteUpdateAsync(se => se.SetProperty(x => x.IsDeleted, true));
            MapToEntity(input, Truck);
            Truck.LastModificationTime = DateTime.UtcNow;
            await _TruckRepository.UpdateAsync(Truck);
            return MapToEntityDto(Truck);
        }
*/
        /// <summary>
        /// Delete Truck Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            //CheckDeletePermission();
            var Truck = await _TruckManager.GetEntityByIdAsync(input.Id);
            if (Truck is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
          
            await _TruckRepository.DeleteAsync(Truck);
        }

        /// <summary>
        /// Filter For A Group Of Trucks
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Truck> CreateFilteredQuery(PagedTruckResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Company).ThenInclude(x => x.Translations).AsNoTracking();
           
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Number.Contains(input.Keyword));
            if (input.CompanyId.HasValue)
                data = data.Where(x => x.CompanyId == input.CompanyId);
            
            return data;
        }

        /// <summary>
        /// Sorting Filtered Trucks
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Truck> ApplySorting(IQueryable<Truck> query, PagedTruckResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime);
        }

       
        
    }
}

