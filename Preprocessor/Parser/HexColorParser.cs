using System.Text.RegularExpressions;
using System.Globalization;

namespace Preprocessor.Parser;

/// <summary>
/// Bit mask constans for 32-bit ARGB color format
/// </summary>
public static class ArgbMask
{
    public const int Red = 16;
    public const int Green = 8;
    public const int Blue = 0;
    public const int Alpha = 24;
}

public static class RgbaMask
{
    public const int Red = 24;
    public const int Green = 16;
    public const int Blue = 8;
    public const int Alpha = 0;
}

public readonly struct ArgbColor(uint n)
{
    public readonly uint Value = n;
};

public static partial class HexColorParser
{
    const int Solid = 4;
    const int WithAlpha = 5;
    const int ColorWithAlpha = 9;

    [GeneratedRegex("^#(?:[0-9a-fA-F]{3,4}){1,2}$")]
    private static partial Regex HexNumberRegex();
    // black
    private static readonly ArgbColor defaultColor = new(0u);
    private static readonly Regex format = HexNumberRegex();

    public static bool TryParse(string colorText, out ArgbColor color)
    {
        color = defaultColor;
        if (!format.IsMatch(colorText))
        {
            return false;
        }

        // e.g #AAA | #BBBF
        if (colorText.Length is Solid or WithAlpha)
            color = ParseShorhand(colorText);
        else
            color = ParseColor(colorText);

        return true;
    }

    private static ArgbColor ParseColor(string colorText)
    {
        uint value = uint.Parse(colorText[1..], NumberStyles.HexNumber);
        int byteLocation = colorText.Length == ColorWithAlpha ? 24 : 16;

        byte a = byte.MaxValue;
        byte r = (byte)((value >> byteLocation) & byte.MaxValue);
        byte g = (byte)((value >> byteLocation - 8) & byte.MaxValue);
        byte b = (byte)((value >> byteLocation - 16) & byte.MaxValue);

        if (colorText.Length == ColorWithAlpha)
        {
            a = (byte)(value & byte.MaxValue);
        }
        return new ArgbColor(value);
    }

    public static ArgbColor ParseShorhand(string colorText)
    {
        char r, g, b;
        // colorText[0] is #
        r = colorText[1];
        g = colorText[2];
        b = colorText[3];

        byte red = ComputeDuplicateHex(r);
        byte green = ComputeDuplicateHex(g);
        byte blue = ComputeDuplicateHex(b);
        byte alpha = colorText.Length > Solid
        ? ComputeDuplicateHex(colorText[4])
        : byte.MaxValue;

        uint value = (uint) (
            alpha << ArgbMask.Alpha |
            red << ArgbMask.Red |
            green << ArgbMask.Green |
            blue
        );
        return new ArgbColor(value);
    }

    private static byte ComputeDuplicateHex(char hex)
    {
        var valueString = new string(hex, 2);
        byte value = byte.Parse(valueString, NumberStyles.HexNumber);
        return value;
    }
}