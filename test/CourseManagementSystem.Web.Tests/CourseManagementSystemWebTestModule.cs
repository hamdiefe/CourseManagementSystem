using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CourseManagementSystem.EntityFrameworkCore;
using CourseManagementSystem.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CourseManagementSystem.Web.Tests
{
    [DependsOn(
        typeof(CourseManagementSystemWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CourseManagementSystemWebTestModule : AbpModule
    {
        public CourseManagementSystemWebTestModule(CourseManagementSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CourseManagementSystemWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CourseManagementSystemWebMvcModule).Assembly);
        }
    }
}