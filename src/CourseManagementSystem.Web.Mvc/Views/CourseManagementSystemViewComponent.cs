using Abp.AspNetCore.Mvc.ViewComponents;

namespace CourseManagementSystem.Web.Views
{
    public abstract class CourseManagementSystemViewComponent : AbpViewComponent
    {
        protected CourseManagementSystemViewComponent()
        {
            LocalizationSourceName = CourseManagementSystemConsts.LocalizationSourceName;
        }
    }
}
