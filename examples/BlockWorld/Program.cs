using System;
using SwiftSharp;
using SwiftSharp.Foundation;

namespace BlockWorld
{
    public class Person : IEncodable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Dictionary<string, object> Encode()
        {
            return new Dictionary<string, object>
            {
                { "name", Name },
                { "age", Age }
            };
        }
    }

    internal class Program
    {
        public static void Main()
        {
            
        }
    }
}