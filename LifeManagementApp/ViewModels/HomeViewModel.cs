
using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using System.Collections.ObjectModel;

namespace LifeManagementApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly WeatherService _weatherService;
        private readonly TodoService _todoService;

        public ObservableCollection<Todo> ImportantTodos { get; set; }
            = new ObservableCollection<Todo>();

        public HomeViewModel(TodoService todoService, WeatherService weatherService)
        {
            _todoService = todoService;
            _weatherService = weatherService;
        }

        public async Task LoadImportantTodosAsync()
        {
            var todos = await _todoService.GetTopThreeTodosAsync();

            ImportantTodos.Clear();

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
        private bool isKitesurfingWeather;

        public async Task LoadWeatherAsync()
        {
            var weather = await _weatherService.GetWeatherAsync();

            Temperature = $"{weather.Temperature} °C";
            WindSpeed = $"{weather.WindSpeed} m/s";

            IsKitesurfingWeather = weather.WindSpeed > 5;
        }
    }
}
