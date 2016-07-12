

namespace OtbornaIgra.Interfaces
{
    using OtbornaIgra.GameObjects;
    public interface IMovable:IGameObject
    {

        Position Speed { get; set; } //vector na skorostta
        void Move(int widht, int height);
        void MoveWithCurrentSpeed();


    }
}
