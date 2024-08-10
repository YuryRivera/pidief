using System.Xml;

namespace Preprocessor.Parser;

public static class XmlParser
{
  public static XmlElementNode Parse(string filePath)
  {
    var root = new XmlElementNode("root", [], []);
    using (XmlReader reader = XmlReader.Create(filePath))
    {
      TraverseDFS(reader, root);
    }

    return root;
  }

  private static void TraverseDFS(XmlReader reader, XmlElementNode parent)
  {
    while (reader.Read())
    {
      switch (reader.NodeType)
      {
        case XmlNodeType.Element:
          {
            var newElmement = new XmlElementNode(reader.Name, [], []);
            parent.Children.Add(newElmement);
            if (reader.HasAttributes)
            {
              while (reader.MoveToNextAttribute())
              {
                Console.WriteLine($" {reader.Name}={reader.Value}");
              }
              reader.MoveToElement();
              Console.WriteLine();
            }
            if (!reader.IsEmptyElement)
            {
              TraverseDFS(reader, newElmement);
            }
            break;
          }
        case XmlNodeType.Text:
          {
            var textNode = new XmlElementText(reader.Value);
            parent.Children.Add(textNode);
            break;
          }
        case XmlNodeType.EndElement:
          return;
      }
    }
  }
}
