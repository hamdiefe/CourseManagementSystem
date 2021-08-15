using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Calendar)]
    public class CalendarController : CourseManagementSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
