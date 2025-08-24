using System;
using SwiftSharp;
using SwiftSharp.Foundation;

namespace BlockWorld
{
    internal class Program
    {
        public static void Main()
        {
            var id = new UUID();
            var id2 = new UUID();

            if (id == id2)
            {
                Console.WriteLine(id);
            }
        }
    }
}