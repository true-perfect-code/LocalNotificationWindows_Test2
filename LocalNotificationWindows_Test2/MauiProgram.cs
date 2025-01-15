﻿using Microsoft.Extensions.Logging;

namespace LocalNotificationWindows_Test2
{
    public static class AppLaunchArguments
    {
        public static string LaunchArguments { get; set; }
        public static string LaunchArgumentsGetCommandLineArgs { get; set; }
        public static string LaunchArgumentsOnNotificationInvoked { get; set; }
    }

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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
