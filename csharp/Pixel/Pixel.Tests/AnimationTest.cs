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
            var resultString0 = Animation.GetResultString(Animation.HandleAnimation(_tests[0].Speed, Animation.GetInitialPixelPositions(_tests[0].Init)));
            var resultString1 = Animation.GetResultString(Animation.HandleAnimation(_tests[1].Speed, Animation.GetInitialPixelPositions(_tests[1].Init)));
            var resultString2 = Animation.GetResultString(Animation.HandleAnimation(_tests[2].Speed, Animation.GetInitialPixelPositions(_tests[2].Init)));
            var resultString3 = Animation.GetResultString(Animation.HandleAnimation(_tests[3].Speed, Animation.GetInitialPixelPositions(_tests[3].Init)));
            var resultString4 = Animation.GetResultString(Animation.HandleAnimation(_tests[4].Speed, Animation.GetInitialPixelPositions(_tests[4].Init)));
            var resultString5 = Animation.GetResultString(Animation.HandleAnimation(_tests[5].Speed, Animation.GetInitialPixelPositions(_tests[5].Init)));

            Assert.True(resultString0 == "....R..");
            Assert.True(resultString1 == ".Y.OR..");
            Assert.True(resultString2 == "Y..O.O..R");
            Assert.True(resultString3 == "..........");
            Assert.True(resultString4 == "...");
            Assert.True(resultString5 == "..ORY..O..RR.O..OR.");
        }

        [Fact]
        public void TestWorkForDots()
        {
            const string result0 = @"..R....
....R..
......R
.......
";

            const string result1 = @"RR..YRY
.Y.OR..
Y.....R
.......
";

            const string result2 = @"YRYR.YRYR
Y..O.O..R
.Y.Y.R.R.
.Y.....R.
.........
";

            const string result3 = @"RYRYRYRYRY
..........
";

            const string result4 = @"...
";

            const string result5 = @"YRRY.YR.YRR.R.YRRY.
..ORY..O..RR.O..OR.
.Y.OR.Y.R..RO.RY.RR
Y.Y.RO...R.YRRYR..R
.Y..YRR...O..OR.R..
Y..Y..RR.Y.RY.RR.R.
..Y....RO..YR..RR.R
.Y.....YRRY..R..RR.
Y.....Y..OR...R..RR
.....Y..Y.RR...R..R
....Y..Y...RR...R..
...Y..Y.....RR...R.
..Y..Y.......RR...R
.Y..Y.........RR...
Y..Y...........RR..
..Y.............RR.
.Y...............RR
Y.................R
...................
";

            Assert.Equal(Animation.WorkForDots(_tests[0].Speed, _tests[0].Init), result0);
            Assert.Equal(Animation.WorkForDots(_tests[1].Speed, _tests[1].Init), result1);
            Assert.Equal(Animation.WorkForDots(_tests[2].Speed, _tests[2].Init), result2);
            Assert.Equal(Animation.WorkForDots(_tests[3].Speed, _tests[3].Init), result3);
            Assert.Equal(Animation.WorkForDots(_tests[4].Speed, _tests[4].Init), result4);
            Assert.Equal(Animation.WorkForDots(_tests[5].Speed, _tests[5].Init), result5);
        }

        [Fact]
        public void TestIsFinished()
        {
            Assert.True(Animation.IsFinished("..."));
        }
    }

    public class AnimationTestCase
    {
        public int Speed { get; set; }
        public string Init { get; set; }
    }
}
