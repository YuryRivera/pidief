using Preprocessor.DrawCall;
using Preprocessor.Parser;
using Preprocessor.Render;

namespace Preprocessor.Process;

public static class SvgTags
{
    public const string Svg = "svg";
    public const string Text = "text";
    public const string Tspan = "tspan";
    public const string G = "g";
    public const string Rect = "rect";
}

public static class SvgAttribs
{
    public const string Fill = "fill";
    public const string XmlSpace = "xml:space";
    public const string Style = "style";
    public const string FontFamily = "font-family";
    public const string FontSize = "font-size";
    public const string LetterSpacing = "letter-spacing";
    public const string FontWeight = "font-weight";
    public const string X = "x";
    public const string Y = "y";
    public const string Id = "id";
}

public static class XmlDrawCallExtractor
{

    public static PropertyContextArgs ExtractProperties(XmlElementNode node)
    {
        int? weight = null;
        float? x = null;
        float y = 0;
        float fontSize = 0;
        string? fillColor = null;
        string? fontFamily = null;

        foreach (var attrib in node.Attributes)
        {
            var attribName = attrib.Name.ToLower();
            string? value = attrib.Value;

            switch (attribName)
            {
                case SvgAttribs.Fill:
                    fillColor = value;
                    continue;
                case SvgAttribs.FontFamily:
                    fontFamily = value;
                    continue;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(value)) continue;

            // this way we save checking for null in the remaining cases

            switch (attribName)
            {
                case SvgAttribs.X:
                    if (float.TryParse(value, out float tempX))
                        x = tempX;
                    break;
                case SvgAttribs.Y:
                    if (float.TryParse(value, out float tempY))
                        y = tempY;
                    break;

                case SvgAttribs.FontSize:
                    if (float.TryParse(value, out float tempSize))
                        fontSize = tempSize;
                    break;
                case SvgAttribs.FontWeight:
                    if (int.TryParse(value, out int tempWeight))
                        weight = tempWeight;
                    break;
            }
        }

        return new PropertyContextArgs
        {
            FontColor = fillColor,
            FontFace = fontFamily,
            FontSize = fontSize,
            FontWeight = weight,
            X = x,
            Y = y
        };
    }
}