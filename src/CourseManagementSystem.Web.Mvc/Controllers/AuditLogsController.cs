using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Services.AuditLogs;
using CourseManagementSystem.Web.Models.AuditLogs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_AuditLogs)]
    public class AuditLogsController : CourseManagementSystemControllerBase
    {
        private readonly IAuditLogsAppService _auditLogsAppService;

        public AuditLogsController(IAuditLogsAppService auditLogsAppService)
        {
            _auditLogsAppService = auditLogsAppService;
        }

        public ActionResult Index()
        {
            var model = new AuditLogsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getAuditLogForViewDto = await _auditLogsAppService.GetAuditLogForView(id);

            var model = new AuditLogViewModel()
            {
                AuditLog = getAuditLogForViewDto.AuditLog,
                UserName = getAuditLogForViewDto.UserName
            };

            return PartialView("_ViewModal", model);
        }
    }
}
