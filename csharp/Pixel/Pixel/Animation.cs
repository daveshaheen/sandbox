using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixel
{
    public class Animation : IAnimation
    {
        private readonly ILogger _logger;

        public Animation(ILogger logger)
        {
            _logger = logger;
        }

        public void Animate(int speed, string init)
        {
            var isFinished = IsFinished(init);
            if (isFinished)
            {
                _logger.Log(init);
                _logger.Log();

                return;
            }

            var results = WorkForDots(speed, init);
            _logger.Log(results);
        }

        public static string WorkForDots(int speed, string init)
        {
            var results = new StringBuilder();
            results.AppendLine(init);

            var newPositions = HandleAnimation(speed, GetInitialPixelPositions(init));

            var isFinished = IsFinished(init);
            // print and loop until we have dots
            while (!isFinished)
            {
                // print
                var resultString = GetResultString(newPositions);
                results.AppendLine(resultString);

                // check if finished
                isFinished = IsFinished(resultString);
                if (!isFinished)
                {
                    newPositions = HandleAnimation(speed, newPositions);
                }
            }

            return results.ToString();
        }

        public static string GetResultString(Dictionary<int, IList<char>> positions)
        {
            var sb = new StringBuilder();
            foreach (var item in positions)
            {
                if (item.Value.Count == 1)
                {
                    sb.Append(item.Value.FirstOrDefault());
                }
                else
                {
                    if (item.Value.Any(r => r == 'R') && item.Value.Any(y => y == 'Y'))
                    {
                        sb.Append('O');
                    }
                    else if (item.Value.Any(r => r == 'R'))
                    {
                        sb.Append('R');
                    }
                    else if (item.Value.Any(y => y == 'Y'))
                    {
                        sb.Append('Y');
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
            }

            return sb.ToString();
        }

        public static Dictionary<int, IList<char>> HandleAnimation(int speed, IReadOnlyDictionary<int, IList<char>> pixelPositions)
        {
            // initialize results dictionary
            var results = new Dictionary<int, IList<char>>();
            for (var i = 0; i < pixelPositions.Count; i++)
            {
                results.Add(i, new List<char>());
            }

            // loop through positions
            for (var position = 0; position < pixelPositions.Count; position++)
            {
                var moveRight = position + speed;
                var moveLeft = position - speed;

                // loop though values
                var pixelValues = pixelPositions[position];
                for (var i = 0; i < pixelValues.Count; i++)
                {
                    var value = pixelValues[i];
                    switch (value)
                    {
                        case '.':
                            results[position].Add(value);
                            break;
                        case 'R':
                            if (moveRight < pixelPositions.Count)
                            {
                                results[moveRight].Add(value);
                            }

                            break;
                        case 'Y':
                            if (moveLeft >= 0)
                            {
                                results[moveLeft].Add(value);
                            }

                            break;
                    }
                }
            }

            return results;
        }

        public static Dictionary<int, IList<char>> GetInitialPixelPositions(string str)
        {
            var arrayOfChar = str.ToCharArray();
            var results = new Dictionary<int, IList<char>>();

            for (var position = 0; position < arrayOfChar.Length; position++)
            {
                var value = arrayOfChar[position];
                var chars = new List<char>();
                if (value == 'O')
                {
                    chars.Add('R');
                    chars.Add('Y');
                }
                else
                {
                    chars.Add(value);
                }

                results.Add(position, chars);
            }

            return results;
        }

        public static bool IsFinished(string str)
        {
            return str.All(c => c == '.');
        }
    }
}
