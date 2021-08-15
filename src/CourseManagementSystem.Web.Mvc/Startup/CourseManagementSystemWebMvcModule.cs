using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CourseManagementSystem.Configuration;

namespace CourseManagementSystem.Web.Startup
{
    [DependsOn(typeof(CourseManagementSystemWebCoreModule))]
    public class CourseManagementSystemWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CourseManagementSystemWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<CourseManagementSystemNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CourseManagementSystemWebMvcModule).GetAssembly());
        }
    }
}
