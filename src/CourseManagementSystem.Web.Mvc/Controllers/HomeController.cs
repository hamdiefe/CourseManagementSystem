using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CourseManagementSystemControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/Calendar");
        }
    }
}
