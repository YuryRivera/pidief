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

public static class RgbaChannel
{
    public const int Red = 0;
    public const int Green = 1;
    public const int Blue = 2;
    public const int Alpha = 3;
}

public static class ColorByte
{
    public const int RGBA = 4;
    public const int ARGB = 4;
    public const int RGB = 3;
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
        const int BitColorBand = 8;
        if (colorText.Length < ColorWithAlpha)
        {
            // adding alpha channel;
            colorText += "ff";
        }

        uint value = uint.Parse(colorText[1..], NumberStyles.HexNumber);
        // R, G, B, A channels
        byte[] colorBytes = [byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue];

        for (int index = 0; index < ColorByte.RGBA; index++)
        {
            int loc = index * BitColorBand;
            uint byteData = (value >> loc) & byte.MaxValue;
            // -1 because of arrys index starts at zero 0
            colorBytes[ColorByte.RGBA - index - 1] = (byte)byteData;
        }

        uint reorderedValue = (uint)(
            colorBytes[RgbaChannel.Alpha] << ArgbMask.Alpha |
            colorBytes[RgbaChannel.Red] << ArgbMask.Red |
            colorBytes[RgbaChannel.Green] << ArgbMask.Green |
            colorBytes[RgbaChannel.Blue] << ArgbMask.Blue
        );

        return new ArgbColor(reorderedValue);
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

        uint value = (uint)(
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