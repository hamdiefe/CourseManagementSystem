using System.Threading.Tasks;
using Abp.Application.Services;
using CourseManagementSystem.Authorization.Accounts.Dto;

namespace CourseManagementSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
