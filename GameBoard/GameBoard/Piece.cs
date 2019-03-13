using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoard
{
    public class Piece
    {
        public int player;  // 1:green, 2: red, 3:blue, 4:yellow
        public allFields placement;

        public PictureBox piece;

        public Piece(int p, allFields f)
        {
            player = p;
            placement = f;

            piece = MakePiece(f);
        }


        private PictureBox MakePiece(allFields field)
        {
            PictureBox Piece = new PictureBox();
            Piece.Image = Image.FromFile(@"C:\Users\Alexi\Documents\GitHub\P2Software\GameBoard\GameBoard\Images\Red.png");
            Piece.Size = new Size(29, 29);
            Piece.Location = new Point(field.x, field.y);

            return Piece;
        }

        public void newField(allFields newPlacement)
        {
            placement = newPlacement;
            piece.Location = new Point(newPlacement.x, newPlacement.y);
        }
    }
}
