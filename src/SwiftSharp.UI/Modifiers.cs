namespace SwiftSharp.UI
{
    public sealed partial class Text
    {
        public Text Italic() =>
            new(Content, Font.System(Font.TextStyle.Body, Font.Weight.Regular), Alignment);
        
    }
}