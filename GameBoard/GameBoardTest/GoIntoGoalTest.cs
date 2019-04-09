using System;
using GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameBoardTest
{
    [TestClass]
    public class GoIntoGoalTest
    {
        [TestMethod]
        public void NormalMovementIntoGoal()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 0;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.pathPlayerGreen[3]);    // Second last field on path
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 2;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[player].pieces[0].placement.index);
            goalField goalField = new goalField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, goalField.GetType());
        }

        [TestMethod]
        public void StarIntoGoal()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 0;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.pathPlayerGreen[3]);    // Second last field on path
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 3;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[player].pieces[0].placement.index);
            goalField goalField = new goalField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, goalField.GetType());
        }

        [TestMethod]
        public void GlobusIntoGoal()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 0;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.pathPlayerGreen[3]);    // Second last field on path
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 5;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[player].pieces[0].placement.index);
            goalField goalField = new goalField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, goalField.GetType());
        }
    }
}
