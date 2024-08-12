using System.Text.RegularExpressions;
using System.Globalization;

public record RgbaColor(byte R, byte G, byte B, byte A = 255);

public static partial class HexColorParser
{
    const int Solid = 4;
    const int WithAlpha = 5;
    const int ColorWithAlpha = 9;

    [GeneratedRegex("^#(?:[0-9a-fA-F]{3,4}){1,2}$")]
    private static partial Regex HexNumberRegex();
    // black
    private static readonly RgbaColor defaultColor = new(0, 0, 0);
    private static readonly Regex format = HexNumberRegex();

    public static bool TryParse(string colorText, out RgbaColor color)
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

    private static RgbaColor ParseColor(string colorText)
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
        return new RgbaColor(r, g, b, a);
    }

    public static RgbaColor ParseShorhand(string colorText)
    {
        char r, g, b;
        // colorText[0] is #
        r = colorText[1];
        g = colorText[2];
        b = colorText[3];

        byte redComp = ComputeDuplicateHex(r);
        byte greenComp = ComputeDuplicateHex(g);
        byte blueComp = ComputeDuplicateHex(b);
        byte alpha = colorText.Length > Solid
        ? ComputeDuplicateHex(colorText[4])
        : byte.MaxValue;

        return new RgbaColor(redComp, greenComp, blueComp, alpha);
    }

    private static byte ComputeDuplicateHex(char hex)
    {
        var valueString = new string(hex, 2);
        byte value = byte.Parse(valueString, NumberStyles.HexNumber);
        return value;
    }
}