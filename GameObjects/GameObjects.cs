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
        public GameObjects()
        {

            this.IsAlive = true;
        }

        public Position Position { get; set; }

        public Size Bounds { get; set; }

        public virtual bool IsAlive { get; set; }

   

}
}
