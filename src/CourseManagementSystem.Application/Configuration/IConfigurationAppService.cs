using System.Threading.Tasks;
using CourseManagementSystem.Configuration.Dto;

namespace CourseManagementSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
