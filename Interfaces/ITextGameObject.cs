using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Interfaces
{
    public interface ITextGameObject:IStaticGameObject
    {
        string Text { get; set;}

        void ParseFromInt(int number);
    }
}
