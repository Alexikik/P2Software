using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    class Xela : Player
    {
        public List<Player> allPlayers;
        
        public Xela(int teamIn, GameBoard gameBoard) : base(teamIn, gameBoard)
        {
            allPlayers = gameBoard.players;
        }

    }
}
