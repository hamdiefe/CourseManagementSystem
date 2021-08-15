using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.AuditLogs.Dtos;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.AuditLogs
{
    public interface IAuditLogsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAuditLogForViewDto>> GetAll(GetAllAuditLogsInput input);

        Task<GetAuditLogForViewDto> GetAuditLogForView(long id);
    }
}
