using System;
using System.Runtime.InteropServices;

namespace SwiftSharp.UI
{
    public enum TextAlignment
    {
        Leading,
        Center,
        Trailing
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed partial class Text : View
    {
        public string Content { get; }
        public Font Font { get; }
        public TextAlignment Alignment { get; }

        private Text(string content, Font font, TextAlignment alignment)
        {
            Content = content ?? string.Empty;
            Font = font;
            Alignment = alignment;
        }

        public Text(string content) : this(content, new Font(), TextAlignment.Leading) {}
    }
}