﻿using System;
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
            Console.WriteLine(gameManager.turnCount + ": Helo");
            bool notDone = true;

            while (notDone)
            {
                gameManager.rollDice();

                if (gameManager.Ludo.ControlPanel.piecebtnOne.Enabled)
                {
                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                    $"    Dice: [{gameManager.diceValue}] Piece: 1");
                    gameManager.turnEnd(1);
                }
                else if (gameManager.Ludo.ControlPanel.piecebtnTwo.Enabled)
                {
                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                    $"    Dice: [{gameManager.diceValue}] Piece: 2");
                    gameManager.turnEnd(2);
                }
                else if (gameManager.Ludo.ControlPanel.piecebtnThree.Enabled)
                {
                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                    $"    Dice: [{gameManager.diceValue}] Piece: 3");
                    gameManager.turnEnd(3);
                }
                else if (gameManager.Ludo.ControlPanel.piecebtnFour.Enabled)
                {
                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                    $"    Dice: [{gameManager.diceValue}] Piece: 4");
                    gameManager.turnEnd(4);
                }
                else if (gameManager.Ludo.ControlPanel.dicebtn.Enabled)
                {
                    Console.WriteLine($"[{gameManager.turnCount}] {gameManager.currentPlayerString(gameManager.currentPlayer)} \n" +
                    $"    Dice: [{gameManager.diceValue}] CAN NOT MOVE!");
                    gameManager.rollDice();
                }
                


                if (gameManager.currentPlayer.team != team)
                    notDone = false;

            }
        }
    }
}
