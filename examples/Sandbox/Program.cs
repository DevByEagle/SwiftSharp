using System;
using System.Runtime.InteropServices;
using SwiftSharp;

namespace Sandbox
{
    internal class Program
    {
        public static unsafe void Main()
        {
            var a = Result<int, InvalidOperationException>.Success(1);
            var b = Result<int, InvalidOperationException>.Success(2);
            var c = Result<int, InvalidOperationException>.Failure(new InvalidOperationException("Error"));


            Console.WriteLine(a != b);
        }
    }
}