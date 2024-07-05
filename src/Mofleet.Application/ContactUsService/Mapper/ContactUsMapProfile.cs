using AutoMapper;

using Mofleet.ContactUsService.Dto;
using Mofleet.Domain.ContactUses;

namespace Mofleet.ContactUsService.Mapper
{
    public class ContactUsMapProfile : Profile
    {
        public ContactUsMapProfile()
        {
            CreateMap<CreateContactUsDto, ContactUs>();
            CreateMap<CreateContactUsDto, ContactUsDto>();
            //  // CreateMap<ContactUsDto, ContactUs>();
            ////   CreateMap<ContactUs, ContactUsDto>();
            //   //CreateMap<ContactUs, ContactUsDetailsDto>();
            CreateMap<ContactUs, ContactUsListDto>();
            CreateMap<ContactUs, UpdateContactUsDto>();
            CreateMap<UpdateContactUsDto, ContactUs>();

        }



    }
}
