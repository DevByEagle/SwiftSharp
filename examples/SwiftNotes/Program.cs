using System;
using SwiftSharp;
using SwiftSharp.Foundation;

namespace SwiftNotes
{
    class NetworkError : Exception
    {
        public NetworkError() : base("bthe ") {}
    }

    internal class Program
    {
        public static void Main()
        {
            var bundle = Bundle.Main;
            Console.WriteLine("Bundle Identifier: " + bundle.BundleIdentifier);

            var defaults = UserDefaults.Standard;

            defaults.Set("Username", "DevByEagle");
            defaults.Set("HighScore", 9001);
            defaults.Set("SoundEnabled", true);

            string username = defaults.Get("Username", "Unknown");
            int highScore = defaults.Get("HighScore", 0);
            bool sound = defaults.Get("SoundEnabled", false);

            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"HighScore: {highScore}");
            Console.WriteLine($"SoundEnabled: {sound}");
        }
    }
}