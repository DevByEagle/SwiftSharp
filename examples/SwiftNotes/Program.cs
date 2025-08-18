using System;
using SwiftSharp;

namespace SwiftNotes
{
    internal class Program
    {
        public static void Main()
        {
            Result<int, DllNotFoundException> result = new();
        }
    }
}