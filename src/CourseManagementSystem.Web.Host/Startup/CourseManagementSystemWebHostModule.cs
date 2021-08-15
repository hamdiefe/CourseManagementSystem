using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CourseManagementSystem.Configuration;

namespace CourseManagementSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(CourseManagementSystemWebCoreModule))]
    public class CourseManagementSystemWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CourseManagementSystemWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CourseManagementSystemWebHostModule).GetAssembly());
        }
    }
}
