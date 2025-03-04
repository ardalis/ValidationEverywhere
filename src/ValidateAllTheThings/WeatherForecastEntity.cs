
using Ardalis.GuardClauses;
using System.Reflection.Emit;

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

    public void UpdateTemperatureC(int newTemperatureC)
    {
        TemperatureC = Guard.Against.OutOfRange(newTemperatureC, nameof(newTemperatureC), -273, 200);
    }
    public void UpdateZipCode(string newZipCode)
    {
        ZipCode = Guard.Against.InvalidZipCode(newZipCode);
    }
}
