using Abp.Application.Services.Dto;

namespace Mofleet.ContactUsService.Dto
{
    public class UpdateContactUsDto : CreateContactUsDto, IEntityDto
    {

        public int Id { get; set; }



    }
}
