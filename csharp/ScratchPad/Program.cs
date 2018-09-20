using System;
using ScratchPad.Misc;

namespace ScratchPad
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            var myDictionary = new MyDictionary<string, string>
            {
                { "A", "AA" },
                { "A", "AB" },
                { "B", "BA" },
                { "B", "BB" },
                { "C", "CC" }
            };

            myDictionary["C"] = "CA";
            myDictionary.Add("C", "CB");

            Console.WriteLine(myDictionary["A"]);
            Console.WriteLine(myDictionary["A", 1]);
            Console.WriteLine(myDictionary["B"]);
            Console.WriteLine(myDictionary["B", 1]);
            Console.WriteLine(myDictionary["C"]);
            Console.WriteLine(myDictionary.Get("C", 1));
        }
    }
}
