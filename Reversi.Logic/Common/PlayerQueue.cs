using Reversi.Logic.Enumerations;
using Reversi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class PlayerQueue : IPlayerQueue
    {
        private Player[] _players;

        public Player Current { get; private set; }
        public Player Next { get { return getNextPlayer(Current); } }

        public PlayerQueue()
        {
            _players = new Player[] { new Player(Token.Black), new Player(Token.White) };
            Current = _players[0];
        }

        public PlayerQueue(Player p1, Player p2)
        {
            _players = new Player[] { p1, p2 };
            Current = _players[0];
        }

        public bool IsPlayerInGame(Player p)
        {
            return _players.Contains(p);
        }

        public void ChangePlayer()
        {
            Current = Next;
        }

        private Player getNextPlayer(Player p)
        {
            return _players[(Array.IndexOf(_players, p) + 1) % _players.Length];
        }

        public void SkipTurn()
        {
            Current = getNextPlayer(getNextPlayer(Current));

            FireTurnSkippedEvent();
            //throw new NotImplementedException();
        }

        private void FireTurnSkippedEvent()
        {
            if (OnTurnSkipped != null)
            {
                OnTurnSkipped(this, EventArgs.Empty);
            }
        }


        public event OnTurnSkippedEventHandler OnTurnSkipped;

        public IEnumerable<Player> Players()
        {
            return _players.AsEnumerable();
        }

        public IPlayerQueue Clone()
        {
            return new PlayerQueue()
            {
                Current = this.Current,
                _players = this._players,
            };
        }

        public void MakeCurrentTurnOf(Player p)
        {
            Current = p;
        }
    }
}
