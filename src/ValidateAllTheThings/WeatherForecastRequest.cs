using System.ComponentModel.DataAnnotations;
// Define a request model with validation attributes
public class WeatherForecastRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Days must be 1 or more.")]
    public int Days { get; set; }

    [Required]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zip code must be in the format nnnnn or nnnnn-nnnn.")]
    public string ZipCode { get; set; } = string.Empty;
}
