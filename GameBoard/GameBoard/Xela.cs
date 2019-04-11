using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameBoard
{
    public class Xela : AllPlayers
    {

        public List<AllPlayers> allPlayers;
        GameManager gameManager;
        public List<bool> canMovePiece = new List<bool>();
        public enum Behavior { Aggresive, Passive, Tactical };
        public Behavior myBehavior;

        public int chaser;

        public Xela(int teamIn, GameBoard gameBoard, Behavior behavior) : base(teamIn, gameBoard)
        {
            allPlayers = gameBoard.players;
            gameManager = gameBoard.gameManager;
            myBehavior = behavior;
            for (int i = 0; i < 4; i++)
                canMovePiece.Add(false);
        }


        public async override void takeTurn()
        {
            await Task.Delay(1 * 1);

            if (gameManager.diceRollsForCurrentPlayer == 0)
                Console.WriteLine("\n" + gameManager.turnCount + $": Hello c: [{gameManager.currentPlayerString(gameManager.currentPlayer)}]");
            bool notDone = true;
            int bestPieceToMove;

            while (notDone && !gameManager.gameDone)
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
                    Console.WriteLine($"    {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                       $"    Dice: [{gameManager.diceValue}] DiceRolls: {gameManager.diceRollsForCurrentPlayer} ");   // Prints best move

                    bestPieceToMove = calculateBestMove() + 1;  // Finds best move

                   

                    gameManager.turnEnd(bestPieceToMove);   // Does best move

                    if (gameManager.currentPlayer.team != team)
                        notDone = false;
                }

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
                double scoreP = GetScore(p);
                Console.WriteLine("Piece: " + (p.number + 1) + "..." + " Score: " + scoreP);
                if (scoreP > score)
                {
                    score = scoreP;
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

            switch (myBehavior)
            {
                case Behavior.Aggresive:
                    return Aggressive(p, field);
                case Behavior.Passive:
                    return Aggressive(p, field);
                case Behavior.Tactical:
                    return Aggressive(p, field);
            }

            return 1;
        }

        double Passive(Piece p)
        {
            double points = 0;
            if (p.placement is homeField)
                points += 1;
            else
            {
                Random rnd = new Random();  // The seed for the dice is made here
                points += rnd.Next(10) + 1;
            }

            return points;

        }

        double Aggressive(Piece p, allFields f)
        {
            double points = 0;

            allFields field = f;

            if (field is starField || gameManager.diceValue == 3)
                field = gameManager.Ludo.boardFields[gameManager.Ludo.findNextStar(p, 6, true)];

            if(p.placement is homeField == false )
            {
                if(ChaseChecker(p, field.index, 10) > 0) //Piece is being chased
                {
                    points -= (GetProgress(p, field) * ChaseChecker(p, field.index, 10) * 2 + 20);
                }
                if(CanKill(p, field) > 0)
                {
                    points += CanKill(p, field);
                }
                if(ChaseChecker(p, p.placement.index, 10) > 0)
                {
                    if(p.placement is globeField == false)
                    points += (GetProgress(p, p.placement) * ChaseChecker(p, p.placement.index, 10) * 2 + 20);
                }
                points += MovePoints(p, field);
            }
            else
            {
                points += 30;
            }
           


            return points;

        }

        double MovePoints(Piece p, allFields field)
        {
            double points = 0;
            if (field.index - p.placement.index > 0)
            {
                points += field.index - p.placement.index;
            }
            else
            {
                points += gameManager.Ludo.boardFields.Count + field.index - p.placement.index;
            }

            if (field is globeField)
                points += 10;

            if (field is pathField)
                points += 100;
            return points;
        }

        int GetProgress(Piece p, allFields field)
        {
            int progress = 0;
            if (field.index - gameManager.Ludo.GetHomeField(this) >= 0)
                progress = field.index - gameManager.Ludo.GetHomeField(this);
            else
            {
                progress = gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this);
            }

            return progress;

        }


        double ChaseChecker(Piece p, int fieldId, int fieldsToCheck)
        {
            int field = fieldId;
            int playersInRange = 0;
            double probability = 0;
            for (int i = 1; i <= fieldsToCheck; i++)
            {
                if (field - i < 0)
                {
                    int remainingMoves = ((gameManager.Ludo.boardFields.Count + field) - i);
                    field = gameManager.Ludo.boardFields[remainingMoves].index;
                }
                else
                {
                    field = gameManager.Ludo.boardFields[field - i].index;
                }

                if(gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field]).Count > 0)
                {
                    if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field])[0].player.team != team)
                    {
                        if (gameManager.Ludo.findPiecesAtField(gameManager.Ludo.boardFields[field])[0].player.team != team)
                        playersInRange++;
                    }
                }

            }

            chaser = playersInRange;

            probability = playersInRange;

            return probability;


        }

        double CanKill(Piece p, allFields field)
        {
            double points = 0;

            if(gameManager.Ludo.findPiecesAtField(field).Count > 0)
            {
                if (gameManager.Ludo.findPiecesAtField(field)[0].player.team != team)
                {
                    if (gameManager.Ludo.findPiecesAtField(field).Count < 2)
                    {
                        Piece piece = gameManager.Ludo.findPiecesAtField(field)[0];
                        points += GetProgress(piece, piece.placement);
                    }
                }
            }

            return points;

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