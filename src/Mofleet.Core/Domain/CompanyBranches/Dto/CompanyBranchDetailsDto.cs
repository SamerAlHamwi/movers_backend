using Abp.Application.Services.Dto;
using Mofleet.Cities.Dto;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.Regions.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.TimeWorks.Dtos;
using Mofleet.Domain.UserVerficationCodes;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.CompanyBranches.Dto
{
    public class CompanyBranchDetailsDto : EntityDto
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public List<CompanyBranchTranslationDto> Translations { get; set; }
        public LiteRegionDto Region { get; set; }
        public CompanyContactDetailsDto CompanyContact { get; set; }
        public CompanyDto Company { get; set; }
        public LiteUserDto User { get; set; }
        public bool IsActive { get; set; }
        public List<LiteCityDto> AvailableCities { get; set; }
        public List<ServiceDetailsDto> Services { get; set; }
        public int NumberOfTransfers { get; set; }
        public ServiceType ServiceType { get; set; }
        public GeneralRatingDto GeneralRating { get; set; }
        public int NumberOfPaidPoints { get; set; }
        public int NumberOfGiftedPoints { get; set; }
        public int TotalPoints { get; set; }
        public bool AcceptRequests { get; set; }
        public bool AcceptPossibleRequests { get; set; }
        public bool IsFeature { get; set; }
        public DateTime? StartFeatureSubscribtionDate { get; set; }
        public DateTime? EndFeatureSubscribtionDate { get; set; }
        public List<TimeOfWorkDto> TimeOfWorks { get; set; } = new List<TimeOfWorkDto>();
        public CompanyBranchStatues Statues { get; set; }
        public string ReasonRefuse { get; set; }

    }
}
