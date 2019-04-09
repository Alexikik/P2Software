using System;
using GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameBoardTest
{
    [TestClass]
    public class PathsTest
    {
        [TestMethod]
        public void GreenGoingOnPath()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[48]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 6;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(2, gameManager.players[0].pieces[0].placement.index);
            pathField pathField = new pathField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, pathField.GetType());
        }

        [TestMethod]
        public void GreenGoingOnPathWitGlobus()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[48]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 5;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[0].pieces[0].placement.index);
            pathField pathField = new pathField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, pathField.GetType());
        }
        [TestMethod]
        public void GreenGoingOnPathWithStar()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[48]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 3;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[0].pieces[0].placement.index);
            goalField goalField = new goalField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, goalField.GetType());
        }

        [TestMethod]
        public void GreenGoingOnPathWithStarWithEnemyOnPassedStar()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[48]);    // Fourth last field in boardFields
            gameManager.players[1].pieces[0].newField(gameManager.Ludo.boardFields[50]);    // Places enemy on last star
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 3;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(0, gameManager.players[0].pieces[0].placement.index);
            goalField goalField = new goalField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, goalField.GetType());

            Assert.AreEqual(50, gameManager.players[1].pieces[0].placement.index);  // Checks that the enemy didn't get send home
        }

        [TestMethod]
        public void RedGoingOnPath()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 1;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.boardFields[9]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 6;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(2, gameManager.players[player].pieces[0].placement.index);
            pathField pathField = new pathField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, pathField.GetType());
        }

        [TestMethod]
        public void BlueGoingOnPath()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 2;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.boardFields[22]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 6;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(2, gameManager.players[player].pieces[0].placement.index);
            pathField pathField = new pathField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, pathField.GetType());
        }

        [TestMethod]
        public void YellowGoingOnPath()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            int player = 3;
            gameManager.players[player].pieces[0].newField(gameManager.Ludo.boardFields[35]);    // Three fields outside of path entry
            gameManager.currentPlayer = gameManager.players[player];

            //Act:
            gameManager.diceValue = 6;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(2, gameManager.players[player].pieces[0].placement.index);
            pathField pathField = new pathField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[player].pieces[0].placement, pathField.GetType());
        }

    }
}
