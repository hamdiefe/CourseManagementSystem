using System.Threading.Tasks;
using Abp.Configuration.Startup;
using CourseManagementSystem.Services.MySettings;
using CourseManagementSystem.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Web.Views.Shared.Components.RightNavbarUserArea
{
    public class RightNavbarUserAreaViewComponent : CourseManagementSystemViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IMySettingsAppService _mySettingsAppService;

        public RightNavbarUserAreaViewComponent(
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig,
            IMySettingsAppService mySettingsAppService)

        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _mySettingsAppService = mySettingsAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RightNavbarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                Profile = _mySettingsAppService.GetProfileImageForEdit()
            };

            return View(model);
        }
    }
}

