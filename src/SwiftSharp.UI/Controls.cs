using System.Runtime.InteropServices;

namespace SwiftSharp.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Text : IView
    {
        public string Content { get; }

        public Text(string text) => Content = text;

        IView IView.Body => null;
    }
}