using Abp.Application.Services.Dto;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.Reviews.Dto;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.CompanyBranches.Dto
{
    public class CompanyBranchDto : EntityDto<int>
    {
        public string Bio { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<CompanyBranchTranslationDto> Translations { get; set; }
        public CompanyContactDto CompanyContact { get; set; }
        public GeneralRatingDto GeneralRating { get; set; }
        public List<ReviewDetailsDto> Reviews { get; set; }
        public int NumberOfTransfers { get; set; }
        public ServiceType ServiceType { get; set; }


    }

}
