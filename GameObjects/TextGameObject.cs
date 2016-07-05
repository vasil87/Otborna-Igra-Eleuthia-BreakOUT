using OtbornaIgra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.GameObjects
{
    public abstract class TextGameObject : GameObjects, IGameObject, IStaticGameObject, ITextGameObject
    {
        public TextGameObject(string text)
        {
            this.Text = text;
        }


        private string text;
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
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
