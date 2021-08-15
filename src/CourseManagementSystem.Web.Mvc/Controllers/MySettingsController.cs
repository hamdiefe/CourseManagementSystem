using Abp.AspNetCore.Mvc.Authorization;
using Abp.Localization;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Services.MySettings;
using CourseManagementSystem.Web.Models.MySettings;
using CourseManagementSystem.Web.Views.Shared.Components.RightNavbarLanguageSwitch;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_MySettings)]
    public class MySettingsController : CourseManagementSystemControllerBase
    {
        private readonly ILanguageManager _languageManager;
        private readonly IMySettingsAppService _mySettingsAppService;
        private IHostingEnvironment hostingEnv;


        public MySettingsController(ILanguageManager languageManager,
                                    IMySettingsAppService mySettingsAppService,
                                    IHostingEnvironment env)
        {
            _languageManager = languageManager;
            _mySettingsAppService = mySettingsAppService;
            this.hostingEnv = env;
        }

        public async Task<ActionResult> Index()
        {
            var model = new MySettingsViewModel
            {
                Languages = new RightNavbarLanguageSwitchViewModel
                {
                    CurrentLanguage = _languageManager.CurrentLanguage,
                    Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList(),
                },
                BasicInformations = _mySettingsAppService.GetBasicInformationsForEdit(),
                ProfileImage = _mySettingsAppService.GetProfileImageForEdit(),
                UserLoginAttempts = await _mySettingsAppService.GetUserLoginAttempts()
            };

            return View(model);
        }

        [HttpPost]
        public string UploadImage()
        {
            string result = string.Empty;
            try
            {
                long size = 0;
                var file = Request.Form.Files;
                var fileName = (ContentDispositionHeaderValue
                                 .Parse(file[0].ContentDisposition)
                                 .FileName
                                 .Trim('"')).Replace(' ', '-');

                string filePath = hostingEnv.WebRootPath + $@"\img\profile-img\{fileName}";

                if (System.IO.File.Exists(filePath))
                {
                    filePath = hostingEnv.WebRootPath + $@"\img\profile-img\{fileName}";

                    var filePathSplit = filePath.Split(".");
                    var guid = Guid.NewGuid();
                    filePathSplit[filePathSplit.Length - 2] = filePathSplit[filePathSplit.Length - 2] + "-" + guid.ToString();

                    var fileNameSplit = filePathSplit[filePathSplit.Length - 2].Split("\\");

                    fileName = fileNameSplit[fileNameSplit.Length - 1] + "." + filePathSplit[filePathSplit.Length - 1];

                    filePath = "";
                    foreach (var item in filePathSplit)
                    {
                        if (filePath == "")
                        {
                            filePath = item;

                        }
                        else
                        {
                            filePath = filePath + "." + item;
                        }
                    }
                }

                size += file[0].Length;
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }
                result = fileName;
            }

            catch (Exception ex)
            {
                result = false.ToString();
            }
            return result;
        }
    }
}
