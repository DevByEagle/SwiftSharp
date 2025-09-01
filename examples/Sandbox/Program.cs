using System;
using System.Runtime.InteropServices;
using SwiftSharp;
using SwiftSharp.Foundation;

namespace Sandbox
{
    internal class Program
    {
        public static unsafe void Main()
        {
            SwiftSharp.Dictionary<string, object> dictionary = new SwiftSharp.Dictionary<string, object>(
                ("John", 32)
            );

            dictionary["John"] = 1;
        }
    }
}