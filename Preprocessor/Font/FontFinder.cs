using System.Diagnostics;
using PdfSharp.Fonts;

namespace Preprocessor.Font;

public enum FontWeight
{
    Thin = 100,
    ExtraLight = 200,
    Light = 300,
    Regular = 400,
    Medium = 500,
    SemiBold = 600,
    Bold = 700,
    ExtraBold = 800,
    Black = 900,
}

public sealed class FontFinder: IFontResolver
{
    const string GoogleFontItalicName = "Italic";

    public static string FindFontFaceName(string family, int weight, bool italic)
    {
        var fontWeight = ToFontWeight(weight);


        return "";
    }

    private static FontWeight ToFontWeight(int weight) => weight switch
    {
        < 0 => FontWeight.Regular,
        >= (int)FontWeight.Thin and < (int)FontWeight.ExtraLight => FontWeight.Thin,
        >= (int)FontWeight.ExtraLight and < (int)FontWeight.Light => FontWeight.ExtraLight,
        >= (int)FontWeight.Light and < (int)FontWeight.Regular => FontWeight.Light,
        >= (int)FontWeight.Regular and < (int)FontWeight.Medium => FontWeight.Regular,
        >= (int)FontWeight.Medium and < (int)FontWeight.SemiBold => FontWeight.Medium,
        >= (int)FontWeight.SemiBold and < (int)FontWeight.Bold => FontWeight.SemiBold,
        >= (int)FontWeight.Bold and < (int)FontWeight.ExtraBold => FontWeight.Bold,
        >= (int)FontWeight.ExtraBold and < (int)FontWeight.Black => FontWeight.ExtraBold,
        (int)FontWeight.Black => FontWeight.Black,
        _ => FontWeight.Regular,
    };

    private static string NormalizeName(string name) {
        if (string.Equals(name, "Poppins")) return "Poppins";
        return "Poppins";
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo("Poppins");
    }

    public byte[] GetFont(string faceName)
    {
        Debug.WriteLine(faceName);
        var file = File.ReadAllBytes(Path.Join("fonts", "Poppins", "Poppins-Regular.ttf"));
        return file;
    }
}