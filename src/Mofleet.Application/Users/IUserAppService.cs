using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mofleet.Roles.Dto;
using Mofleet.Users.Dto;
using System.Threading.Tasks;

namespace Mofleet.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UpdateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<string> GetCurrentFcmTokenAsync();

        Task SetCurrentFcmTokenAsync(string input);
        Task<bool> AskForHelp(string message);
    }
}
