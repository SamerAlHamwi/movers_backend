using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization;
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Cities.Dto;
using Mofleet.Domain.Codes;
using Mofleet.Domain.Codes.Dto;
using Mofleet.Domain.Partners;
using Mofleet.Domain.Partners.Dto;
using Mofleet.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mofleet.Enums.Enum;

namespace Mofleet.Partners
{

    /// <summary>
    /// 
    /// </summary>
    public class PartnerAppService : MofleetAsyncCrudAppService<Partner, PartnerDetailsDto, int, LitePartnerDto,
        PagedPartnerResultRequestDto, CreatePartnerDto, UpdatePartnerDto>,
        IPartnerAppService
    {
        private readonly IPartnerManager _partnerManager;
        private readonly ICodeManager _codeManager;
        private readonly ICityManager _cityManager;
        private readonly IMapper _mapper;
        private readonly IRepository<Partner, int> _partnerRepository;

        public PartnerAppService(IRepository<Partner, int> repository, IPartnerManager partnerManager, ICodeManager codeManager, ICityManager cityManager, IMapper mapper) : base(repository)
        {
            _partnerManager = partnerManager;
            _codeManager = codeManager;
            _cityManager = cityManager;
            _mapper = mapper;
            _partnerRepository = repository;
        }


        public override async Task<PartnerDetailsDto> GetAsync(EntityDto<int> input)
        {
            try
            {
                var partner = await _partnerManager.GetFullEntityByIdAsync(input.Id);
                var partnerDetailsDto = _mapper.Map<PartnerDetailsDto>(partner);

                foreach (Code code in partner.Codes.ToList())
                {
                    var phoneNumberArray = code.PhonesNumbers.Split(',').ToList();
                    var codeDto = partnerDetailsDto.Codes.Where(x => x.RSMCode == code.RSMCode).FirstOrDefault();
                    codeDto.PhonesNumbers = phoneNumberArray;
                }

                return partnerDetailsDto;
            }
            catch (Exception ex) { throw; }
        }


        public override async Task<PagedResultDto<LitePartnerDto>> GetAllAsync(PagedPartnerResultRequestDto input)
        {
            try
            {
                var result = await base.GetAllAsync(input);
                return result;
            }
            catch (Exception ex) { throw; }
        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public override async Task<PartnerDetailsDto> CreateAsync(CreatePartnerDto input)
        {
            CheckCreatePermission();
            if (await _partnerManager.CheckIfPartnerExist(input.PartnerPhoneNumber))
                throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Partner));

            var partner = ObjectMapper.Map<Partner>(input);

            partner.IsActive = true;

            var cities = new List<City>();
            if (input.CitiesIds.Count > 0 && input.CitiesIds is not null)
                cities = await _cityManager.CheckAndGetCitiesById(input.CitiesIds);
            partner.CitiesPartner = cities;
            await _partnerRepository.InsertAsync(partner);
            return MapToEntityDto(partner);
        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public override async Task<PartnerDetailsDto> UpdateAsync(UpdatePartnerDto input)
        {
            CheckUpdatePermission();

            var partner = await _partnerManager.GetEntityByIdAsync(input.Id);
            if (partner.PartnerPhoneNumber != input.PartnerPhoneNumber)
            {
                if (await _partnerManager.CheckIfPartnerExistForUpdate(input.PartnerPhoneNumber, partner.Id))
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Partner));
            }
            if (partner is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Partner));
            var activeStatus = partner.IsActive;
            MapToEntity(input, partner);

            if (input.CitiesIds.Count > 0)
                await _partnerManager.UpdateCitiesForPartnerAsync(input.CitiesIds, partner);
            partner.LastModificationTime = DateTime.UtcNow;
            partner.IsActive = activeStatus;
            await _partnerRepository.UpdateAsync(partner);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return MapToEntityDto(partner);

        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public async Task AddNewCodeToPatner(CreateCodeInputDto input)
        {

            if (await _codeManager.CheckIfPartnerCodeExist(input.createCode.RSMCode))
                throw new UserFriendlyException(string.Format(Exceptions.TheCodeIsAlreadyUsed, Tokens.Partner));
            var code = _mapper.Map<Code>(input.createCode);
            var phonenumbers = input.createCode.PhoneNumbers;
            var phonesNumbers = string.Join(",", phonenumbers.ToArray());
            code.PhonesNumbers = phonesNumbers;
            code.IsActive = true;
            code.PartnerId = input.Id;
            await _codeManager.InsertNewCodeToPartner(code);
        }

        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public async Task DeleteCodeFromPatner(int Id)
        {
            var code = await _codeManager.GetCodeById(Id);

            await _codeManager.SoftDeleteCode(code);

        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public async Task AddNumberToCode([FromBody] AddNumberOfCode input)
        {
            var code = await _codeManager.GetCodeById(input.Id);
            if (!code.PhonesNumbers.IsNullOrWhiteSpace())
            {
                var phoneNumberArray = code.PhonesNumbers.Split(',').ToList();
                if (phoneNumberArray.Contains(input.PhoneNumber))
                    throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.PhoneNumber));
                phoneNumberArray.Add(input.PhoneNumber);
                var phonesNumbers = string.Join(",", phoneNumberArray.ToArray());
                code.PhonesNumbers = phonesNumbers;
            }
            else code.PhonesNumbers = input.PhoneNumber;
            await UnitOfWorkManager.Current.SaveChangesAsync();

        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public async Task DeleteNumberFromCode([FromBody] DeleteNumberOfCode input)
        {
            var code = await _codeManager.GetCodeById(input.Id);
            var phoneNumberArray = code.PhonesNumbers.Split(',').ToList();
            if (!phoneNumberArray.Contains(input.PhoneNumber))
                throw new UserFriendlyException(string.Format(Exceptions.PhoneNumberNotFoundInCode, Tokens.Code));
            phoneNumberArray.Remove(input.PhoneNumber);
            var phonesNumbers = string.Join(",", phoneNumberArray.ToArray());
            code.PhonesNumbers = phonesNumbers;
            await UnitOfWorkManager.Current.SaveChangesAsync();

        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Partner> ApplySorting(IQueryable<Partner> query, PagedPartnerResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime.Date);
        }

        protected override IQueryable<Partner> CreateFilteredQuery(PagedPartnerResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(x => x.CitiesPartner).ThenInclude(x => x.Translations);

            if (!input.Keyword.IsNullOrEmpty())
            {

                var keyword = input.Keyword.ToLower();
                data = data.Where(x => x.CompanyName.Contains(keyword)
                || x.Email.ToLower().Contains(keyword)
                || x.FirstName.ToLower().Contains(keyword)
                || x.LastName.ToLower().Contains(keyword)
                || x.PartnerPhoneNumber.ToLower().Contains(keyword)
                || x.CitiesPartner.Any(x => x.Translations.Any(x => x.Name.ToLower().Contains(keyword)))
                );

            }
            if (input.IsActive.HasValue)
                data = data.Where(x => x.IsActive == input.IsActive.Value);
            return data;
        }

        [HttpPost]
        [AbpAuthorize]
        public async Task<OutPutBooleanStatuesWithPriceDto> CheckIfCodeExictWithThisUserAsync(double price, string code)
        {
            if (!await _codeManager.CheckIfPartnerCodeExist(code))
                return new OutPutBooleanStatuesWithPriceDto() { BooleanStatues = false };
            await _codeManager.CheckIfThisCodeHaveThisNumber(AbpSession.UserId.Value, code);
            var codetoDiscount = await _codeManager.GetCodeByRSMCode(code);
            double finalPrice;
            if (codetoDiscount.CodeType == CodeType.DiscountPercentageValue)
                finalPrice = price - ((price * (double)codetoDiscount.DiscountPercentage) / 100);
            else
                finalPrice = price - (double)codetoDiscount.DiscountPercentage;
            return new OutPutBooleanStatuesWithPriceDto
            {
                BooleanStatues = true,
                FinalPrice = finalPrice
            };
        }
        [AbpAuthorize(PermissionNames.Partner_FullControl)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            var partner = await _partnerManager.GetFullEntityByIdAsync(input.Id);
            if (partner is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Partner));
            partner.CitiesPartner.Clear();
            partner.Codes.Clear();
            await _partnerRepository.DeleteAsync(input.Id);
        }


    }
}
