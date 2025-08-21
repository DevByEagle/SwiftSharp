using System;

namespace SwiftSharp.UI.Rendering
{
    internal class GraphicsContext
    {
        public static GraphicsContext Current => new();

        private GraphicsContext() { }
    }
}