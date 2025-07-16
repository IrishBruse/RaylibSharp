namespace RaylibSharp;

using System.Text;
using System.Text.RegularExpressions;

public unsafe partial class Raylib
{
    /// <summary> Text formatting with variables (sprintf() style) </summary>
    [Obsolete("Please use C# string interpolation instead of this function", false)]
    public static string TextFormat(string format, params object[] args)
    {
        StringBuilder sb = new();
        int arg = 0;
        for (int i = 0; i < format.Length; i++)
        {
            if (format[i] != '%')
            {
                sb.Append(format[i]);
                continue;
            }

            i++;

            int zeros = 0;
            int decimals = 0;
            char c = '0';

            if (format[i] == '0')
            {
                i++;

                zeros = int.Parse(format[i++].ToString());
                _ = format[i++];  // .
                decimals = int.Parse(format[i++].ToString());
            }
            else if (char.IsNumber(format[i]))
            {
                c = ' ';
            }

            switch (char.ToLower(format[i]))
            {
                case 'i':
                case 'd':
                sb.Append(IntParser((int)args[arg], zeros, decimals, c));
                break;

                case 's':
                sb.Append((string)args[arg]);
                break;

                case 'f':
                sb.Append(FloatParser((float)args[arg], zeros, decimals, c));
                break;

                default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Unhandled format: " + format[i]);
                Console.ResetColor();
                break;
            }

            arg++;
        }

        return sb.ToString();
    }

    static string IntParser(int val, int zeros, int decimals, char c)
    {
        // int pre = zeros - decimals - 1 - (val < 0 ? 1 : 0);
        return val.ToString();
    }

    static string FloatParser(double val, int zeros, int decimals, char c)
    {
        int pre;
        if (zeros < decimals)
        {
            pre = 1;
        }
        else
        {
            pre = zeros - decimals - 1 - (val < 0 ? 1 : 0);
        }
        string format = new string(c, pre) + "." + new string(c, decimals);
        return val.ToString(format).PadRight(decimals, c);
    }

    /// <summary> TODO: </summary>
    public static string ConvertSprintfToCSharpFormat(string sprintfFormat, out List<ExpectedArgType> expectedArgTypes)
    {
        expectedArgTypes = new List<ExpectedArgType>();
        StringBuilder csharpFormat = new();
        int argIndex = 0;

        string patternWithPrecision = @"%((?:[0-9]+\$)?(?:[+-]?\d*)?(?:\.(?<precision>\d*))?)(?<type>[a-zA-Z%])";

        int lastIndex = 0;
        foreach (Match match in Regex.Matches(sprintfFormat, patternWithPrecision))
        {
            // Append the text before the current sprintf placeholder
            csharpFormat.Append(sprintfFormat.AsSpan(lastIndex, match.Index - lastIndex));

            string specifierPart = match.Groups[1].Value; // E.g., ".2" or ""
            string typeChar = match.Groups["type"].Value; // E.g., "d", "s", "f", "%"
            string precision = match.Groups["precision"].Value; // E.g., "2" or ""

            switch (typeChar)
            {
                case "%":
                csharpFormat.Append('%'); // Literal %
                break;
                case "d":
                case "i":
                case "u": // C# int can often handle unsigned values implicitly or with casting
                csharpFormat.Append($"{{{argIndex}}}"); // Default for integers
                expectedArgTypes.Add(ExpectedArgType.Integer);
                argIndex++;
                break;
                case "s":
                csharpFormat.Append($"{{{argIndex}}}"); // Default for strings
                expectedArgTypes.Add(ExpectedArgType.String);
                argIndex++;
                break;
                case "f":
                case "F":
                if (!string.IsNullOrEmpty(precision))
                {
                    csharpFormat.Append($"{{{argIndex}:F{precision}}}"); // Fixed-point with specified precision
                }
                else
                {
                    csharpFormat.Append($"{{{argIndex}}}"); // Default float
                }
                expectedArgTypes.Add(ExpectedArgType.FloatingPoint);
                argIndex++;
                break;
                case "e":
                case "E":
                csharpFormat.Append($"{{{argIndex}:E}}"); // Scientific
                expectedArgTypes.Add(ExpectedArgType.FloatingPoint);
                argIndex++;
                break;
                case "g":
                case "G":
                csharpFormat.Append($"{{{argIndex}:G}}"); // General
                expectedArgTypes.Add(ExpectedArgType.FloatingPoint);
                argIndex++;
                break;
                case "x":
                csharpFormat.Append($"{{{argIndex}:x}}"); // Hex lowercase
                expectedArgTypes.Add(ExpectedArgType.Integer);
                argIndex++;
                break;
                case "X":
                csharpFormat.Append($"{{{argIndex}:X}}"); // Hex uppercase
                expectedArgTypes.Add(ExpectedArgType.Integer);
                argIndex++;
                break;
                case "c":
                csharpFormat.Append($"{{{argIndex}}}"); // Character
                expectedArgTypes.Add(ExpectedArgType.Character);
                argIndex++;
                break;
                case "p":
                // Pointers are tricky. Often represented as long or IntPtr in C#
                csharpFormat.Append($"{{{argIndex}}}");
                expectedArgTypes.Add(ExpectedArgType.Pointer);
                argIndex++;
                break;
                default:
                // For unsupported or unrecognized specifiers, append them as-is
                // or throw an exception, depending on desired strictness.
                csharpFormat.Append(match.Value);
                expectedArgTypes.Add(ExpectedArgType.Unknown);
                argIndex++; // Still increment, assuming it's an argument
                break;
            }
            lastIndex = match.Index + match.Length;
        }

        // Append any remaining text after the last placeholder
        csharpFormat.Append(sprintfFormat.AsSpan(lastIndex));

        return csharpFormat.ToString();
    }

}

/// <summary>
/// Specifies the expected argument type for format specifiers.
/// </summary>
public enum ExpectedArgType
{
    /// <summary>  </summary>
    Unknown,
    /// <summary>  </summary>
    Integer,
    /// <summary>  </summary>
    FloatingPoint,
    /// <summary>  </summary>
    String,
    /// <summary>  </summary>
    Character,
    /// <summary>  </summary>
    Pointer
}
