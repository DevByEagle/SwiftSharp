using System;
using SwiftSharp;
using SwiftSharp.Foundation;

internal class Program
{
    public static void Main()
    {
        Glibc.Printf("Hello, World!\n");
        ulong length = Glibc.Strlen($"Hello, {10}!");
        Console.WriteLine($"Length of 'Hello, World!': {length}");
    }
}