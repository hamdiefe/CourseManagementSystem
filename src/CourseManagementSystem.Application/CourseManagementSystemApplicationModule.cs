using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CourseManagementSystem.Authorization;

namespace CourseManagementSystem
{
    [DependsOn(
        typeof(CourseManagementSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CourseManagementSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CourseManagementSystemAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CourseManagementSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
