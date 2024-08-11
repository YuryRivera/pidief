using Preprocessor;
using Preprocessor.Parser;
using Preprocessor.Process;

Console.WriteLine(Environment.CurrentDirectory);
var filePath = Path.Join("Preprocessor", "template", "investment.svg");
var rootNode = XmlParser.Parse(filePath);

var visitor = new XmlVisitor();
var traversal = new XmlTraversal(visitor);

traversal.Visit(rootNode);

var instructions = visitor.DrawCalls;
var renderer = new PDFRenderer();
renderer.Render(instructions);

static void DoSomething(XmlNode n, int level = 0)
{
    if (n is XmlElementNode eNode)
    {
        Console.Write("<".PadLeft(level));
        Console.Write(eNode.TagName);
        foreach(var attrib in eNode.Attributes) {
            Console.Write($" {attrib.Name}={attrib.Value}");
        }
        Console.WriteLine(">");
        foreach (var child in eNode.Children)
        {
            DoSomething(child, level + 2);
        }
        return;
    }

    if (n is XmlElementText tNode) {
        Console.WriteLine(tNode.Text.PadLeft(level + 1));
    }
}

var stream = File.Create(Path.Join("output", "test.pdf"));
renderer.Save(stream);
stream.Close();
// tada
stream.Dispose();