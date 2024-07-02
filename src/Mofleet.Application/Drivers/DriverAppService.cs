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
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Countries;
using Mofleet.Domain.Drivers;
using Mofleet.Domain.Drivers.Dto;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Mofleet.Enums.Enum;

namespace ClinicSystem.Drivers
{
    public class DriverAppService : MofleetAsyncCrudAppService<Driver, DriverDetailsDto, int, LiteDriverDto,
        PagedDriverResultRequestDto, CreateDriverDto, UpdateDriverDto>,
        IDriversAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IDriverManager _DriverManager;
        private readonly ICompanyManager _CompanyManager;
        private readonly IRepository<Driver> _DriverRepository;



        public DriverAppService(IRepository<Driver> repository,
            UserRegistrationManager userRegistrationManager,
            DriverManager DriverManager,
            ICompanyManager CompanyManager,
          
            IRepository<Driver> DriverRepository)
            : base(repository)
        {
            _userRegistrationManager = userRegistrationManager;
            _DriverManager = DriverManager;
            _CompanyManager = CompanyManager;
            _DriverRepository = DriverRepository;
        }
        /// <summary>
        /// Get Driver Details ById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<DriverDetailsDto> GetAsync(EntityDto<int> input)
        {
            var Driver = await _DriverManager.GetEntityByIdAsync(input.Id);
            if (Driver is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
            return MapToEntityDto(Driver);
        }
        /// <summary>
        /// Get All Drivers Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public override async Task<PagedResultDto<LiteDriverDto>> GetAllAsync(PagedDriverResultRequestDto input)
        {

            return await base.GetAllAsync(input);
        }
        /// <summary>
        /// Add New Driver Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<DriverDetailsDto> CreateAsync(CreateDriverDto input)
        {
            var Driver = ObjectMapper.Map<Driver>(input);
            await _CompanyManager.GetCompanyEntityById(input.CompanyId);
            //User user = await _userRegistrationManager.RegisterAsyncForUserDriver(input.userDto.EmailAddress, input.userDto.PhoneNumber, input.userDto.DialCode, input.userDto.Password, UserType.CompanyUser);
            //Driver.User = user;
            Driver.CreationTime = DateTime.UtcNow;
            await _DriverRepository.InsertAsync(Driver);
            return MapToEntityDto(Driver);
        }
        /// <summary>
        /// Update Driver Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      /*  public override async Task<DriverDetailsDto> UpdateAsync(UpdateDriverDto input)
        {
            CheckUpdatePermission();
            var Driver = await _DriverManager.GetEntityByIdAsync(input.Id);
            if (Driver is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Driver));
            await _DriverTranslationRepository.GetAll().Where(x => x.CoreId == input.Id).
                          ExecuteUpdateAsync(se => se.SetProperty(x => x.IsDeleted, true));
            MapToEntity(input, Driver);
            Driver.LastModificationTime = DateTime.UtcNow;
            await _DriverRepository.UpdateAsync(Driver);
            return MapToEntityDto(Driver);
        }
*/
        /// <summary>
        /// Delete Driver Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var Driver = await _DriverManager.GetEntityByIdAsync(input.Id);
            if (Driver is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
          
            await _DriverRepository.DeleteAsync(Driver);
        }

        /// <summary>
        /// Filter For A Group Of Drivers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Driver> CreateFilteredQuery(PagedDriverResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.Company).AsNoTracking();
           
            if (!input.Keyword.IsNullOrEmpty())
                data = data.Where(x => x.Name.Contains(input.Keyword));
            if (input.CompanyId.HasValue)
                data = data.Where(x => x.CompanyId == input.CompanyId);
            
            return data;
        }

        /// <summary>
        /// Sorting Filtered Drivers
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Driver> ApplySorting(IQueryable<Driver> query, PagedDriverResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime);
        }

       
        
    }
}

