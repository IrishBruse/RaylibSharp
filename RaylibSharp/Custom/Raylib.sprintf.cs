// https://github.com/mpaland/printf

// \author (c) Marco Paland (info@paland.com)
//             2014-2019, PALANDesign Hannover, Germany
//
// \license The MIT License (MIT)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// \brief Tiny printf, sprintf and (v)snprintf implementation, optimized for speed on
//        embedded systems with a very limited resources. These routines are thread
//        safe and reentrant!
//        Use this instead of the bloated standard/newlib printf cause these use
//        malloc for printf (and may not be thread safe).

// Adapted to C# by Ethan Conneely

namespace RaylibSharp;

using System.Text;

public unsafe partial class Raylib
{    /// <summary> Text formatting with variables (sprintf() style) </summary>
    [Obsolete("Please use C# string interpolation instead of this function", false)]
    public static string TextFormat(string format, params object[] args)
    {
        return format;
    }

    /// <summary> Text formatting with variables (sprintf() style) </summary>
    [Obsolete("Please use C# string interpolation instead of this function", false)]
    private static string SprintF(sbyte* formatPtr, IntPtr argsPtr)
    {
        string format = new(formatPtr);

        // if (!format.StartsWith("TEXTURE: [ID"))
        // {
        //     return "";
        // }

        StringBuilder sb = new();

        // Console.WriteLine(format);

        for (int i = 0; i < format.Length; i++)
        {
            if (format[i] != '%')
            {
                sb.Append(format[i]);
                continue;
            }

            i++;

            if (format[i] == '0')
            {
                i++;

            }
            else if (char.IsAsciiDigit(format[i]))
            {
                i++;

            }

            switch (char.ToLower(format[i]))
            {
                case 'i':
                case 'd':
                {
                    int* iptr = (int*)argsPtr;

                    sb.Append(*iptr);
                    // Console.WriteLine("%i " + *iptr);

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
                    // Console.WriteLine("%s " + str);

                    argsPtr = (nint)((int*)argsPtr + 2);
                }
                break;

                case 'f':
                {
                    float* ptr = (float*)argsPtr;

                    sb.Append(*ptr);
                    // Console.WriteLine("%s "+*fptr);
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
