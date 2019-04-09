using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameBoard
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GameRecord gameRecord = new GameRecord();

            for (int i = 1; i <= 3; i++)    // i amount of games will be played
            {
                // Change this to true, if you want more games to be played!
                GameManager LudoManager = new GameManager(1, false);    // Change this to true, if you want more games to be played!
                // Change this to true, if you want more games to be played!
                LudoManager.playGame();

                gameRecord.addGame(LudoManager.players[0].placement, LudoManager.players[1].placement, LudoManager.players[2].placement, LudoManager.players[3].placement);
                Console.WriteLine("\n\nGame done!" + i);
            }


            Console.WriteLine(gameRecord.ToString());
            Console.ReadKey();
        }
    }
}



// Change this to true, if you want more games to be played!
// Change this to true, if you want more games to be played!
// Change this to true, if you want more games to be played!
// Change this to true, if you want more games to be played!
// Change this to true, if you want more games to be played!
// Change this to true, if you want more games to be played!

// Helo Thomas I'm Xela, how are you? c: