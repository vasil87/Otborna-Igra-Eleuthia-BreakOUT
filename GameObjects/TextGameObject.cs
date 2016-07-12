using OtbornaIgra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.GameObjects
{
    public abstract class TextGameObject : GameObjects, IGameObject, ITextGameObject
    {
        

        public TextGameObject(Position givePosition, Size giveSize, string giveText):base(givePosition,giveSize)
        {
            this.Text = giveText;
        }

        private string text;
        public virtual string Text
        {
            get
            {
                return this.text;
            }

           protected set
            {
                this.text = value;
            }
        }

        public virtual void ParseFromInt(int number)
        {
            this.Text =string.Format("{0}", number);
        }
    }
}
