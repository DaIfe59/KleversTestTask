using System;
using System.Text;

class Program
{
    static string Compress(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        StringBuilder result = new StringBuilder();
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                result.Append(input[i - 1]);
                if (count > 1)
                    result.Append(count);

                count = 1;
            }
        }
        result.Append(input[input.Length - 1]);
        if (count > 1)
            result.Append(count);

        return result.ToString();
    }
    static string Decompress(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];
            int count = 0;

            while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
            {
                i++;
                count = count * 10 + (input[i] - '0');
            }

            if (count == 0)
                count = 1;

            result.Append(new string(currentChar, count));
        }

        return result.ToString();
    }

    static void Main()
    {
        string original = "adssssdeeedsdddd";

        string compressed = Compress(original);
        Console.WriteLine("Сжатая: " + compressed);

        string decompressed = Decompress(compressed);
        Console.WriteLine("Восстановленная: " + decompressed);
    }
}