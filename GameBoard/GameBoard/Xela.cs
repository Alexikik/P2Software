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
        public List<bool> canMovePiece = new List<bool>();
        public enum Behavior { Aggresive, Passive, Tactical };
        public Behavior myBehavior;

        public Xela(int teamIn, GameBoard gameBoard, Behavior behavior) : base(teamIn, gameBoard)
        {
            allPlayers = gameBoard.players;
            gameManager = gameBoard.gameManager;
            myBehavior = behavior;
            for (int i = 0; i < 4; i++)
                canMovePiece.Add(false);
        }


        public override void takeTurn()
        {

            if (gameManager.diceRollsForCurrentPlayer == 0)
                Console.WriteLine("\n" + gameManager.turnCount + $": Hello c: [{gameManager.currentPlayerString(gameManager.currentPlayer)}]");
            bool notDone = true;
            int bestPieceToMove;

            while (notDone)
            {
                gameManager.rollDice();
                // If the player has all it's pieces at home it gets 3 tries in total
                while (moveablePieces().Count == 0 && gameManager.diceRollsForCurrentPlayer < 3)
                {
                    Console.WriteLine($"    Dice: [{gameManager.diceValue}] Piece: n/a DiceRolls: {gameManager.diceRollsForCurrentPlayer}");
                    gameManager.rollDice();
                }

                if (gameManager.currentPlayer.team != team)
                    notDone = false;
                else
                {
                    bestPieceToMove = calculateBestMove() + 1;  // Finds best move

                    Console.WriteLine($"    {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                        $"    Dice: [{gameManager.diceValue}] Piece: {bestPieceToMove} DiceRolls: {gameManager.diceRollsForCurrentPlayer} Behavior: {myBehavior}");   // Prints best move

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
                field = gameManager.Ludo.boardFields[gameManager.Ludo.findNextStar(p, gameManager.diceValue, true)];

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
            int bestPieceToMove = 0;
            double score = float.MinValue;
            List<Piece> moveablePiecesList = moveablePieces();

            if (moveablePiecesList.Count == 0)
                return 98;  // This will be incrementet by one so it becomes 99, which is the value for no possible moves.

            foreach (Piece p in moveablePiecesList)
            {
                Console.WriteLine("Score" + GetScore(p) + " : " + p.number);
                if (GetScore(p) > score)
                {
                    score = GetScore(p);
                    bestPieceToMove = p.number;
                }
            }
            return bestPieceToMove;
        }

        private double GetScore(Piece p)
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
            
            switch(myBehavior)
            {
                case Behavior.Aggresive:
                    return BehaviorWeight(p, field, 0, 0, 5, 0, 0);
                case Behavior.Passive:
                    return BehaviorWeight(p, field, 0, 0, 0, 0, 0);
                case Behavior.Tactical:
                    return BehaviorWeight(p, field, 20, 0, 0, 0, 100);
            }

            return 1;
        }

        double BehaviorWeight(Piece p, allFields f, double getOutWeight, double killWeight, double starWeight, double groupWeight, double evadeWeight)
        {

            double points = 1;

            if(f.index > gameManager.Ludo.boardFields.Count)
            {
                points = p.placement.index;
            }

            return points;

        }

        bool ChaseChecker(Piece p, int fieldId, int fieldsToCheck)
        {
            int field = fieldId;
            for (int i = 1; i < fieldsToCheck; i++)
            {
                if (field - i < 0)
                {
                    int remainingMoves = ((gameManager.Ludo.boardFields.Count + field) - i);
                    field = gameManager.Ludo.boardFields[remainingMoves].index;
                    if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field]).Count > 0)
                    {
                        if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field])[0].player.team != team)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    field = gameManager.Ludo.boardFields[field - i].index;
                    if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field]).Count > 0)
                    {
                        if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field])[0].player.team != team)
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
        }

        private List<Piece> moveablePieces()
        {
            List<Piece> moveablePieces = new List<Piece>();

            for (int i = 0; i < 4; i++)
            {
                if (canMovePiece[i])
                    moveablePieces.Add(pieces[i]);
            }

            return moveablePieces;
        }
    }
}