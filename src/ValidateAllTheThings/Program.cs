using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<WeatherService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/weatherforecast", async (WeatherForecastRequest request,
                                       WeatherService weatherService) =>
{
    // Manual validation since Minimal APIs do NOT automatically validate attributes
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(request);

    if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
    }

    try
    {
        var forecasts = weatherService.GetForecasts(request.Days, request.ZipCode);
        return Results.Ok(forecasts);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
})
.WithName("GetWeatherForecasts")
.WithOpenApi();

app.MapGet("/weatherforecast/{year}/{month}/{day}", async (
    int year,
    int month,
    int day,
    WeatherService weatherService) =>
{
    
    if (year < DateTime.Now.Year || year > DateTime.Now.Year + 2) return Results.BadRequest("Year out of range");
    if (month < 0 || month > 12) return Results.BadRequest("Month out of range");
    if (day < 0 || day > 31) return Results.BadRequest("Day out of range");

    try
    {
        var forecast = weatherService.GetForecastForDate(year, month, day);
        return Results.Ok(forecast);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }

}
)
.WithName("GetWeatherForecastForDate")
.WithOpenApi();

app.Run();
