using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoard
{
    public class GameManager
    {
        public List<Player> players = new List<Player>();
        public GameBoard Ludo;
        public Player currentPlayer;
        public int diceRollsForCurrentPlayer;
        public Dice dice;
        public int diceValue;
        public bool turnDone;
        public int turnCount;

        public GameManager()
        {
            setupGame();
        }



        private void setupGame()
        {
            Ludo = new GameBoard(players, this);
            dice = new Dice();
            turnCount = 1;

            for (int i = 0; i < 4; i++)
            {
                players.Add(new Player(i + 1, Ludo));
            }

            Ludo.SetupControls();

            currentPlayer = chooseStartingPlayer();
            Ludo.ControlPanel.currentPlayer.Text = currentPlayerString(currentPlayer);

            Ludo.ControlPanel.piecebtnOne.Enabled = false;
            Ludo.ControlPanel.piecebtnTwo.Enabled = false;
            Ludo.ControlPanel.piecebtnThree.Enabled = false;
            Ludo.ControlPanel.piecebtnFour.Enabled = false;
            Ludo.ControlPanel.turnCount.Text = $"Turn: {turnCount}";
        }

        public void playGame()
        {
            Application.EnableVisualStyles();
            Application.Run(Ludo);
        }

        private Player chooseStartingPlayer()
        {
            Random seed = new Random();

            int randValue = seed.Next(4);

            return players[randValue];
        }

        private string currentPlayerString(Player player)
        {
            switch (player.team)
            {
                case 1:
                    return "Green";
                case 2:
                    return "Red";
                case 3:
                    return "Blue";
                case 4:
                    return "Yellow";
                default:
                    return "Error";
            }
        }

        public void turnEnd(int pieceNum)
        {
            if (pieceNum != 99)     // pieceNum is 99 if no pieces can move
                Ludo.movePiece(currentPlayer.pieces[pieceNum - 1], diceValue);

            // Gives the turn to the next player
            if (currentPlayer.team == 4)    // If it's player four, then start with player one
                currentPlayer = players[0];
            else
                currentPlayer = players[currentPlayer.team];
            diceRollsForCurrentPlayer = 0;

            Ludo.ControlPanel.currentPlayer.Text = currentPlayerString(currentPlayer);
            Ludo.ControlPanel.dicebtn.Enabled = true;
            Ludo.ControlPanel.piecebtnOne.Enabled = false;
            Ludo.ControlPanel.piecebtnTwo.Enabled = false;
            Ludo.ControlPanel.piecebtnThree.Enabled = false;
            Ludo.ControlPanel.piecebtnFour.Enabled = false;
            Ludo.ControlPanel.dice.Image = Image.FromFile("Images/Dice/DiceBlank.png");

            turnCount++;
            Ludo.ControlPanel.turnCount.Text = $"Turn: {turnCount}";
        }

        public void rollDice()
        {
            int unmovablePiecesAtHome = 0;
            diceRollsForCurrentPlayer++;

            diceValue = dice.Roll();
            Ludo.ControlPanel.dice.Image = Image.FromFile($"Images/Dice/Dice{diceValue}.png"); // Update dice image

            Ludo.ControlPanel.dicebtn.Enabled = false;
            Ludo.ControlPanel.piecebtnOne.Enabled = true;
            Ludo.ControlPanel.piecebtnTwo.Enabled = true;
            Ludo.ControlPanel.piecebtnThree.Enabled = true;
            Ludo.ControlPanel.piecebtnFour.Enabled = true;

            if ((diceValue == 5 || diceValue == 6) == false) // Disables buttons for pieces that are at home, if the player didn't get a globus
            {
                if (currentPlayer.pieces[0].placement is homeField)
                {
                    Ludo.ControlPanel.piecebtnOne.Enabled = false;
                    unmovablePiecesAtHome++;
                }
                if (currentPlayer.pieces[1].placement is homeField)
                {
                    Ludo.ControlPanel.piecebtnTwo.Enabled = false;
                    unmovablePiecesAtHome++;
                }
                if (currentPlayer.pieces[2].placement is homeField)
                {
                    Ludo.ControlPanel.piecebtnThree.Enabled = false;
                    unmovablePiecesAtHome++;
                }
                if (currentPlayer.pieces[3].placement is homeField)
                {
                    Ludo.ControlPanel.piecebtnFour.Enabled = false;
                    unmovablePiecesAtHome++;
                }
            }

            // If the player has all pieces at home, then the player can roll the dice 3 times in total
            if (unmovablePiecesAtHome == 4 && diceRollsForCurrentPlayer <= 2)     
            {
                Ludo.ControlPanel.dicebtn.Enabled = true;
                Ludo.ControlPanel.piecebtnOne.Enabled = false;
                Ludo.ControlPanel.piecebtnTwo.Enabled = false;
                Ludo.ControlPanel.piecebtnThree.Enabled = false;
                Ludo.ControlPanel.piecebtnFour.Enabled = false;
            }
            else
            {
                if (unmovablePiecesAtHome == 4)
                    turnEnd(99);
            }
        }

    }
}