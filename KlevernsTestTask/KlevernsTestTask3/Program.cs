using System.Text.RegularExpressions;
using KlevernsTestTask3;
using System.Globalization;

class Program
{
    static void Main()
    {
        string inputPath = "input.txt";
        string outputPath = "output.txt";
        string problemPath = "problems.txt";

        using var writer = new StreamWriter(outputPath);
        using var problemWriter = new StreamWriter(problemPath);

        foreach (var line in File.ReadLines(inputPath))
        {
            var entry = Parse(line);

            if (entry == null)
            {
                problemWriter.WriteLine(line);
                continue;
            }

            writer.WriteLine(entry.Date.ToString("yyyy-MM-dd"));
            writer.WriteLine($"{entry.Time}\t{entry.Level}\t{entry.Method}");
            writer.WriteLine(entry.Message);
        }

        Console.WriteLine("Готово!");
    }
    static LogEntry Parse(string line)
    {
        return ParseFormat1(line) ?? ParseFormat2(line);
    }
    static LogEntry ParseFormat1(string line)
    {
        var match = Regex.Match(line,
            @"^(?<date>\d{2}\.\d{2}\.\d{4}) (?<time>\d{2}:\d{2}:\d{2}\.\d+) (?<level>\w+) (?<msg>.+)$");

        if (!match.Success) return null;

        return new LogEntry
        {
            Date = DateTime.ParseExact(match.Groups["date"].Value, "dd.MM.yyyy", CultureInfo.InvariantCulture),
            Time = match.Groups["time"].Value,
            Level = NormalizeLevel(match.Groups["level"].Value),
            Method = "DEFAULT",
            Message = match.Groups["msg"].Value
        };
    }
    static LogEntry ParseFormat2(string line)
    {
        var match = Regex.Match(line,
            @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d+)\|\s*(?<level>\w+)\|\d+\|(?<method>[^|]+)\|\s*(?<msg>.+)$");

        if (!match.Success) return null;

        return new LogEntry
        {
            Date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            Time = match.Groups["time"].Value,
            Level = NormalizeLevel(match.Groups["level"].Value),
            Method = match.Groups["method"].Value,
            Message = match.Groups["msg"].Value
        };
    }

    static string NormalizeLevel(string level)
    {
        return level switch
        {
            "INFORMATION" => "INFO",
            "WARNING" => "WARN",
            _ => level
        };
    }
}