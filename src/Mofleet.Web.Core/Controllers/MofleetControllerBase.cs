using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Mofleet.Controllers
{
    public abstract class MofleetControllerBase : AbpController
    {
        protected MofleetControllerBase()
        {
            LocalizationSourceName = MofleetConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
