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
    public class PieceMovementTest
    {
        [TestMethod]
        public void allTurnEnd()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    TurnEnd(j, i, 1);
                    TurnEnd(j, i, 2);
                    TurnEnd(j, i, 4);
                }
            }
        }

        [TestMethod]
        private void TurnEnd(int playerNum, int pieceNum, int diceValue)
        {
            //Arrange:
            GameManager gameManager = new GameManager(0);
            gameManager.players[playerNum].pieces[pieceNum].newField(gameManager.Ludo.boardFields[0]);
            gameManager.diceValue = diceValue;
            int newFieldIndex = diceValue + gameManager.players[playerNum].pieces[pieceNum].placement.index;
            gameManager.currentPlayer = gameManager.players[playerNum];

            //Act:
            gameManager.turnEnd(pieceNum+1);


            // Assert:
            Assert.AreEqual(newFieldIndex, gameManager.players[playerNum].pieces[pieceNum].placement.index);
        }

        [TestMethod]
        public void TurnEndDiceValue1()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[2]);

            //Act:
            gameManager.diceValue = 1;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(3, gameManager.players[0].pieces[0].placement.index);
        }

        [TestMethod]
        public void diceValu3Test()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[0]);

            //Act:
            gameManager.diceValue = 3;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(12, gameManager.players[0].pieces[0].placement.index);
        }

        [TestMethod]
        public void diceValue5Test()
        {
            //Arrange:
            GameManager gameManager = new GameManager(0);
            gameManager.players[0].pieces[0].newField(gameManager.Ludo.boardFields[0]);

            //Act:
            gameManager.diceValue = 5;
            gameManager.turnEnd(1);

            // Assert:
            Assert.AreEqual(1, gameManager.players[0].pieces[0].placement.index);
        }
    }
}
