namespace OtbornaIgra.GameObjects
{
    using System;
    using OtbornaIgra.Interfaces;

    public class BallGameObject : GameObjects, IMovable, IGameObject
    {
        public Position BallSpeed {get;set;}

        public BallGameObject()
        {
            BallSpeed=new Position();
        }
        public void Move(int width, int height)
        {
            this.Position = new Position(width, height);
        }

        public void MoveWithCurrentSpeed()
        {
            this.Position = new Position(this.Position.Left+this.BallSpeed.Left, this.Position.Top + this.BallSpeed.Top);
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

     
    }
}
