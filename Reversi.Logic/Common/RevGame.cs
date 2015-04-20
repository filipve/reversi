using Reversi.Logic.Classes;
using Reversi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class RevGame : BoardGame
    {
        public RevGame() : this(new PlayerQueue()) { }

        public RevGame(IPlayerQueue queue) : base(queue) { }

        public RevGame(Player black, Player white) : this(new PlayerQueue(black, white)) { }

        protected override void CreateStartingBoard()
        {
            //is gewoon voor de beginPosities
            PutToken(playerQueue.Current, 3, 4);
            PutToken(playerQueue.Next, 3, 3);
            PutToken(playerQueue.Current, 4, 3);
            PutToken(playerQueue.Next, 4, 4);
        }

        public void Play(Player p, int x, int y)
        {
            if (!IsValidPlay(p, x, y))
            {
                return;
            }

            var list = MakeListOfConvertedTokens(p, x, y);
            if (list.Any())
            {
                PutTokens(p, list);
                HandlePlayerChange();
                if (SnapshotContainer != null)
                {
                    SnapshotContainer.TakeSnapShot(new OnePartTurn() { Coord = new Coord(x, y), PlayerThatPlayed = p, PlayerToPlay = CurrentPlayer, Board = board.Clone() });
                }
            }
        }




        public override bool CanPlay(Player p)
        {
            SpiralCoordEnumerator enumerator = new SpiralCoordEnumerator(8, 1);
            while (enumerator.MoveNext())
            {
                var coord = enumerator.Current;
                if (CanPlayThere(p, coord.x, coord.y))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanPlayThere(Player p, int x, int y)
        {
            if (!IsValidPlay(p, x, y))
            {
                return false;
            }

            var calc = new OffsetCalculator(x, y);
            for (int dir = 0; dir < 9; dir++)
            {
                calc.setDirection(dir);
                int offset = CountLine(p, calc);

                if (offset > 1)
                {
                    return true;
                }
            }

            return false;
        }


        public IEnumerable<Coord> MakeListOfConvertedTokens(Player p, int x, int y)
        {
            var playChanges = new List<Coord>();

            OffsetCalculator calc = new OffsetCalculator(x, y);

            for (int dir = 0; dir < 9; dir++)
            {
                calc.setDirection(dir);
                int length = CountLine(p, calc);
                calc.MakeOffset(length);

                if (LineShouldChangeOwnership(length, calc, p))
                {
                    var lineChanges = ListChangesInLine(length, calc);
                    playChanges.AddRange(lineChanges);
                }
            }

            if (playChanges.Any())
            {//als er een lege plaats wordt aangeklikt, kom je hier zeker al terecht
                playChanges.Add(new Coord(x, y));
            }

            return playChanges;
        }

        private void PutTokens(Player p, IEnumerable<Coord> coords)
        {
            foreach (Coord item in coords)
            {
                PutToken(p, item.x, item.y);
            }
        }

        private IEnumerable<Coord> ListChangesInLine(int length, OffsetCalculator calc)
        {
            for (int i = length - 1; i > 0; i--)
            {
                calc.MakeOffset(i);
                yield return new Coord(calc.ResultX, calc.ResultY);
            }
        }

        private bool LineShouldChangeOwnership(int length, OffsetCalculator calc, Player p)
        {
            return length > 0 && board[calc.ResultX, calc.ResultY] == p;
        }

        protected bool IsValidPlay(Player p, int x, int y)
        {
            //als de plaats op het bord leeg is en het een plaats is waar we een token kunnen plaatsen
            return board.IsEmpty(x, y) && IsValid(p, x, y);
        }

        private int CountLine(Player p, OffsetCalculator calc)
        {
            int offset = 0;

            do
            {
                offset++;
                calc.MakeOffset(offset);
            } while (board.CanOvertakeToken(p, calc.ResultX, calc.ResultY));

            if (!board.IsInBoard(calc.ResultX, calc.ResultY) || board[calc.ResultX, calc.ResultY] != p)
            {
                offset = 0;
            }

            return offset;
        }


        public RevGame Clone()
        {
            return new RevGame()
            {
                board = board.Clone(),
                playerQueue = playerQueue.Clone(),
            };
        }



    }
}
