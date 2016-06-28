
namespace OtbornaIgra.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class KeyDownEventArgs:EventArgs
    {
        public GameComand Command { get; set; }
        public KeyDownEventArgs(GameComand enteredCommand)
        {
            this.Command = enteredCommand;
        }
    }
}
