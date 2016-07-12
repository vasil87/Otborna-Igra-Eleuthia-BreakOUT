using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Interfaces
{
    public interface ITextGameObject:IGameObject
    {
        string Text { get;}

        void ParseFromInt(int number);
    }
}
