using Abp.Application.Services;
using CourseManagementSystem.MultiTenancy.Dto;

namespace CourseManagementSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

