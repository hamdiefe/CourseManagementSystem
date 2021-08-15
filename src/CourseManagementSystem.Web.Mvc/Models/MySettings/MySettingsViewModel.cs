using CourseManagementSystem.Services.MySettings.Dtos;
using CourseManagementSystem.Web.Views.Shared.Components.RightNavbarLanguageSwitch;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.MySettings
{
    public class MySettingsViewModel
    {
        public RightNavbarLanguageSwitchViewModel Languages { get; set; }
        public BasicInformationsDto BasicInformations { get; set; }
        public ProfileImageDto ProfileImage { get; set; }
        public List<UserLoginAttemptDto> UserLoginAttempts { get; set; }
    }
}
