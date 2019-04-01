using System;
using System.Collections.Generic;
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

        public override void takeTurn()
        {
            Console.WriteLine(gameManager.turnCount + ": Helo c:");
            bool notDone = true;

            while (notDone)
            {
                gameManager.rollDice();

                gameManager.turnEnd(calculateBestMove().number + 1);
                // Ændre calculateBestMove() Til at returnere en int. Da hvis ingen brikker kan rykke, så skal 99 returneres med turnEnd();



                //if (gameManager.Ludo.ControlPanel.piecebtnOne.Enabled)
                //{
                //    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                //    $"    Dice: [{gameManager.diceValue}] Piece: 1");
                //    gameManager.turnEnd(1);
                //}
                //else if (gameManager.Ludo.ControlPanel.piecebtnTwo.Enabled)
                //{
                //    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                //    $"    Dice: [{gameManager.diceValue}] Piece: 2");
                //    gameManager.turnEnd(2);
                //}
                //else if (gameManager.Ludo.ControlPanel.piecebtnThree.Enabled)
                //{
                //    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                //    $"    Dice: [{gameManager.diceValue}] Piece: 3");
                //    gameManager.turnEnd(3);
                //}
                //else if (gameManager.Ludo.ControlPanel.piecebtnFour.Enabled)
                //{
                //    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                //    $"    Dice: [{gameManager.diceValue}] Piece: 4");
                //    gameManager.turnEnd(4);
                //}
                
                //else if (gameManager.Ludo.ControlPanel.dicebtn.Enabled)
                //{
                //    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                //    $"    Dice: [{gameManager.diceValue}] CAN NOT MOVE!");
                //    gameManager.rollDice();
                //}

                if (gameManager.currentPlayer.team != team)
                    notDone = false;

            }
        }

        private int canKnockHomePiece(Piece p)
        {
            allFields field = gameManager.Ludo.boardFields[p.placement.index + gameManager.diceValue];
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

        private Piece calculateBestMove()
        {
            int value = int.MinValue;
            int temp;
            Piece bestPieceToMove = null;

            foreach (Piece p in moveablePieces())
            {
                temp = canKnockHomePiece(p);
                if (temp > value)
                {
                    value = temp;
                    bestPieceToMove = p;
                }
            }
            return bestPieceToMove;
        }

        private List<Piece> moveablePieces()
        {
            List<Piece> moveablePieces = pieces;

            if (!gameManager.Ludo.ControlPanel.piecebtnOne.Enabled)
                moveablePieces.Remove(pieces[0]);
            if (!gameManager.Ludo.ControlPanel.piecebtnTwo.Enabled)
                moveablePieces.Remove(pieces[0]);
            if (!gameManager.Ludo.ControlPanel.piecebtnThree.Enabled)
                moveablePieces.Remove(pieces[0]);
            if (!gameManager.Ludo.ControlPanel.piecebtnFour.Enabled)
                moveablePieces.Remove(pieces[0]);

            return moveablePieces;
        }
    }
}
