using Abp.Application.Services.Dto;
using System;

namespace CourseManagementSystem.Services.AuditLogs.Dtos
{
    public class AuditLogDto : EntityDto<long>
    {
        public string BrowserInfo { get; internal set; }
        public string ClientIpAddress { get; internal set; }
        public int ExecutionDuration { get; internal set; }
        public DateTime ExecutionTime { get; internal set; }
        public string Exception { get; internal set; }
        public string MethodName { get; internal set; }
        public string Parameters { get; internal set; }
        public string ServiceName { get; internal set; }
        public long? UserId { get; internal set; }
    }
}
