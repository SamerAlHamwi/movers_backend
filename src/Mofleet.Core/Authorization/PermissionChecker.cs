using Abp.Authorization;
using Mofleet.Authorization.Roles;
using Mofleet.Authorization.Users;

namespace Mofleet.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
