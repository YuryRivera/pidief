namespace Preprocessor.DrawCall;

public enum TextAlign {
    Justify,
    Center,
    Start,
    End,
};

public enum TextBaseLine {
    Middle,
    Bottom,
    Top,
};

public enum TextCase {
    Normal,
    UpperCase,
    LowerCase,
}

public sealed record TextOptions (
  TextAlign Align =  TextAlign.Start,
  TextBaseLine Baseline = TextBaseLine.Middle,
  int Indent = 0,
  int lineGap = 1,
  string? Url = null,
  bool Underline = false
);

public sealed record DrawCallText (
  string Text,
  int X,
  int Y,
  TextCase Casing = TextCase.Normal,
  TextOptions? options = null
): DrawCall;
