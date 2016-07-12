
namespace OtbornaIgra.GameObjects
{
    public class HighScore : TextGameObject

    {
        private const string highScoreText = "HIGH SCORE: 0";
        public HighScore(Position givePosition,Size giveSize) : base(givePosition,giveSize, highScoreText)
        {
            
        }
        public void IncreaseHighScore(int increase)
        {
            var score = this.Text.Remove(0, 11);
            this.ParseFromInt(int.Parse(score) + increase);
        }

        public override void ParseFromInt(int number)
        {

            this.Text = string.Format("HIGH SCORE: {0}", number);
        }
    }
}
