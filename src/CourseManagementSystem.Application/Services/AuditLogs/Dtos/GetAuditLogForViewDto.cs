namespace CourseManagementSystem.Services.AuditLogs.Dtos
{
    public class GetAuditLogForViewDto
    {
        public AuditLogDto AuditLog { get; set; }
        public string UserName { get; set; }
        public bool ExceptionStatus { get; set; }
    }
}
