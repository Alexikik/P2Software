using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public class Xela : AllPlayers
    {
        public List<AllPlayers> allPlayers;
        GameManager gameManager;
        
        public Xela(int teamIn, GameBoard gameBoard) : base(teamIn, gameBoard)
        {
            allPlayers = gameBoard.players;
            gameManager = gameBoard.gameManager;
        }

        public async override void takeTurn()
        {
            await Task.Delay(1 * 1000);

            Console.WriteLine("\n" + gameManager.turnCount + $": Helo c: [{gameManager.currentPlayerString(this)}]");
            bool notDone = true;
            int bestPieceToMove;

            while (notDone)
            {
                gameManager.rollDice();

                if (gameManager.currentPlayer.team != team)
                    notDone = false;
                else
                {
                    bestPieceToMove = calculateBestMove() + 1;  // Finds best move

                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                        $"    Dice: [{gameManager.diceValue}] Piece: {bestPieceToMove}");   // Prints best move

                    gameManager.turnEnd(bestPieceToMove);   // Does best move

                    if (gameManager.currentPlayer.team != team)
                        notDone = false;
                }

            }
        }

        private int canKnockHomePiece(Piece p)
        {
            allFields field;
            int moves = gameManager.diceValue;
            if (p.placement.index + moves >= gameManager.Ludo.boardFields.Count)
            {
                int remainingMoves = moves - (gameManager.Ludo.boardFields.Count - p.placement.index);
                field = gameManager.Ludo.boardFields[remainingMoves];
            }
            else
                field = gameManager.Ludo.boardFields[p.placement.index + gameManager.diceValue];

            if (field is starField)
                field = gameManager.Ludo.boardFields[gameManager.Ludo.findNextStar(p, gameManager.diceValue)];

            List<Piece> pieces = gameManager.Ludo.findPiecesAtField(field);

            if (pieces.Count != 0 && field is globeField)
            {
                if (pieces[0].player.team == p.player.team)
                    return 0;
                else
                    return -1;
            }

            switch (pieces.Count)
            {
                case 0:
                    return 0;
                case 1:
                    if (pieces[0].player.team == p.player.team)
                        return 0;
                    else
                        return 1;
                case 2:
                case 3:
                case 4:
                    if (pieces[0].player.team == p.player.team)
                        return 0;
                    else
                        return -1;
                default:
                    return 0;
            }
        }

        private int calculateBestMove()
        {
            int value = int.MinValue;
            int temp;
            int bestPieceToMove = 0;
            List<Piece> moveablePiecesList = moveablePieces();

            if (moveablePiecesList.Count == 0)
                return 98;  // This will be incrementet by one so it becomes 99, which is the value for no possible moves.

            foreach (Piece p in moveablePiecesList)
            {
                temp = canKnockHomePiece(p);
                if (temp > value)
                {
                    value = temp;
                    bestPieceToMove = p.number;
                }
            }
            return bestPieceToMove;
        }

        private List<Piece> moveablePieces()
        {
            List<Piece> moveablePieces = new List<Piece>();

            if (gameManager.Ludo.ControlPanel.piecebtnOne.Enabled)
                moveablePieces.Add(pieces[0]);
            if (gameManager.Ludo.ControlPanel.piecebtnTwo.Enabled)
                moveablePieces.Add(pieces[1]);
            if (gameManager.Ludo.ControlPanel.piecebtnThree.Enabled)
                moveablePieces.Add(pieces[2]);
            if (gameManager.Ludo.ControlPanel.piecebtnFour.Enabled)
                moveablePieces.Add(pieces[3]);
            
            return moveablePieces;
        }
    }
}