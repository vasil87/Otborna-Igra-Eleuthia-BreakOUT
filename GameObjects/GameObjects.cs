namespace OtbornaIgra.GameObjects
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class GameObjects: IGameObject
    {
        public Position Position { get; set; }

        public Size Bounds { get; set; }

        public bool IsAlive { get; set; }

    }
}
