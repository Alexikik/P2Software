using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameBoard
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GameBoard Ludo = new GameBoard();

            //Ludo.movePiece(Ludo.redPieces[0], 1, Ludo.boardFields);

            Application.EnableVisualStyles();
            Application.Run(Ludo);
        }
    }
}