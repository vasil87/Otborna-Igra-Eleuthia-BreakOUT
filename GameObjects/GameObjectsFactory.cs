

namespace OtbornaIgra.GameObjects
{
   
    using Interfaces;
    public class GameObjectsFactory
    {
        public static ITextGameObject GenerateNewErrorText(Position pos, Size size,string text)
        {
            return new ErorTextGameObject(pos,size,text);
        }
        public static Size GenerateNewSize(int width, int height)
        {
            return new Size(width, height);
        }
        public static Position GenerateNewPosition(int giveLeft, int giveTop)
        {
            return new Position(giveLeft, giveTop);
        }
        public static Position GenerateNewPosition(Position giveSpeed,IGameObject curent)
        {
            var left = curent.Position.Left + giveSpeed.Left;
            var top = curent.Position.Top+giveSpeed.Top;
            return new Position(left, top);
        }
        public static IRebouncable GeneratePad(Position giveStartPosition,Size giveSize)
        {
            return new PadGameObject(giveStartPosition, giveSize);
        }

        public static IMovable GenerateNewBall(Position ballPosition, Size ballSize, Position ballSpeed)
        {
            return new BallGameObject(ballPosition, ballSize, ballSpeed);
        }

        public static ITextGameObject GenerateNewHighScore(Position highScorePosition, Size highScoreSize)
        {
            return new HighScore(highScorePosition, highScoreSize);
        }

        internal static IDestroyable GenerateNewBrick(Position brickPosition, Size brickSize)
        {
            return new BricksGameObject(brickPosition, brickSize);
        }
    }   
}
