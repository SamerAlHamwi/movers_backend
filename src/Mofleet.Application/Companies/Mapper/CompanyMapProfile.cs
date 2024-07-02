using AutoMapper;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.Reviews;
using Mofleet.Domain.Reviews.Dto;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.TimeWorks.Dtos;


namespace Mofleet.Companies.Mapper
{
    public class CompanyMapProfile : Profile
    {
        public CompanyMapProfile()
        {
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<CreateCompanyDto, CompanyDto>();
            //CreateMap<CompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>();
            //CreateMap<Company, CompanyDto>();
            CreateMap<CompanyContactDto, CompanyContact>();
            CreateMap<CompanyContact, CompanyContactDto>();
            CreateMap<CompanyContact, CompanyContactDetailsDto>();
            CreateMap<Review, ReviewDetailsDto>();
            CreateMap<TimeOfWorkDto, TimeWork>();
            CreateMap<TimeWork, TimeOfWorkDto>();
            CreateMap<UpdateCompanyDto, CreateCompanyDto>()
                .ForMember(dest => dest.OldCompanyId, opt => opt.MapFrom(src => src.Id));


            CreateMap<CompanyDetailsDto, UpdateCompanyDto>();

            CreateMap<CompanyContactDetailsDto, CompanyContactDto>();

        }
    }
}
