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

var stream = File.Create("test.pdf");
renderer.Save(stream);
stream.Close();
// tada
stream.Dispose();