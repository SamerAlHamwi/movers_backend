using AutoMapper;
using Mofleet.Domain.Terms;
using Mofleet.TermService.Dto;

namespace Mofleet.TermService.Mapper
{
    public class TermMapProfile : Profile
    {
        public TermMapProfile()
        {
            CreateMap<CreateTermDto, Term>();
        }
    }
}
