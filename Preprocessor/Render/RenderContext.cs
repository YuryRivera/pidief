namespace Preprocessor.Render;

public class PropertyContextArgs
{

    public float? X { get; set; } = null;
    public float? Y { get; set; } = null;
    public float? FontSize { get; set; } = null;
    public int? FontWeight { get; set; } = null;
    public string? FontFace { get; set; } = null;
    public string? FontColor { get; set; } = null;
}

public sealed record PropertyContext(
    float X = 0,
     float Y = 0,
     float FontSize = 12,
     string FontFace = "",
     string FontColor = "#000",
     int FontWeight = 400
)
{
    public PropertyContext CopyWith(PropertyContextArgs args) => this with
    {
        FontColor = args.FontColor ?? FontColor,
        FontFace = args.FontFace ?? FontFace,
        FontSize = args.FontSize ?? FontSize,
        X = args.X ?? X,
        Y = args.Y ?? Y,
    };
}