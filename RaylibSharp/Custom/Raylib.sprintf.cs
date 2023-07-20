namespace RaylibSharp;

using System.Text;

public unsafe partial class Raylib
{    /// <summary> Text formatting with variables (sprintf() style) </summary>
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

    private static string IntParser(int val, int zeros, int decimals, char c)
    {
        // int pre = zeros - decimals - 1 - (val < 0 ? 1 : 0);
        return val.ToString();
    }

    private static string FloatParser(double val, int zeros, int decimals, char c)
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

    /// <summary> Text formatting with variables (sprintf() style) </summary>
    private static string SprintF(sbyte* formatPtr, IntPtr argsPtr)
    {
        string format = new(formatPtr);

        StringBuilder sb = new();

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
                char d = format[i++];
                if (d == '0')
                {
                    decimals = int.Parse(format[i++].ToString());
                }
                else
                {
                    decimals = int.Parse(d.ToString());
                }
            }
            else if (char.IsNumber(format[i]))
            {
                c = ' ';
            }

            switch (char.ToLower(format[i]))
            {
                case 'i':
                case 'd':
                {
                    int* iptr = (int*)argsPtr;
                    sb.Append(*iptr);
                    iptr += 2;
                    argsPtr = (nint)iptr;
                }
                break;

                case 's':
                {
                    nint* ptr = (nint*)argsPtr; // Read the pointer to the string
                    sbyte* sptr = (sbyte*)*ptr; // convert the pointer to sbyte pointer

                    string str = "";

                    while (*sptr != '\0')
                    {
                        str += (char)*sptr;
                        sptr++;
                    }

                    sb.Append(str);

                    argsPtr = (nint)((int*)argsPtr + 2);
                }
                break;

                case 'f':
                {
                    double* ptr = (double*)argsPtr;
                    sb.Append(FloatParser(*ptr, zeros, decimals, c));
                    ptr++;
                    argsPtr = (nint)ptr;
                }
                break;

                default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Unhandled format: " + format[i]);
                Console.ResetColor();
                break;
            }

        }

        return sb.ToString();
    }
}

[Flags]
internal enum FormatFlags
{
    Zeropad,
    Left,
    Plus,
    Space,
    Hash,
    Uppercase,
    Char,
    Short,
    Long,
    LongLong,
    Precision,
    AdaptExp,
}
