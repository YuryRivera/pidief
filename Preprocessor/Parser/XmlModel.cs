
namespace Preprocessor.Parser;

public sealed record XmlAttribute (string Name, string? Value);

public abstract record XmlNode {}

public sealed record XmlElementNode(
 string TagName,
  List<XmlNode> Children,
  List<XmlAttribute> Attributes
): XmlNode;

public sealed record XmlElementText(string Text): XmlNode;
