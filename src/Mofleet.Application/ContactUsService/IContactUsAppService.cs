
using Mofleet.ContactUsService.Dto;
using Mofleet.CrudAppServiceBase;

namespace Mofleet.ContactUsService
{
    public interface IContactUsAppService : IMofleetAsyncCrudAppService<ContactUsDetailsDto, int, ContactUsDto, PagedContactUsResultRequestDto,
        CreateContactUsDto, UpdateContactUsDto>
    {
        // Task<ContactUsDetailsDto> SwitchActivationAsync(ContactUsSwitchActivationDto input);
    }
}
