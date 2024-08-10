using Preprocessor.DrawCall;
using Preprocessor.Parser;

namespace Preprocessor.Process;

public static class SvgTags
{
    public const string Svg = "svg";
    public const string Text = "text";
    public const string Tspan = "tspan";
    public const string G = "g";
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

public sealed class XmlDrawCallExtractor
{

    public static DrawCallText ExtracTextCall(XmlElementNode node)
    {
        float x = 0;
        float y = 0;
        int weight = 400;
        string fontFamily = string.Empty;
        string text = string.Empty;
        string fillColor = "#000";
        float fontSize = 12;

        foreach (var (Name, Value) in node.Attributes)
        {
            var attribName = Name.ToLower();

            switch (attribName)
            {
                case SvgAttribs.X:
                    _ = float.TryParse(Value, out x);
                    break;
                case SvgAttribs.Y:
                    _ = float.TryParse(Value, out y);
                    break;
                case SvgAttribs.Fill:
                    fillColor = Value ?? fillColor;
                    break;
                case SvgAttribs.FontFamily:
                    fontFamily = Value ?? fontFamily;
                    break;
                case SvgAttribs.FontSize:
                    _ = float.TryParse(Value, out fontSize);
                    break;
                case SvgAttribs.FontWeight:
                    _ = int.TryParse(Value, out weight);
                    break;
            }
        }

        if (node.Children.Count == 1)
        {
            var child = node.Children.First();

            // only has a string as children
            if (child is XmlElementText textChild)
            {
                text = textChild.Text;
            }
        }

        return new DrawCallText(text, x, y);
    }
}