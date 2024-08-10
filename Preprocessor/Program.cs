using System.Diagnostics;
using Preprocessor;
using Preprocessor.DrawCall;
using Preprocessor.Parser;

var instructions = new List<DrawCall>
{
    new PageCall(),
    new DrawCallText("Fuck the police", 200, 300),
    new DrawCallText("Love and Peace", 200, 500),
    new PageCall(),
    new DrawCallText("Gosth in theEhhhhhh shell", 200, 300),
    new DrawCallText("ehhhhhhh!", 200, 500),
};

var renderer = new PDFRenderer();
renderer.Render(instructions);

Console.WriteLine(Environment.CurrentDirectory);
var filePath = Path.Join("Preprocessor", "template", "investment.svg");
var rootNode = XmlParser.Parse(filePath);

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

DoSomething(rootNode.Children[0]);

var stream = File.Create("test.pdf");
renderer.Save(stream);
stream.Close();
// tada
stream.Dispose();