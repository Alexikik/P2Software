using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameBoard;

namespace GameBoardTest
{
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void TestsBasicRoll()
        {
            // Arrange
            GameBoard.Dice dice = new Dice();
            int diceValue;

            // Act
            diceValue = dice.Roll();

            // Assert
            Assert.AreEqual(3.5, diceValue, 3);
        }

        [TestMethod]
        public void Test50Outof1000Rolls()
        {
            // Arrange
            GameBoard.Dice dice = new Dice();
            int diceValue;
            List<int> diceValues = new List<int>();
            for (int i = 0; i < 6; i++)
                diceValues.Add(0);

            // Act
            for (int i = 0; i < 1000; i++)
            {
                diceValue = dice.Roll();
                diceValues[diceValue - 1] += 1;
            }

            // Assert
            for (int i = 0; i < 6; i++)
            {
                Assert.IsTrue(diceValues[i] > 100);
            }
        }
    }
}
