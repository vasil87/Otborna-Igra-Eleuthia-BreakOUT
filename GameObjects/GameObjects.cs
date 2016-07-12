namespace OtbornaIgra.GameObjects
{
    using Interfaces;
   

    public abstract class GameObjects: IGameObject
    {
        public GameObjects()
        {
            
        }

        public GameObjects(Position givePosition):this()
        {
            this.Position = givePosition;
        }

        public GameObjects(Position givePosition, Size giveSize):this(givePosition)
        {
            this.Bounds = giveSize;
        }
        public Position Position { get; set; }

        public Size Bounds { get; set; }

          

}
}
