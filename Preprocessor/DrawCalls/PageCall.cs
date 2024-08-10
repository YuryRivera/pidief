using PdfSharp;
using PdfSharp.Pdf;
using Preprocessor.DrawCall;

public enum PageOrientation
{
    Vertical,
    Horizontal
};

public enum PageFormat
{
    A3,
    A4,
    Letter
};

public sealed record PageCall : DrawCall
{
    PageOrientation Orientation { get; } = PageOrientation.Vertical;
    public int Width { get; } = 100;
    public int Height { get; } = 100;

    public PageCall(int w, int h)
    {
        Orientation = w > h ? PageOrientation.Horizontal : PageOrientation.Vertical;
        Width = w;
        Height = h;
    }

    public PageCall(PageOrientation o = PageOrientation.Vertical, PageFormat f = PageFormat.A4)
    {
        Orientation = o;
    }
}
