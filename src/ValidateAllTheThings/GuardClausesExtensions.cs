using Ardalis.GuardClauses;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public static class GuardClausesExtensions
{
    private static readonly Regex ZipCodeRegex = new(@"^\d{5}(-\d{4})?$", RegexOptions.Compiled);

    public static string InvalidZipCode(
        this IGuardClause guardClause,
        string zipCode,
        [CallerArgumentExpression("zipCode")] string? parameterName = null)
    {
        Guard.Against.NullOrWhiteSpace(zipCode, parameterName, "Zip code is required.");

        if (!ZipCodeRegex.IsMatch(zipCode))
        {
            throw new ArgumentException("Zip code must be in the format NNNNN or NNNNN-NNNN.", parameterName);
        }

        if (zipCode == "99999")
        {
            throw new ArgumentException("Zip code '99999' is not allowed.", parameterName);
        }

        return zipCode;
    }
}
