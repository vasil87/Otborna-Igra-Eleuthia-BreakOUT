


namespace OtbornaIgra.GameObjects
{
    using OtbornaIgra.Interfaces;
    public class BricksGameObject : GameObjects, IGameObject,IDestroyable
    {
        private bool isAlive;
        private const int pointsForBraking = 15;

        public BricksGameObject(Position givePosition,Size giveSize) : base(givePosition,giveSize)
        {
            this.IsAlive = true;
        }
        public bool IsAlive
        {
            get
            {
                return this.isAlive;
            }

            private set
            {
                this.isAlive = value;
            }
        }

        public int PointsForBraking
        {
            get
            {
                return pointsForBraking;
            }
        }

        public void DestroyMe()
        {
            this.IsAlive = false;
        }
    }
}
