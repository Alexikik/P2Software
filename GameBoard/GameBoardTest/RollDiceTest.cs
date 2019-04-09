using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameBoard;

namespace GameBoardTest
{
    [TestClass]
    public class RollDiceTest
    {
        [TestMethod]
        public void Test50Outof1000Rolls()
        {
            for (int i = 0; i < 15; i++)
                TestRollDice();
        }

        [TestMethod]
        public void Test50Outof1000RollsOverEndList()
        {
            for (int i = 0; i < 15; i++)
                TestRollDiceOverEndList();
        }
        
        private void TestRollDice()
        {
            // Arrange
            GameManager gameManager = new GameManager(0, true);
            Piece piece = gameManager.players[0].pieces[0];
            gameManager.currentPlayer = piece.player;
            allFields field;
                
            piece.newField(gameManager.Ludo.boardFields[0]);

            // Act
            gameManager.rollDice();
            gameManager.turnEnd(1);

            // Assert
            field = piece.placement;
            switch (gameManager.diceValue)
            {
                case 1:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[1]);
                    break;
                case 2:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[2]);
                    break;
                case 3:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[12]);
                    break;
                case 4:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[4]);
                    break;
                case 5:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[1]);
                    break;
                case 6:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[12]);
                    break;
            }
        }

        private void TestRollDiceOverEndList()
        {
            // Arrange
            GameManager gameManager = new GameManager(0, true);
            Piece piece = gameManager.players[1].pieces[0];
            gameManager.currentPlayer = piece.player;
            allFields field;
            
            piece.newField(gameManager.Ludo.boardFields[51]);

            // Act
            gameManager.rollDice();
            gameManager.turnEnd(1);

            // Assert
            field = piece.placement;
            switch (gameManager.diceValue)
            {
                case 1:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[0]);
                    break;
                case 2:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[1]);
                    break;
                case 3:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[12]);
                    break;
                case 4:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[3]);
                    break;
                case 5:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[1]);
                    break;
                case 6:
                    Assert.AreSame(field, gameManager.Ludo.boardFields[5]);
                    break;
            }
        }
    }
}
