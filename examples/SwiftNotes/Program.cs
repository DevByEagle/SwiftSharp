using System;
using System.IO;
using SwiftSharp;
using SwiftSharp.Foundation;

namespace SwiftNotes
{
    internal class Program
    {
        public static void Main()
        {
            var defaults = UserDefaults.Standard;
            defaults.Set("DevByEagle", "username");
            Console.WriteLine(defaults.GetString("username")!);
        }
    }
}