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

        public GameManager()
        {
            setupGame();
        }

        public void setupGame()
        {
            Ludo = new GameBoard(players);

            for (int i = 0; i < 4; i++)
            {
                players.Add(new Player(i + 1, Ludo));
            }

            Ludo.SetupControls();

            Application.EnableVisualStyles();
            Application.Run(Ludo);
        }
    }
}
