using System;
using SwiftSharp.UI.Rendering;

namespace SwiftSharp.UI
{
    public abstract partial class View
    {
        internal virtual void Render(Rendering.GraphicsContext context) { }
    }

    public class Text(string text) : View
    {
        public string Conent { get; set; } = text;
    }
}