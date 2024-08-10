using Preprocessor;
using Preprocessor.DrawCall;

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

var stream = File.Create("test.pdf");
renderer.Save(stream);
stream.Close();
// tada
stream.Dispose();
