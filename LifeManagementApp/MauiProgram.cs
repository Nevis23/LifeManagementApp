using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LifeManagementApp.Data;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace LifeManagementApp
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

            var databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "lifemanagement.db"
            );

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlite($"Data Source={databasePath}")
            );

            builder.Services.AddSingleton<TodoService>();

            builder.Services.AddTransient<TodoViewModel>();

            builder.Services.AddTransient<TodoPage>();

            return builder.Build();
        }
    }
}
