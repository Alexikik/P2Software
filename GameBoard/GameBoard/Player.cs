using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public abstract class AllPlayers
    {
        public List<Piece> pieces = new List<Piece>();
        public int team;   // 1:green, 2:red, 3:blue, 4:yellow
        public int piecesInGoal;
        public int placement;   // 0: aren't done yet, 1: firstplace, 2: secondplace, 3: thirdplace, 4: fourthplace

        public AllPlayers(int teamIn, GameBoard gameBoard)
        {
            team = teamIn;
            piecesInGoal = 0;
            placement = 0;

            int pieceIndex = 0;
            switch (team)
            {
                case 1:
                    pieceIndex = 0;
                    break;
                case 2:
                    pieceIndex = 4;
                    break;
                case 3:
                    pieceIndex = 8;
                    break;
                case 4:
                    pieceIndex = 12;
                    break;
            }

            for (int i = 0; i < 4; i++)
                pieces.Add(new Piece(this, i, gameBoard.allHomeFields[pieceIndex++]));
        }

        public abstract void takeTurn();
    }


    public class HumanPlayer : AllPlayers
    {
        public HumanPlayer(int teamIn, GameBoard gameBoard) : base(teamIn, gameBoard)
        {
        }

        public override void takeTurn()
        { }
    }
}
