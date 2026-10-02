
namespace QueueMS.Appilication.Helper;

public static class TokenPrefixGenerator
{
    public static string Generate(string serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
        {
            throw new ArgumentException("Service name is required");
        }
        var words = serviceName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if(words.Length == 1)
        {
            return words[0]
                .Substring(0, Math.Min(2, words[0].Length))
                .ToUpper();
        }

        var result = string.Concat(
                words.Select(x => char.ToUpper(x[0]))
            );

        Console.WriteLine(result);

        return result;
    }
}
