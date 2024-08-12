using Preprocessor.Parser;
using Preprocessor.DrawCall;
using Preprocessor.Render;

namespace Preprocessor.Process;


public class XmlVisitor : IXmlVisitor
{

  private readonly List<DrawCall.DrawCall> calls = [];

  public List<DrawCall.DrawCall> DrawCalls { get => calls; }

  public void VisitRect(XmlElementNode rect)
  {
  }

  public void VisitRoot(XmlElementNode node)
  {
    calls.Add(new PageCall());
  }

  public void VisitString(XmlElementText textNode, PropertyContext ctx)
  {
    _ = HexColorParser.TryParse("#47d", out ArgbColor parsedColor);
    var textCall = new DrawCallText(
            textNode.Text,
            ctx.X,
            ctx.Y,
            ArgbColor: parsedColor.Value,
            Size: ctx.FontSize,
            Weight: ctx.FontWeight,
            FontFace: ctx.FontFace
          );
    calls.Add(textCall);
  }

  public void VisitText(XmlElementNode node)
  {
  }

  public void VisitTspan(XmlElementNode node)
  {
  }
}

