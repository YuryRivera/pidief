using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Preprocessor.DrawCall;

namespace Preprocessor;

public sealed class PDFRenderer
{
    private readonly PdfDocument doc = new();
    private PdfPage currentPage = null!;
    private XGraphics g = null!;

    public void Save(Stream ss) {
        doc.Save(ss);
        doc.Dispose();
    }

    public void Render(IEnumerable<DrawCall.DrawCall> calls)
    {
        foreach (var call in calls)
        {
            switch (call)
            {
                case PageCall pc:
                    RenderPage(pc);
                    break;
                case DrawCallText tc:
                    RenderText(tc);
                    break;
            }
        }
    }

    public void RenderPage(PageCall pc)
    {
        currentPage = doc.AddPage();
        currentPage.Size = PdfSharp.PageSize.A4;
        // currentPage.Width = new XUnit(pc.Width);
        // currentPage.Height = new XUnit(pc.Height);
        g = XGraphics.FromPdfPage(currentPage);
    }

    public void RenderText(DrawCallText tc) {
        var style = ComputeStyle(false, false);
        var font = new XFont("Poppins", tc.Size, style);

        g.DrawString(tc.Text, font, XBrushes.Black, tc.X, tc.Y);
    }

    public static XFontStyleEx ComputeStyle(bool bold, bool italic) {
        if (bold && italic) return XFontStyleEx.BoldItalic;
        if(bold) return XFontStyleEx.Bold;
        if(italic) return XFontStyleEx.Italic;

        return XFontStyleEx.Regular; 
    }
}
