using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CourseManagementSystem.Localization
{
    public static class CourseManagementSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CourseManagementSystemConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CourseManagementSystemLocalizationConfigurer).GetAssembly(),
                        "CourseManagementSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
