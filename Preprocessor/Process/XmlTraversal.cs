using Preprocessor.Parser;
using Preprocessor.Render;

namespace Preprocessor.Process;

public class XmlTraversal
{
  private readonly IXmlVisitor _visitor;
  private readonly Stack<PropertyContext> contextStack = new();

  public XmlTraversal(IXmlVisitor visitor)
  {
    _visitor = visitor;
    contextStack.Push(new());
  }

  public void Visit(XmlNode node)
  {
    switch (node)
    {
      case XmlElementNode eNode:
        VisitElement(eNode);
        break;
      case XmlElementText tNode:
        _visitor.VisitString(tNode, contextStack.Peek());
        break;
      default:
        throw new NotImplementedException();
    };
  }

  private void VisitElement(XmlElementNode n)
  {
    var tag = n.TagName.ToLower();
    var newProps = XmlDrawCallExtractor.ExtractProperties(n);
    var newContext = contextStack.Peek().CopyWith(newProps);
    contextStack.Push(newContext);
    switch (tag)
    {
      case "root":
        _visitor.VisitRoot(n);
        break;
      case SvgTags.Text:
        _visitor.VisitText(n);
        break;
      case SvgTags.Tspan:
        _visitor.VisitTspan(n);
        break;
      case SvgTags.Rect:
        _visitor.VisitRect(n);
        break;
      default:
        break;
    }

    foreach (var child in n.Children)
    {
      Visit(child);
    }

    contextStack.Pop();
  }

}