using NUnit.Framework;
using ScratchPad.Misc;

namespace ScratchPad.Tests.Misc
{
    public class MyDictionaryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MyDictionaryTest()
        {
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

            Assert.IsTrue(myDictionary["A"] == "AA");
            Assert.IsTrue(myDictionary["A", 1] == "AB");
            Assert.IsTrue(myDictionary["B"] == "BA");
            Assert.IsTrue(myDictionary["B", 1] == "BB");
            Assert.IsTrue(myDictionary["C"] == "CA");
            Assert.IsTrue(myDictionary.Get("C", 1) == "CB");
        }
    }
}
