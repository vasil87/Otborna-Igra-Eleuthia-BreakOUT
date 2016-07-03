namespace OtbornaIgra.GameObjects
{
    using System;
    using OtbornaIgra.Interfaces;

    public class BallGameObject : GameObjects, IMovable, IGameObject
    {
        public void Move(int width, int height)
        {
            this.Position = new Position(width, height);
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
