using System;
using SwiftSharp;
using static SwiftSharp.Glibc;

internal class Program
{
    public static void Main()
    {
        var dict = new SwiftSharp.Dictionary<string, int>(
            ("Alice", 25),
            ("Bob", 30),
            ("Charlie", 20)
        );

        foreach (var key in dict.Keys)
        {
            Printf($"{key} -> {dict[key]}");
        }
    }
}