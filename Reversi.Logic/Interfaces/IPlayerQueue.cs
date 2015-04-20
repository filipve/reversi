using Reversi.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Interfaces
{
    public delegate void OnTurnSkippedEventHandler(object sender, EventArgs e);

    public interface IPlayerQueue
    {
        Player Current { get; }
        Player Next { get; }

        void ChangePlayer();
        void SkipTurn();
        bool IsPlayerInGame(Player p);
        IEnumerable<Player> Players();
        void MakeCurrentTurnOf(Player p);
        IPlayerQueue Clone();

        event OnTurnSkippedEventHandler OnTurnSkipped;
    }
}
