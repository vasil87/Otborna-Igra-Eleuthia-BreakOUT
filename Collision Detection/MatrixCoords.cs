using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Collision_Detection
{
    public class MatrixCoords
    {
        public int Top { get; set; }
        public int Left { get; set; }

        public MatrixCoords(int top, int left)
        {
            this.Top = top;//row
            this.Left = left;//col
        }

        public static MatrixCoords operator +(MatrixCoords a, MatrixCoords b)
        {
            return new MatrixCoords(a.Top + b.Top,a.Left + b.Left);
        }

        public static MatrixCoords operator -(MatrixCoords a, MatrixCoords b)
        {
            return new MatrixCoords(a.Top - b.Top, a.Left - b.Left);
        }


        public override int GetHashCode()
        {
            return this.Top.GetHashCode() * 7 + this.Left;
        }

        public static bool operator ==(MatrixCoords a, MatrixCoords b)
        {
            if (a.Top == b.Top && a.Left == b.Left)
                return true;
            else { return false; }
        }

        public static bool operator !=(MatrixCoords a, MatrixCoords b)
        {
            if (a.Top == b.Top && a.Left == b.Left)
                return false;
            else { return true; }

        }

        public override bool Equals(object obj)
        {

            MatrixCoords temp = obj as MatrixCoords;

            if (temp == null) return false;
            else if (temp == this) { return true; }

            return false;
            

        }
    }
}
