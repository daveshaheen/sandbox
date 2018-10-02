using System.Collections.Generic;

namespace Pixel
{
    public interface ILogger
    {
        void Log();
        void Log(string message);
        void Log(char c);
        void Log(IEnumerable<char> chars);
    }
}
