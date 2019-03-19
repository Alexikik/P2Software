using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{

    public class Player
    {
        public List<Piece> pieces = new List<Piece>();
        int team;   // 1:green, 2:red, 3:blue, 4:yellow

        public Player (int team, GameBoard gameBoard)
        {
            this.team = team;

            int pieceIndex;
            if (team == 1)
                pieceIndex = 0;
            else if (team == 2)
                pieceIndex = 4;
            else if (team == 3)
                pieceIndex = 8;
            else
                pieceIndex = 12;

            for (int i = 0; i < 4; i++)
            {
                pieces.Add(new Piece(team, gameBoard.allHomeFields[pieceIndex++]));
            }
            
        }
    }
}
