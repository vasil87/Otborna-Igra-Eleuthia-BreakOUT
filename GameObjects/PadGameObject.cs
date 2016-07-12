

namespace OtbornaIgra.GameObjects
{
    using OtbornaIgra.Interfaces;
    public class PadGameObject : GameObjects, IGameObject,IRebouncable
    {
        public PadGameObject(Position givePostion,Size giveSize):base(givePostion,giveSize)
        {
                
        }
    }
}
