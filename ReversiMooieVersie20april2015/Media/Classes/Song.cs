using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiMooieVersie20april2015.Media.Classes
{
    public class Song
    {
        public Song(string title, string path)
        {
            this.Path = path;
            this.Title = title;
        }

        public string Title { get; private set; }
        public string Path { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}