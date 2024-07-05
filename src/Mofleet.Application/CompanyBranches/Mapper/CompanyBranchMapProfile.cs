using AutoMapper;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.Reviews;
using Mofleet.Domain.Reviews.Dto;

namespace Mofleet.CompanyBranches
{
    public class CompanyBranchMapProfile : Profile
    {
        public CompanyBranchMapProfile()
        {

            CreateMap<UpdateCompanyBranchDto, CompanyBranch>();
            CreateMap<CompanyContactDto, CompanyContact>();
            CreateMap<CreateCompanyBranchDto, CompanyBranch>();
            CreateMap<CompanyContact, CompanyContactDetailsDto>();
            CreateMap<CompanyBranch, CompanyBranchAndUserDto>();
            CreateMap<CompanyBranchAndUserDto, CompanyBranch>();
            CreateMap<Review, ReviewDetailsDto>();
        }
    }
}
