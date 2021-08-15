using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Authorization.Accounts;
using CourseManagementSystem.Authorization.Users;
using CourseManagementSystem.Services.MySettings.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.MySettings
{
    [AbpAuthorize(PermissionNames.Pages_MySettings)]
    public class MySettingsAppService : CourseManagementSystemAppServiceBase, IMySettingsAppService
    {

        private readonly IAbpSession _abpSession;
        private readonly UserManager _userManager;
        private readonly LogInManager _logInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserLoginAttempt, long> _userLoginAttemptRepository;

        public MySettingsAppService(IAbpSession abpSession,
                                    UserManager userManager,
                                    LogInManager logInManager,
                                    IPasswordHasher<User> passwordHasher,
                                    IRepository<User, long> userRepository,
                                    IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
        {
            _abpSession = abpSession;
            _userManager = userManager;
            _logInManager = logInManager;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userLoginAttemptRepository = userLoginAttemptRepository;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException(L("ExistingPasswordDidNotMatch"));
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException(L("PasswordDidNotFollowTheRules"));
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public BasicInformationsDto GetBasicInformationsForEdit()
        {
            var output = (from o in _userRepository.GetAll().Where(x => x.Id == _abpSession.GetUserId())
                          select new BasicInformationsDto
                          {
                              Name = o.Name,
                              Surname = o.Surname,
                              Email = o.EmailAddress,
                              Phone = o.PhoneNumber,
                              UserName = o.UserName
                          }).FirstOrDefault();

            return output;
        }

        public async Task<bool> UpdateBasicInformations(BasicInformationsDto input)
        {
            var user = _userRepository.FirstOrDefault(_abpSession.GetUserId());

            user.UserName = input.UserName;
            user.Name = input.Name;
            user.Surname = input.Surname;
            user.EmailAddress = input.Email;

            await _userManager.UpdateAsync(user);
            return true;
        }

        public ProfileImageDto GetProfileImageForEdit()
        {
            var output = (from o in _userRepository.GetAll().Where(x => x.Id == _abpSession.GetUserId())
                          select new ProfileImageDto
                          {
                              ProfileImage = o.ProfileImage
                          }).FirstOrDefault();

            return output;
        }

        public async Task<bool> UpdateProfileImage(ProfileImageDto input)
        {
            var user = _userRepository.FirstOrDefault(_abpSession.GetUserId());

            user.ProfileImage = input.ProfileImage;

            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<List<UserLoginAttemptDto>> GetUserLoginAttempts()
        {
            var output = await (from o in _userLoginAttemptRepository.GetAll().Where(x => x.UserId == _abpSession.GetUserId() && x.Result == AbpLoginResultType.Success)
                                select new UserLoginAttemptDto
                                {
                                    BrowserInfo = o.BrowserInfo,
                                    ClientIpAddress = o.ClientIpAddress,
                                    Date = o.CreationTime,
                                    Location = GetLocationInfoFromIp(o.ClientIpAddress),
                                }).OrderByDescending(x => x.Date).Take(5).ToListAsync();
            return output;
        }

        private static string GetLocationInfoFromIp(string ipaddress)
        {
            try
            {

                string strResponse = new WebClient().DownloadString("http://api.ipstack.com/" + ipaddress + "?access_key=d758a527ff990a1b93eb0f69ca9d2ea8");
                if (strResponse == null || strResponse == "") return "";
                JObject json = JObject.Parse(strResponse);

                string city = "";
                string region = "";
                string country = "";

                foreach (var item in json)
                {
                    if (item.Key == "city")
                    {
                        city = item.Value.ToString();
                    }
                    if (item.Key == "region_name")
                    {
                        region = item.Value.ToString();
                    }
                    if (item.Key == "country_code")
                    {
                        country = item.Value.ToString();
                    }
                }
                var result = "";
                if (city != "")
                {
                    result += city + " ";
                }
                if (city != "")
                {
                    result += region + " ";
                }
                if (country != "")
                {
                    result += country;
                }
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
