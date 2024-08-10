using Preprocessor.Parser;

namespace Preprocessor.Process;

public abstract class BaseXmlVisitor {

    public void Visit(XmlElementNode node) {

    }


    public abstract void VisitRoot(XmlElementNode node);

    public abstract void VisitText(XmlElementNode node);

    public abstract void VisitTspan(XmlElementNode node);

    public abstract void VisitString(XmlElementText text);

    public abstract void VisitRect(XmlElementNode rect);
}