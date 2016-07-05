using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.GameObjects
{
    public class HighScore : TextGameObject

    {
        public HighScore() : base("HIGH SCORE: 0")
        {
            
        }
        public override bool IsAlive
        {
            get
            {
                return true;
            }

            set
            {

            }
        }
        public void IncreaseHighScore()
        {
            var score = this.Text.Remove(0, 11);
            this.ParseFromInt(int.Parse(score) + 15);
        }

        public override void ParseFromInt(int number)
        {

            this.Text = string.Format("HIGH SCORE: {0}", number);
        }
    }
}
