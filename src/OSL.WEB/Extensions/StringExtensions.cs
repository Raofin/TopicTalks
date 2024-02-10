namespace OSL.WEB.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> SplitAndTrim(this string input)
    {
        return input.Split(',')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s));
    }
}