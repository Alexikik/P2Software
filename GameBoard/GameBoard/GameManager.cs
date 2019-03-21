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
        public int diceValue;

        public GameManager()
        {
            setupGame();
        }



        private void setupGame()
        {
            Ludo = new GameBoard(players);

            for (int i = 0; i < 4; i++)
            {
                players.Add(new Player(i + 1, Ludo));
            }

            Ludo.SetupControls();

            currentPlayer = chooseStartingPlayer();
            playGame();

            Application.EnableVisualStyles();
            Application.Run(Ludo);
        }

        private Player chooseStartingPlayer()
        {
            Random seed = new Random();

            int randValue = seed.Next(4);

            return players[randValue];
        }

        public void playGame()
        {
            Ludo.ControlPanel.currentPlayer.Text = currentPlayerString(currentPlayer);
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
    }
}
