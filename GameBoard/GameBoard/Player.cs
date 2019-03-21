﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{

    public class Player
    {
        public List<Piece> pieces = new List<Piece>();
        public int team;   // 1:green, 2:red, 3:blue, 4:yellow

        public Player (int teamIn, GameBoard gameBoard)
        {
            team = teamIn;

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
                pieces.Add(new Piece(team, gameBoard.allHomeFields[pieceIndex++]));
            
        }
    }
}