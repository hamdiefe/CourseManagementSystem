using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CourseManagementSystem.Configuration.Dto;

namespace CourseManagementSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CourseManagementSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
