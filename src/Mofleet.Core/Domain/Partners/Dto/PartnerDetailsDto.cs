using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using Mofleet.Domain.Codes.Dto;
using System.Collections.Generic;

namespace Mofleet.Domain.Partners.Dto
{
    public class PartnerDetailsDto : EntityDto
    {

        public string PartnerPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public List<CodeDto> Codes { get; set; }
        public List<LiteCityDto> CitiesPartner { get; set; }
    }
}
