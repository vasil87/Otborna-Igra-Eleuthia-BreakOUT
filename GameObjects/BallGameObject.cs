namespace OtbornaIgra.GameObjects
{
    using System;
    using OtbornaIgra.Interfaces;

    public class BallGameObject : GameObjects, IMovable
    {
        public void Move(int width, int height)
        {
            this.Position = new Position(width, height);
        }
    }
}
