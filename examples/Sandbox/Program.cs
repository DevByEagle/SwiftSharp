using System;
using SwiftSharp;

internal class Program
{
    public static void Main()
    {
        var strsize = Glibc.Strlen()
        Glibc.Printf("Length of 'Hello, World!' is: {0}\n", strsize);
    }
}