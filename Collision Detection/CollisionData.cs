using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Collision_Detection
{
    public class CollisionData
    {
        private int collideteStaticElementIndex;

        private HitType side;

        public CollisionData(int collideteStaticElementIndex, HitType side)
        {
            this.collideteStaticElementIndex = collideteStaticElementIndex;
            this.side = side;

        }

        public int CollideteStaticElementIndex
        {
            get { return this.collideteStaticElementIndex; }
        }

        public HitType Side
        {
            get
            {
                return this.side;
            }

           
        }
    }
      
}
