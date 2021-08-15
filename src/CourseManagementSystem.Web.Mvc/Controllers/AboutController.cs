using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : CourseManagementSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
