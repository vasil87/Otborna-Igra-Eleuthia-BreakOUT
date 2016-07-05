using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Collision_Detection
{
    public class CollisionData    //tova e custom klass chiito instancii durjat nujnata informaciq pri collision
    {
        private int collideteStaticElementIndex;

        private HitTypeEnum side;

        public CollisionData(int collideteStaticElementIndex, HitTypeEnum side)
        {
            this.collideteStaticElementIndex = collideteStaticElementIndex;
            this.side = side;

        }

        public int CollideteStaticElementIndex
        {
            get { return this.collideteStaticElementIndex; }
        }

        public HitTypeEnum Side
        {
            get
            {
                return this.side;
            }

           
        }
    }
      
}
