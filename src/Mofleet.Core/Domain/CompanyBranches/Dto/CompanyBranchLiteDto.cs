﻿using Abp.Application.Services.Dto;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.Regions.Dto;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.UserVerficationCodes;
using System;
using System.Collections.Generic;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.CompanyBranches.Dto
{
    public class LiteCompanyBranchDto : EntityDto<int>
    {
        public CompanyDto Company { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public LiteRegionDto Region { get; set; }
        public CompanyContactDetailsDto CompanyContact { get; set; }
        public List<CompanyBranchTranslationDto> Translations { get; set; }
        public List<ServiceDto> Services { get; set; }
        public int NumberOfTransfers { get; set; }
        public CompanyBranchStatues Statues { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool IsThisCompanyProvideOffer { get; set; } = false;
        public bool AcceptRequests { get; set; }
        public bool AcceptPossibleRequests { get; set; }
        public bool IsFeature { get; set; }
        public DateTime? StartFeatureSubscribtionDate { get; set; }
        public DateTime? EndFeatureSubscribtionDate { get; set; }
        public double CompatibilityRate { get; set; }
        public double CommissionForBranchWithOutCompany { get; set; }
        public LiteUserDto User { get; set; }


    }
}
