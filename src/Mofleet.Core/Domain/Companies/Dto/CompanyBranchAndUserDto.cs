using Mofleet.Domain.Regions.Dto;
using Mofleet.Domain.UserVerficationCodes;

namespace Mofleet.Domain.Companies.Dto
{
    public class CompanyBranchAndUserDto
    {
        public LiteUserDto? User { get; set; }
        public string Address { get; set; }
        public LiteRegionDto Region { get; set; }
        public CompanyContactDetailsDto CompanyContact { get; set; }
    }
}
