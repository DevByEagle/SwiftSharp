using System.Runtime.InteropServices;

namespace SwiftSharp.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        #region Constructors

        public Color(double red, double green, double blue, double alpha = 1)
        {
            R = red;
            G = green;
            B = blue;
        }

        #endregion

        #region Fields & Properties

        public double R { get; }
        public double G { get; }
        public double B { get; }
        public double A { get; }

        #endregion

        #region Methods
        #endregion

        #region Utilities
        #endregion
    }
}