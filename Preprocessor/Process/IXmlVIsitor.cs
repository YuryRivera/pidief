using Preprocessor.Parser;
using Preprocessor.Render;

namespace Preprocessor.Process;

public interface IXmlVisitor
{
    public void VisitRoot(XmlElementNode node);

    public void VisitText(XmlElementNode node);

    public void VisitTspan(XmlElementNode node);

    public void VisitString(XmlElementText text, PropertyContext ctx);

    public void VisitRect(XmlElementNode rect);
}