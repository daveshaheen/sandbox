using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixel
{
    public class Logger : ILogger
    {
        public void Log()
        {
            WriteMessage();
        }

        public void Log(string message)
        {
            WriteMessage(message);
        }

        public void Log(char c)
        {
            WriteMessage(c.ToString());
        }

        public void Log(IEnumerable<char> chars)
        {
            var str = chars.Aggregate("", (current, c) => $"{current}{c}");

            WriteMessage(str);
        }

        private static void WriteMessage(string message = "")
        {
            Console.WriteLine(message);
        }
    }
}
