using System.Globalization;
using System.Text;

namespace Vulns.Core;

public static class StringExtensions
{
    private static string[] ShortPrepositions = new string[]
    {
        "about", "above", "across", "after", "ago", "at", "before",
        "below", "beside", "by", "down", "during", "for", "from",
        "in", "into", "off", "on", "over", "past", "since", "through",
        "to", "under", "until", "up", "with", "not", "any", "of"
    };

    public static String Humanize(this String input, bool replaceUnderscores = true)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        if (input.Length == 0) return input;
        var sb = new StringBuilder();
        sb.Append(input[0].ToUppercase());
        for (int i = 1; i < input.Length; i++)
        {
            if (input[i].IsUppercase() && input[i - 1].IsLowercase())
                sb.Append(' ');
            sb.Append(input[i]);
        }
        var str = sb.ToString();
        if (replaceUnderscores) str = str.Replace('_', ' ');
        var parts = sb.ToString().Split(' ');
        var output = "";
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Length <= 3)
                if (!ShortPrepositions.Contains(parts[i].ToLower()))
                    output += parts[i].ToUpper();
                else
                    output += parts[i];
            else
                output += textInfo.ToTitleCase(parts[i]);
            if (i < parts.Length - 1)
                output += ' ';
        }

        return output;
    }

    public static String ToSnakeCase(this String input)
    {
        if (input.Length == 0) return input;
        var inputArray = input.Replace(' ', '-').ToCharArray();
        var sb = new StringBuilder();
        sb.Append(inputArray[0]);
        for (int i = 1; i < inputArray.Length; i++)
        {
            if (inputArray[i].IsUppercase() && inputArray[i - 1].IsLowercase())
                sb.Append('-');
            sb.Append(inputArray[i]);
        }
        return sb.ToString().ToLower();
    }

    public static String HumanizeFrom(this String input, String? text, bool replaceUnderscores = true)
    {
        if (replaceUnderscores) input = input.Replace('_', ' ');
        if (text == null) return input.Humanize();
        var lowText = text.ToLower();
        var lowInput = input.ToLower();
        if (lowText.Contains(input.ToLower()))
            return text.Substring(lowText.IndexOf(lowInput), lowInput.Length);
        return input.Humanize();
    }

    public static String TitleCase(this String title)
    {
        var splittedTitle = title.Split(' ');
        var builder = new StringBuilder();
        foreach (var part in splittedTitle)
        {
            if (part.Length > 1)
            {
                builder.Append(part[0].ToUppercase());
                builder.Append(part.Substring(1));
            }
            else if (part.Length == 1 && part.ToLower() != "a")
            {
                builder.Append(part.ToUpper());
            }
            else
            {
                builder.Append(part);
            }
            builder.Append(' ');
        }
        try
        {
            builder.Remove(builder.Length - 1, 0);
        }
        catch {}
        return builder.ToString();
    }
}