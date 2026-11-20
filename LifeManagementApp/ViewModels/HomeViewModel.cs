
using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using System.Collections.ObjectModel;

namespace LifeManagementApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        // get todo and weather data
        private readonly WeatherService _weatherService;
        private readonly TodoService _todoService;

        //three most important todos
        public ObservableCollection<Todo> ImportantTodos { get; set; }
            = new ObservableCollection<Todo>();

        // receives the services
        public HomeViewModel(TodoService todoService, WeatherService weatherService)
        {
            _todoService = todoService;
            _weatherService = weatherService;
        }

        public async Task LoadImportantTodosAsync()
        {
            // get tree unfinished todos
            var todos = await _todoService.GetTopThreeTodosAsync();

            ImportantTodos.Clear();

            // Add todos to list
            foreach (var todo in todos)
            {
                ImportantTodos.Add(todo);
            }
        }

        [ObservableProperty]
        private string temperature = "Loading...";

        [ObservableProperty]
        private string windSpeed = "Loading...";

        [ObservableProperty]
        private string weatherDescription = "Loading...";

        [ObservableProperty]
        private bool isKitesurfingWeather;

        [ObservableProperty]
        private string kitesurfingMessage = string.Empty;

        public async Task LoadWeatherAsync()
        {
            try
            {
                // Current weather data
                var weather = await _weatherService.GetWeatherAsync();

                // updates data
                Temperature = $"{weather.Temperature} °C";
                WindSpeed = $"{weather.WindSpeed} m/s";

                IsKitesurfingWeather = weather.WindSpeed > 5;

                KitesurfingMessage = IsKitesurfingWeather
                    ? $"Hyvät olosuhteet leijalautailuun! Tuuli {weather.WindSpeed:F1} m/s."
                    : $"Huonot olosuhteet leijalautailuun! Tuuli {weather.WindSpeed:F1} m/s.";

                WeatherDescription = weather.WeatherCode switch
                {
                    0 => "☀️ Selkeää",
                    1 => "🌤️ Enimmäkseen selkeää",
                    2 => "⛅ Puolipilvistä",
                    3 => "☁️ Pilvistä",
                    45 or 48 => "🌫️ Sumua",
                    51 or 53 or 55 => "🌦️ Tihkusadetta",
                    61 or 63 or 65 => "🌧️ Vesisadetta",
                    71 or 73 or 75 => "❄️ Lumisadetta",
                    80 or 81 or 82 => "🌦️ Sadekuuroja",
                    95 => "⛈️ Ukkosta",
                    _ => "❔ Tuntematon säätila"
                };
            }
            catch (Exception)
            {
                Temperature = "Weather unavailable";
                WindSpeed = "";
                IsKitesurfingWeather = false;
            }
        }
    }
}
