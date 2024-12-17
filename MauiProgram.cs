using Microsoft.Extensions.Logging;

namespace Team_Manager
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
            builder.Services.AddTransient<MainPage> ();
            builder.Services.AddTransient<Klub>();
            builder.Services.AddTransient<KlubPages.ListaZawodnikow>();
            builder.Services.AddTransient<Admin>();
            builder.Services.AddTransient<Ustawienia>();
            builder.Services.AddSingleton<LocalDbServices>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
