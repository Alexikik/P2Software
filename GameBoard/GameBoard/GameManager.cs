using System;
using System.Collections.Generic;
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
        public Dice dice;
        public int diceValue;
        public bool turnDone;

        public GameManager()
        {
            setupGame();
        }



        private void setupGame()
        {
            Ludo = new GameBoard(players, this);
            dice = new Dice();

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
                    break;
                case 2:
                    return "Red";
                    break;
                case 3:
                    return "Blue";
                    break;
                case 4:
                    return "Yellow";
                    break;
                default:
                    return "Error";
            }
        }

        public void turnEnd()
        {
            Ludo.ControlPanel.piecebtnOne.Enabled = false;
            Ludo.ControlPanel.piecebtnTwo.Enabled = false;
            Ludo.ControlPanel.piecebtnThree.Enabled = false;
            Ludo.ControlPanel.piecebtnFour.Enabled = false;

            if (currentPlayer.team == 4)
                currentPlayer = players[0];
            else
                currentPlayer = players[currentPlayer.team];

            Ludo.ControlPanel.currentPlayer.Text = currentPlayerString(currentPlayer);
            Ludo.ControlPanel.dicebtn.Enabled = true;
        }

        public void rollDice()
        {
            diceValue = dice.Roll();

            Ludo.ControlPanel.dicebtn.Enabled = false;
            Ludo.ControlPanel.piecebtnOne.Enabled = true;
            Ludo.ControlPanel.piecebtnTwo.Enabled = true;
            Ludo.ControlPanel.piecebtnThree.Enabled = true;
            Ludo.ControlPanel.piecebtnFour.Enabled = true;
        }
    }
}
