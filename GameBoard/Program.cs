using System;
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
            GameManager LudoManager = new GameManager();
            LudoManager.playGame();             
        }
    }
}