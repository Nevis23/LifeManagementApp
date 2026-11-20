
using System.Text.Json;

namespace LifeManagementApp.Services
{
    public class WeatherService
    {
        // Used to make requests
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(double Temperature, double WindSpeed, int WeatherCode)> GetWeatherAsync()
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=63.096&longitude=21.616&current=temperature_2m,wind_speed_10m,weather_code&wind_speed_unit=ms";

            // Gets weather data
            var response = await _httpClient.GetStringAsync(url);

            // Reads the JSON
            using JsonDocument document =
                JsonDocument.Parse(response);

            // Gets current weather
            var current = document.RootElement
                .GetProperty("current");

            double temperature = current
                .GetProperty("temperature_2m")
                .GetDouble();

            double windSpeed = current
                .GetProperty("wind_speed_10m")
                .GetDouble();

            int weatherCode = current
                .GetProperty("weather_code")
                .GetInt32();

            return (temperature, windSpeed, weatherCode);
        }
    }
}
