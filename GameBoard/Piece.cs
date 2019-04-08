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
        public AllPlayers player;  // 1:green, 2:red, 3:blue, 4:yellow
        public allFields placement;
        public int number;          // Zero based

        public PictureBox picture;

        public Piece(AllPlayers player, int number, allFields placement)
        {
            this.player = player;
            this.placement = placement;
            this.number = number;

            picture = MakePiece(placement, number);
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
            picture.Location = new Point(newPlacement.x, newPlacement.y);
        }

        private string playerString()
        {
            switch (player.team)
            {
                case 1:
                    return "green";
                case 2:
                    return "red";
                case 3:
                    return "blue";
                case 4:
                    return "yellow";
                default:
                    return "error";
            }
        }
    }
}
