using System;
using SwiftSharp;

namespace Sandbox
{
    internal class Program
    {
        public static void Main()
        {
            var dict = new SwiftSharp.Dictionary<string, object>(
                ("Banana", 5)
            );
            dict["Apple"] = 20;

            Console.WriteLine(dict["Banana"]);
            Console.WriteLine(dict["Apple"]);
        }
    }
}