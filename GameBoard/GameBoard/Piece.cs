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
        public int player;  // 1:green, 2:red, 3:blue, 4:yellow
        public allFields placement;

        public PictureBox piece;

        public Piece(int p, int nr, allFields f)
        {
            player = p;
            placement = f;

            piece = MakePiece(f, nr);
        }


        private PictureBox MakePiece(allFields field, int nr)
        {
            PictureBox Piece = new PictureBox();
            Piece.SizeMode = PictureBoxSizeMode.StretchImage;
            Piece.Image = Image.FromFile($"Images/Pieces/{playerString()}{nr + 1}.png");
            Piece.Size = new Size(26, 26);
            Piece.Location = new Point(field.x, field.y);

            return Piece;
        }

        public void newField(allFields newPlacement)
        {
            placement = newPlacement;
            piece.Location = new Point(newPlacement.x, newPlacement.y);
        }

        private string playerString()
        {
            switch (player)
            {
                case 1:
                    return "green";
                    break;
                case 2:
                    return "red";
                    break;
                case 3:
                    return "blue";
                    break;
                case 4:
                    return "yellow";
                    break;
                default:
                    return "error";
            }
        }
    }
}
