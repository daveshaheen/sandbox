using Xunit;

namespace Pixel.Tests
{
    public class AnimationTest
    {
        [Fact]
        public void Test()
        {
            var logger = new Logger();
            var animation = new Animation(logger);
            var tests = new[]
            {
                new AnimationTestCase { Speed = 2, Init = "..R...." },
                new AnimationTestCase { Speed = 3, Init = "RR..YRY" },
                new AnimationTestCase { Speed = 2, Init = "YRYR.YRYR" },
                new AnimationTestCase { Speed = 10, Init = "RYRYRYRYRY" },
                new AnimationTestCase { Speed = 1, Init = "..." },
                new AnimationTestCase { Speed = 1, Init = "YRRY.YR.YRR.R.YRRY." }
            };

            foreach (var test in tests)
            {
                Assert.True(Animation.GetResultString(Animation.GetInitialPixelPositions(test.Init)) == test.Init);
            }
        }
    }

    public class AnimationTestCase
    {
        public int Speed { get; set; }
        public string Init { get; set; }
    }
}
