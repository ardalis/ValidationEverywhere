
using Ardalis.GuardClauses;

internal class WeatherForecastEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; private set; }
    public int TemperatureC { get; private set; }
    public string ZipCode { get; private set; }

    public WeatherForecastEntity(DateOnly date,
                                 int temperatureC,
                                 string zipCode)
    {
        Date = date;
        TemperatureC = Guard.Against.OutOfRange(temperatureC, nameof(temperatureC), -273, 200);
        ZipCode = Guard.Against.InvalidZipCode(zipCode);
    }
}
