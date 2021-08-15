using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.AuditLogs.Dtos
{
    public class GetAllAuditLogsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? ExceptionFilter { get; set; }
    }
}
