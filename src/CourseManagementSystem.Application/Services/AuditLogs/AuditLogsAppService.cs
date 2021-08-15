using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Authorization.Users;
using CourseManagementSystem.Services.AuditLogs.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.AuditLogs
{
    [AbpAuthorize(PermissionNames.Pages_AuditLogs)]
    public class AuditLogsAppService : CourseManagementSystemAppServiceBase, IAuditLogsAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;


        public AuditLogsAppService(IRepository<AuditLog, long> auditLogRepository,
                               IRepository<User, long> userRepository)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<GetAuditLogForViewDto>> GetAll(GetAllAuditLogsInput input)
        {
            var filteredAuditLog = _auditLogRepository.GetAll().
                                           WhereIf(input.ExceptionFilter != 0, x => x.Exception != "" && x.Exception != null);

            var pagedAndFilteredAuditLog = filteredAuditLog
                .OrderBy(input.Sorting ?? "id desc")
                .PageBy(input);

            var auditLogs = from o in pagedAndFilteredAuditLog
                            join o1 in _userRepository.GetAll() on o.UserId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()
                            select new GetAuditLogForViewDto()
                            {
                                AuditLog = new AuditLogDto
                                {
                                    Id = o.Id,
                                    ClientIpAddress = o.ClientIpAddress,
                                    ExecutionDuration = o.ExecutionDuration,
                                    ExecutionTime = o.ExecutionTime,
                                    MethodName = o.MethodName,
                                    ServiceName = o.ServiceName,
                                    UserId = o.UserId,
                                },
                                UserName = s1.UserName,
                                ExceptionStatus = o.Exception == null || o.Exception == "" ? false : true
                            };

            var totalCount = await filteredAuditLog.CountAsync();
            var auditLogList = await auditLogs.ToListAsync();
            return new PagedResultDto<GetAuditLogForViewDto>(
                totalCount,
                auditLogList
            );
        }

        public async Task<GetAuditLogForViewDto> GetAuditLogForView(long id)
        {
            var auditLog = await _auditLogRepository.GetAsync(id);

            var output = new GetAuditLogForViewDto { AuditLog = ObjectMapper.Map<AuditLogDto>(auditLog) };

            if (output.AuditLog.UserId != null)
            {
                var user = await _userRepository.FirstOrDefaultAsync((int)output.AuditLog.UserId);
                output.UserName = user?.UserName?.ToString();
            }

            return output;
        }
    }
}
