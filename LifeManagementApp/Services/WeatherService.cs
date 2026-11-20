
using System.Text.Json;

namespace LifeManagementApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(double Temperature, double WindSpeed)> GetWeatherAsync()
        {
            string url =
                "https://api.open-meteo.com/v1/forecast?latitude=63.096&longitude=21.616&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

            var response = await _httpClient.GetStringAsync(url);

            using JsonDocument document =
                JsonDocument.Parse(response);

            var current = document.RootElement
                .GetProperty("current");

            double temperature = current
                .GetProperty("temperature_2m")
                .GetDouble();

            double windSpeed = current
                .GetProperty("wind_speed_10m")
                .GetDouble();

            return (temperature, windSpeed);
        }
    }
}
