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
                                       HttpContext http,
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
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
