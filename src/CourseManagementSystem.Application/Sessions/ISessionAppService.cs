using System.Threading.Tasks;
using Abp.Application.Services;
using CourseManagementSystem.Sessions.Dto;

namespace CourseManagementSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
