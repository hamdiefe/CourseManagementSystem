using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CourseManagementSystem.Controllers
{
    public abstract class CourseManagementSystemControllerBase: AbpController
    {
        protected CourseManagementSystemControllerBase()
        {
            LocalizationSourceName = CourseManagementSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
