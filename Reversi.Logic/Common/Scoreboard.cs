using Reversi.Logic.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class Scoreboard : INotifyPropertyChanged
    {
        public IDictionary<Player, PlayerScore> Scores { get; set; }

        private int _black = 2;
        private int _white = 2;
        private const string WhiteCountName = "WhiteCount";
        private const string BlackCountName = "BlackCount";





        public int GetCountScorePlayerBlack
        {
            get { return _black; }
            set
            {
                if (_black != value)
                {
                    _black = value;
                    OnPropertyChanged(BlackCountName);
                }
            }
        }

        public int GetCountScorePlayerWhite
        {
            get { return _white; }
            set
            {
                if (_white != value)
                {
                    _white = value;
                    OnPropertyChanged(WhiteCountName);
                }

            }
        }

        private int _MovesCount;
        private const string MovesCountName = "MovesCount";

        public int MovesCount
        {
            get { return _MovesCount; }
            set
            {
                if (_MovesCount != value)
                {
                    _MovesCount = value;
                    OnPropertyChanged(MovesCountName);
                }
            }
        }

        public Scoreboard()
        {
            Scores = new Dictionary<Player, PlayerScore>();
            GetCountScorePlayerWhite = 0;
            GetCountScorePlayerBlack = 0;
            MovesCount = 0;

        }



        public void AddPointTo(Player p)
        {
            if (Scores.ContainsKey(p))
            {
                Scores[p].Score++;
            }
            else
            {
                Scores.Add(p, new PlayerScore() { Score = 1 });
            }

            if (Scores.ContainsKey(p))
            {
                if (p.Token == Token.Black)
                {
                    GetCountScorePlayerBlack++;
                }
                else
                {
                    GetCountScorePlayerWhite++;
                }
            }
            MovesCount++;
        }

        public void RemovePointFrom(Player p)
        {
            if (Scores.ContainsKey(p) && Scores[p].Score > 0)
            {
                Scores[p].Score--;
                if (p.Token == Token.Black)
                {
                    GetCountScorePlayerBlack--;
                }
                else
                {
                    GetCountScorePlayerWhite--;
                }
            }


        }

        public int ScoreOf(Player p)
        {
            if (Scores.ContainsKey(p))
            {
                return Scores[p].Score;
            }
            else
            {
                return 0;
            }
        }

        public Scoreboard Clone()
        {
            return new Scoreboard()
            {
                Scores = Scores.ToDictionary((x) => x.Key, x => (PlayerScore)x.Value.Clone()),
                GetCountScorePlayerBlack = GetCountScorePlayerBlack,
                GetCountScorePlayerWhite = GetCountScorePlayerWhite

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This routine is normally called from the Set accessor of each property
        // that is bound to the a WPF control.  We use the [CallMemberName] attribute
        // so that the property name of the caller will be substituted as the argument.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}