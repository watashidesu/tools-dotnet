using Prism.Ioc;
using TypingCounter.Models;
using TypingCounter.Services;
using TypingCounter.Views;
using TypingCounter.Views.Archives;

namespace TypingCounter
{
    public class DependencyInjection
    {
        public static void Configure(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CurrentService>();
            containerRegistry.RegisterSingleton<ArchiveService>();

            containerRegistry.RegisterSingleton<CurrentHolder>();
            containerRegistry.RegisterSingleton<ArchiveHolder>();
            containerRegistry.RegisterInstance(new SettingHolder("./settings.json"));

            containerRegistry.RegisterForNavigation<Initialize>();
            containerRegistry.RegisterForNavigation<Current>();
            containerRegistry.RegisterForNavigation<Archive>();
            containerRegistry.RegisterForNavigation<ArchiveList>();
            containerRegistry.RegisterForNavigation<ArchiveChart>();
            containerRegistry.RegisterForNavigation<Error>();
            containerRegistry.RegisterForNavigation<Views.Setting>();
        }
    }
}