using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.GameObjects
{
   public struct Position
    {
       

        public Position(int left, int top) : this()
        {
            this.Left = left;
            this.Top = top;
        }

        public int Left { get; set; }
        public int Top { get; set; }

        
    }
}
