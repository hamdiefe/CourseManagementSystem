using Abp.AutoMapper;
using CourseManagementSystem.Sessions.Dto;

namespace CourseManagementSystem.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
