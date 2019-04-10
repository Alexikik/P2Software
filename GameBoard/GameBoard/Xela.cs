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

            switch (myBehavior)
            {
                case Behavior.Aggresive:
                    return Aggressive(p, field);
                case Behavior.Passive:
                    return Passive(p, field);
                case Behavior.Tactical:
                    return Aggressive(p, field);
            }

            return 1;
        }

        double Aggressive(Piece p, allFields f)
        {
            double points = 0;

            allFields field = f;

            if (p.placement is homeField == false)
            {

                if (gameManager.diceValue == 3 || field is starField)
                {
                    field = gameManager.Ludo.boardFields[gameManager.Ludo.findNextStar(p, 6, true)];
                }

                if (field.index - p.placement.index > 0)
                {
                    points = field.index - p.placement.index;
                }
                else
                {
                    points = gameManager.Ludo.boardFields.Count + field.index - p.placement.index;
                }

                if(f is goalField)
                {
                    if(p.placement is goalField)
                    {
                        points = 1;
                    }
                    else
                    {
                        if (ChaseChecker(p, field.index, 6))
                        {
                            if (p.placement.index - gameManager.Ludo.GetHomeField(this) > 0)
                            {
                                points += p.placement.index - gameManager.Ludo.GetHomeField(this);
                                points = points * 0.3f;
                            }
                            else
                            {
                                points += (gameManager.Ludo.boardFields.Count + p.placement.index - gameManager.Ludo.GetHomeField(this));
                                points = points * 0.3f;
                            }
                        }
                    }
                }

                if (gameManager.Ludo.findPiecesAtField(field).Count > 0)
                {
                    if (field.team != team)
                    {
                        AllPlayers player = gameManager.Ludo.findPiecesAtField(field)[0].player;
                        if (field.index - gameManager.Ludo.GetHomeField(player) > 0)
                        {
                            points = field.index - gameManager.Ludo.GetHomeField(player);
                        }
                        else
                        {
                            points = (gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(player) / 1);
                        }
                        Console.WriteLine(gameManager.currentPlayerString(this) + "Knocked Home: " + gameManager.currentPlayerString(player));
                        //points = 25;
                    }
                }

                if(ChaseChecker(p, field.index, 6))
                {
                    if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                    {
                        points -= field.index - gameManager.Ludo.GetHomeField(this);
                        points = points * 0.3f;
                    }
                    else
                    {
                        points -= (gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this));
                        points = points * 0.3f;
                    }
                }

                if (ChaseChecker(p, p.placement.index, 6))
                {
                    if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                    {
                        points = field.index - gameManager.Ludo.GetHomeField(this);
                        points = points * 0.3f;
                    }
                    else
                    {
                        points = (gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this));
                    }
                }

                if (p.placement is starField)
                {
                    if(ChaseChecker(p, (gameManager.Ludo.findNextStar(p, 0, false)), 6))
                    {
                        if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                        {
                            points = field.index - gameManager.Ludo.GetHomeField(this);
                            points = points * 0.3f;
                        }
                        else
                        {
                            points = gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this);
                            points = points * 0.3f;
                        }
                    }
                }

            }

            else
            {
                if (gameManager.diceValue == 6 || gameManager.diceValue == 5)
                {
                    points = 10;
                }
            }

            return points;

        }

        double Passive(Piece p, allFields f)
        {
            double points = 0;

            allFields field = f;

            if (p.placement is homeField == false)
            {

                if (gameManager.diceValue == 3 || field is starField)
                {
                    field = gameManager.Ludo.boardFields[gameManager.Ludo.findNextStar(p, 6, true)];
                }

                if (field.index - p.placement.index > 0)
                {
                    points = field.index - p.placement.index;
                }
                else
                {
                    points = gameManager.Ludo.boardFields.Count + field.index - p.placement.index;
                }

                if (f is goalField)
                {
                    if (p.placement is goalField)
                    {
                        points = 1;
                    }
                    else
                    {
                        if (ChaseChecker(p, field.index, 6))
                        {
                            if (p.placement.index - gameManager.Ludo.GetHomeField(this) > 0)
                            {
                                points += p.placement.index - gameManager.Ludo.GetHomeField(this);
                            }
                            else
                            {
                                points += (gameManager.Ludo.boardFields.Count + p.placement.index - gameManager.Ludo.GetHomeField(this));
                            }
                        }
                    }
                }

                if (gameManager.Ludo.findPiecesAtField(field).Count > 0)
                {
                    if (field.team != team)
                    {
                        AllPlayers player = gameManager.Ludo.findPiecesAtField(field)[0].player;
                        if (field.index - gameManager.Ludo.GetHomeField(player) > 0)
                        {
                            points = (field.index - gameManager.Ludo.GetHomeField(player)/3);
                        }
                        else
                        {
                            points = ((gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(player))/3);
                        }
                        Console.WriteLine(gameManager.currentPlayerString(this) + "Knocked Home: " + gameManager.currentPlayerString(player));
                        //points = 25;
                    }
                }

                if (ChaseChecker(p, field.index, 6))
                {
                    if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                    {
                        points -= field.index - gameManager.Ludo.GetHomeField(this);
                        points = points * 1;
                    }
                    else
                    {
                        points -= (gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this));
                        points = points * 1;
                    }
                }

                if (ChaseChecker(p, p.placement.index, 6))
                {
                    if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                    {
                        points = field.index - gameManager.Ludo.GetHomeField(this);
                        points = points * 1;
                    }
                    else
                    {
                        points = (gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this));
                    }
                }

                if (p.placement is starField)
                {
                    if (ChaseChecker(p, (gameManager.Ludo.findNextStar(p, 0, false)), 6))
                    {
                        if (field.index - gameManager.Ludo.GetHomeField(this) > 0)
                        {
                            points = field.index - gameManager.Ludo.GetHomeField(this);
                            points = points * 1;
                        }
                        else
                        {
                            points = gameManager.Ludo.boardFields.Count + field.index - gameManager.Ludo.GetHomeField(this);
                            points = points * 1;
                        }
                    }
                }

            }

            else
            {
                if (gameManager.diceValue == 6 || gameManager.diceValue == 5)
                {
                    points = 10;
                }
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