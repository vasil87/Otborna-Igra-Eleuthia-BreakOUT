
namespace OtbornaIgra.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OtbornaIgra.GameObjects;
    using Misc;
    public interface IRenderer
    {

         int ScreenWidth { get;}
         int ScreenHeight { get;}
        void Clear();

        void Draw(params GameObjects[] drawObject);

        event EventHandler<KeyDownEventArgs> presingkey;

    }
}
