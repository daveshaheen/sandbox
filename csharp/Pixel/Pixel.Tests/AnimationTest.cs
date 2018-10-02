using Xunit;

namespace Pixel.Tests
{
    public class AnimationTest
    {
        private readonly AnimationTestCase[] _tests;

        public AnimationTest()
        {
            _tests = new[]
            {
                new AnimationTestCase { Speed = 2, Init = "..R...." },
                new AnimationTestCase { Speed = 3, Init = "RR..YRY" },
                new AnimationTestCase { Speed = 2, Init = "YRYR.YRYR" },
                new AnimationTestCase { Speed = 10, Init = "RYRYRYRYRY" },
                new AnimationTestCase { Speed = 1, Init = "..." },
                new AnimationTestCase { Speed = 1, Init = "YRRY.YR.YRR.R.YRRY." }
            };
        }

        [Fact]
        public void TestResultStringOfInitialPositions()
        {
            foreach (var test in _tests)
            {
                Assert.True(Animation.GetResultString(Animation.GetInitialPixelPositions(test.Init)) == test.Init);
            }
        }

        [Fact]
        public void TestHandleAnimation()
        {
            var speed = _tests[0].Speed;
            var init = _tests[0].Init;
            var initialPixelPositions = Animation.GetInitialPixelPositions(init);
            var handleAnimation = Animation.HandleAnimation(speed, initialPixelPositions);
            var resultString = Animation.GetResultString(handleAnimation);

            Assert.True(resultString == "....R..");
        }
    }

    public class AnimationTestCase
    {
        public int Speed { get; set; }
        public string Init { get; set; }
    }
}
