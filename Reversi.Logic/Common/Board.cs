using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class Board
    {
        //dat is voor de afmetingen van het bord
        public const int Width = 8;

        protected Player[,] board;
        public Scoreboard scoreboard;
        //dit is om na te kijken of het spel gedaan is of niet
        public bool IsGameOver { get; set; }

        //werkt, afblijven dus
        public Board()
        {
            IsGameOver = false;
            board = new Player[Width, Width];
            scoreboard = new Scoreboard();
        }

        //werkt, afblijven dus
        public Player this[int x, int y]
        {
            get { return board[x, y]; }
            set { PutToken(value, x, y); }
        }

        //werkt, afblijven dus
        //hiermee wordt een token op het bord geplaatst
        private void PutToken(Player p, int x, int y)
        {
            //dus de coordinaat voor de geplaatste token wordt hier bewaard.
            Player other = board[x, y];

            //als de token niet leeg is, dan wordt er een punt afgetrokken van de andere speler
            if (other != null)
            {
                scoreboard.RemovePointFrom(other);
            }

            if (p != null)
            {
                scoreboard.AddPointTo(p);
            }

            board[x, y] = p;
        }

        [DebuggerStepThrough]
        public bool IsInBoard(int x, int y)
        {
            //is om te kijken dat er niet buiten het bord geplaatst wordt.
            return !(x < 0 | x > Width - 1 | y < 0 | y > Width - 1);
        }

        [DebuggerStepThrough]
        public bool IsEmpty(int x, int y)
        {
            return board[x, y] == null;
        }

        [DebuggerStepThrough]
        public bool CanPutToken(Player p, int x, int y)
        {
            return board[x, y] != p;
        }

        public bool CanOvertakeToken(Player p, int x, int y)
        {
            return IsInBoard(x, y) && !IsEmpty(x, y) && CanPutToken(p, x, y);
        }

        public Board Clone()
        {
            return new Board()
            {
                board = (Player[,])board.Clone(),
                scoreboard = scoreboard.Clone(),
                IsGameOver = this.IsGameOver,
            };
        }

        public int ScoreOf(Player p)
        {
            return scoreboard.ScoreOf(p);
        }
    }
}