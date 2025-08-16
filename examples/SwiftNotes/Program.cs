using System;
using SwiftSharp;
using SwiftSharp.Collections;

namespace SwiftNotes
{
    public static class Program
    {
        public static void Main()
        {
            var numbers = new Array<int>([1, 2, 3]);
            numbers.Append(4);

            Console.WriteLine(numbers);
        }
    }
}