using Reversi.Logic.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class Player : INotifyPropertyChanged
    {
        public Token Token { get; private set; }
        //public int Score { get; set; }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public string TokenColor { get { return Token.ToString(); } }

        public Player(Token t)
        {
            Token = t;
            Name = Token.ToString();
        }

        public override string ToString()
        {
            return Token.ToString(); //return string.Format("{0} : {1}", Token, Score);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}