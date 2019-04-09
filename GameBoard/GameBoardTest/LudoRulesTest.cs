using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameBoard;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoardTest
{

    [TestClass]
    public class LudoRulesTest
    {
        [TestMethod]
        public void moveOtherPlayerHome()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[1]);
            gameManager.players[1].pieces[0].newField(gameManager.Ludo.boardFields[2]);
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 1;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(2, gameManager.players[0].pieces[0].placement.index);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, gameManager.Ludo.boardFields[0].GetType());
            Assert.IsInstanceOfType(gameManager.players[1].pieces[0].placement, gameManager.Ludo.allHomeFields[0].GetType());
        }

        [TestMethod]
        public void savedOnGlobus()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[0]);
            gameManager.players[1].pieces[0].newField(gameManager.Ludo.boardFields[1]);
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 1;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(1, gameManager.players[1].pieces[0].placement.index);
            homeField homeField = new homeField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, homeField.GetType());
            globeField globeField = new globeField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[1].pieces[0].placement, globeField.GetType());
        }

        [TestMethod]
        public void jumpOnStar()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0, true);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[0]);
            gameManager.currentPlayer = gameManager.players[0];

            //Act:
            gameManager.diceValue = 6;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(12, gameManager.players[0].pieces[0].placement.index);
            starField starField = new starField(0, 0, 0);
            Assert.IsInstanceOfType(gameManager.players[0].pieces[0].placement, starField.GetType());
        }
    }
}
