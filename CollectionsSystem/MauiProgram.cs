using CollectionsSystem.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CollectionsSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            string dbPath = FileSystem.AppDataDirectory;
            builder.Services.AddSingleton(service => ActivatorUtilities.CreateInstance<CollectionsRepository>(service, dbPath));
            Trace.WriteLine(dbPath);

            return builder.Build();
        }
    }
}
