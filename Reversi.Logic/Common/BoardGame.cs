using Reversi.Logic.Classes;
using Reversi.Logic.Enumerations;
using Reversi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public abstract class BoardGame
    {
        protected IPlayerQueue playerQueue;

        protected Board board;

        public event EventHandler GameOver;

        public bool IsGameOver { get { return board.IsGameOver; } }

        public ISnapshotContainer<Turn> SnapshotContainer { get; set; }

        //hier worden de scores opgehaald voor in de statusbar te tonen
        public IDictionary<Player, PlayerScore> ScoreBoard { get { return board.scoreboard.Scores; } }

        public int ScoreBoardBlack { get { return board.scoreboard.GetCountScorePlayerBlack; } }

        public int ScoreBoardWhite { get { return board.scoreboard.GetCountScorePlayerWhite; } }

        public Player CurrentPlayer { get { return playerQueue.Current; } }

        public Token CurrentPlayerToken { get { return playerQueue.Current.Token; } }

        public BoardGame(IPlayerQueue playerQueue)
        {
            this.playerQueue = playerQueue;
            board = new Board();
            CreateStartingBoard();
            SnapshotContainer = new SnapshotContainer<Turn>();


        }

        public Player this[int x, int y]
        {
            get { return board[x, y]; }
        }

        protected abstract void CreateStartingBoard();

        public abstract bool CanPlay(Player p);


        public void PutToken(Player p, int x, int y)
        {
            if (!IsValid(p, x, y))
            {
                throw new ArgumentException();
            }

            board[x, y] = p;
        }

        public bool IsValid(Player p, int x, int y)
        {
            return board.IsInBoard(x, y) && board[x, y] != p && playerQueue.IsPlayerInGame(p);
        }

        public int ScoreOf(Player p)
        {
            return board.ScoreOf(p);
        }

        private void OnGameOver()
        {
            board.IsGameOver = true;

            var handler = GameOver;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public Board CloneBoard()
        {
            return board.Clone();
        }

        public void ChangePlayer()
        {
            playerQueue.ChangePlayer();
        }

        protected void HandlePlayerChange()
        {
            if (CanPlay(playerQueue.Next))
            {
                playerQueue.ChangePlayer();
            }
            else if (CanPlay(playerQueue.Current))
            {
                playerQueue.SkipTurn();
            }
            else
            {
                OnGameOver();
            }
        }

        public void LoadBoard(Board b, Player p)
        {
            this.board = b;
            playerQueue.MakeCurrentTurnOf(p);
        }


    }
}
