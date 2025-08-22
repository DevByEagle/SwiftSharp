using System;
using System.Runtime.InteropServices;

namespace SwiftSharp.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Font : IEquatable<Font>
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Weight
        {
            internal int Value;

            private Weight(int value) => Value = value;

            public static Weight UltraLight => new(100);
            public static Weight Thin => new(200);
            public static Weight Light => new(300);
            public static Weight Regular => new(400);
            public static Weight Medium => new(500);
            public static Weight Semibold => new(600);
            public static Weight Bold => new(700);
            public static Weight Heavy => new(800);
            public static Weight Black => new(900);
        }

        public enum TextStyle
        {
            Body = default,
            LargeTitle,
            Title,
            Headline,
            Caption
        }

        public float Size { get; }

        public Font(float size)
        {
            Size = size;
        }

        public static Font System(TextStyle style, Weight? weight = null)
        {
            float size = style switch
            {
                TextStyle.LargeTitle => 34f,
                TextStyle.Title => 28f,
                TextStyle.Headline => 17f,
                TextStyle.Caption => 17f,
                _ => 17f
            };

            return new Font(size);
        }

        public static Font System(float size, Weight? weight = null)
        {
            return new Font(
                size: size
            );
        }

        public override int GetHashCode() => HashCode.Combine(Size);

        public bool Equals(Font other) => Size == other.Size;

        public override bool Equals(object? obj)
        {
            return obj is Font other && Equals((Font)other);
        }
    }
}