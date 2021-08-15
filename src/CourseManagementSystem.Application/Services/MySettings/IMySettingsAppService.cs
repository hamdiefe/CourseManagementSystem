using Abp.Application.Services;
using CourseManagementSystem.Services.MySettings.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.MySettings
{
    public interface IMySettingsAppService : IApplicationService
    {
        Task<bool> ChangePassword(ChangePasswordDto input);

        BasicInformationsDto GetBasicInformationsForEdit();

        Task<bool> UpdateBasicInformations(BasicInformationsDto input);

        ProfileImageDto GetProfileImageForEdit();

        Task<bool> UpdateProfileImage(ProfileImageDto input);

        Task<List<UserLoginAttemptDto>> GetUserLoginAttempts();
    }
}
