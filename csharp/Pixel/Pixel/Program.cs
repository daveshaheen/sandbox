namespace Pixel
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = new Logger();
            var animation = new Animation(logger);
            var tests = new[]
            {
                new TestCase { Speed = 2, Init = "..R...." },
                new TestCase { Speed = 3, Init = "RR..YRY" },
                new TestCase { Speed = 2, Init = "YRYR.YRYR" },
                new TestCase { Speed = 10, Init = "RYRYRYRYRY" },
                new TestCase { Speed = 1, Init = "..." },
                new TestCase { Speed = 1, Init = "YRRY.YR.YRR.R.YRRY." }
            };

            foreach (var test in tests)
            {
                animation.Animate(test.Speed, test.Init);
            }
        }
    }

    public class TestCase
    {
        public int Speed { get; set; }
        public string Init { get; set; }
    }
}
