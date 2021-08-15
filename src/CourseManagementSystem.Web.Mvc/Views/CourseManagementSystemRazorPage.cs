using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace CourseManagementSystem.Web.Views
{
    public abstract class CourseManagementSystemRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected CourseManagementSystemRazorPage()
        {
            LocalizationSourceName = CourseManagementSystemConsts.LocalizationSourceName;
        }
    }
}
