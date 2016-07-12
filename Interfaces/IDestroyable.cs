

namespace OtbornaIgra.Interfaces
{
    public interface IDestroyable:IGameObject
    {   
        int PointsForBraking { get; }
        bool IsAlive { get; }
        void DestroyMe();

    }
}
