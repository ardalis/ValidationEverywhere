using System.Text.RegularExpressions;

public class WeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public List<WeatherForecast> GetForecasts(int days, string zipCode)
    {
        ValidateInputs(days, zipCode);

        // Generate weather forecasts
        var forecasts = Enumerable.Range(1, days).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            )).ToList();

        foreach (var forecast in forecasts)
        {
            var entity = new WeatherForecastEntity(forecast.Date, forecast.TemperatureC, zipCode);
            // TODO: Save the entity
        }

        return forecasts;
    }

    internal WeatherForecast GetForecastForDate(int year, int month, int day)
    {
        if (year < DateTime.Now.Year || year > DateTime.Now.Year + 2) throw new ArgumentOutOfRangeException("year is out of range");
        if (month < 0 || month > 12) throw new ArgumentOutOfRangeException("month is out of range");
        if (day < 0 || day > 31) throw new ArgumentOutOfRangeException("day is out of range");

        DateOnly theDate;
        try
        {
            theDate = new DateOnly(year, month, day);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid date", ex);
        }
        return new WeatherForecast(theDate, Random.Shared.Next(-20, 55), Summaries[Random.Shared.Next(Summaries.Length)]);
    }

    private void ValidateInputs(int days, string zipCode)
    {
        if (days < 1)
            throw new ArgumentException("Days must be 1 or more.");

        if (string.IsNullOrWhiteSpace(zipCode) || !Regex.IsMatch(zipCode, @"^\d{5}(-\d{4})?$"))
            throw new ArgumentException("Zip code must be in the format nnnnn or nnnnn-nnnn.");

        // test case not included in endpoint validation to show this runs
        if (zipCode == "99999") throw new ArgumentException("Zip code cannot be all 9s");
    }
}
