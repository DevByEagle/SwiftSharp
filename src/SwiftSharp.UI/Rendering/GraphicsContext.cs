using System;


namespace SwiftSharp.UI.Rendering
{
    internal class GraphicsContext
    {
        
        public static GraphicsContext? Current { get; private set; } = null!;

        internal GraphicsContext()
        {
            Current = this;
        }
    }
}