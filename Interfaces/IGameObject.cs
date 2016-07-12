

namespace OtbornaIgra.Interfaces
{
    using OtbornaIgra.GameObjects;
    public interface IGameObject
    {
       
         Position Position { get; set; }

         Size Bounds { get; set; }

    }
}