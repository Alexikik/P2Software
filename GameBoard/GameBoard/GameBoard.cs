using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoard
{
    public class GameBoard : Form
    {
        public List<Piece> redPieces = new List<Piece>();
        public List<allFields> boardFields = new List<allFields>();
        public List<homeField> allHomeFields = new List<homeField>();
        public List<pathField> pathPlayerGreen = new List<pathField>();
        public List<pathField> pathPlayerRed = new List<pathField>();
        public List<pathField> pathPlayerBlue = new List<pathField>();
        public List<pathField> pathPlayerYellow = new List<pathField>();

        public GameBoard()
        {
            // Setup
            this.Height = 804;
            this.Width = 1204;
            SetupFields();
            SetupAllHomeFields();
            //SetupAllPathFields();

            // Ludo image
            PictureBox gameBoard = new PictureBox();
            gameBoard.SizeMode = PictureBoxSizeMode.StretchImage;    // Streches the image
            gameBoard.Image = Image.FromFile("Images/LudoPlade.png");
            gameBoard.Size = new Size(765, 765);


            // Move one field forward button
            Button btn = new Button();
            btn.Size = new Size(60, 30);
            btn.Location = new Point(1000, 200);
            btn.Text = "Next";

            // Move one field back button
            Button btnBack = new Button();
            btnBack.Size = new Size(60, 30);
            btnBack.Location = new Point(900, 200);
            btnBack.Text = "Back";

            // Add one red piece
            //redPieces.Add(new Piece(2, boardFields[2]));
            redPieces.Add(new Piece(2, allHomeFields[6]));

            // Controls
            Controls.Add(btn);
            Controls.Add(btnBack);
            Controls.Add(redPieces[0].piece);
            Controls.Add(gameBoard);

            // Events
            btn.Click += btn_Click;
            btnBack.Click += btnBack_Click;
        }


        private void btn_Click(object sender, EventArgs e)
        {
            movePiece(redPieces[0], 1);

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            movePiece(redPieces[0], -1);
        }




        private void SetupFields()
        {
            int index = 0;
            int offset = 10;

            for (int i = 0; i < 6; i++)
                boardFields.Add(new normalField(409 + offset, 13 + offset + i * 50, index++));
            boardFields.Add(new starField(458 + offset, 310 + offset, index++));                // green/red star
            boardFields.Add(new normalField(508 + offset, 310 + offset, index++));
            boardFields.Add(new normalField(558 + offset, 310 + offset, index++));
            boardFields.Add(new globeField(608 + offset, 310 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(657 + offset + i * 50, 310 + offset, index++)); // UNTIL HERE IS RIGHT!!!

            boardFields.Add(new normalField(923 + offset, 406 + offset, index++));                
            boardFields.Add(new starField(923 + offset, 406 + offset + 64, index++));         // red star
            boardFields.Add(new normalField(923 + offset, 535 + offset, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(859 + offset - i * 65, 535 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(665 + offset - i * 65, 535 + offset, index++));
            boardFields.Add(new starField(535 + offset, 600 + offset, index++));              // red/blue star
            boardFields.Add(new normalField(535 + offset, 600 + offset + 65, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(535 + offset, 729 + offset + i * 65, index++));
            boardFields.Add(new normalField(535 + offset, 923 + offset, index++));
            boardFields.Add(new starField(535 + offset - 65, 923 + offset, index++));         // blue star
        }

        private void SetupAllHomeFields()
        {
            int index = 0;
            int offset = 10;

            // Green
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(469 + offset + i * 49, 63 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(469 + offset + i * 49, 63+49 + offset, index++));
            // Red
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(608 + offset + i * 49, 469 + offset, index++)); 
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(608 + offset + i * 49, 469+49 + offset, index++));
            // Blue
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(263 + offset + i * 49, 794 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(263 + offset + i * 49, 859 + offset, index++));
            // Yellow
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(82 + offset + i * 49, 262 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(82 + offset + i * 49, 327 + offset, index++));
        }

        private void SetupAllPathFields()
        {
            throw new NotImplementedException();
        }

        public void movePiece(Piece p, int moves)
        {
            if (p.placement.index + moves == boardFields.Count) // Starts back from the start
            {
                p.newField(boardFields[0]);
            }
            else
            {
                if (boardFields[p.placement.index + moves] is starField)
                    p.newField(boardFields[nextStar(p)]);
                else
                    p.newField(boardFields[p.placement.index + moves]);
            }
        }

        private int nextStar(Piece p)
        {
            int index = p.placement.index + 2;

            while (boardFields[index] is starField != true)
                index++;

            return index;
        }

    }
}




// Circle piece
/* PictureBox RedPiece = new PictureBox();
RedPiece.Image = Image.FromFile(@"C:\Users\Alexi\Desktop\RedCircle.png");
RedPiece.SizeMode = PictureBoxSizeMode.StretchImage;    // Streches the image
RedPiece.Size = new Size(30, 30);
RedPiece.Location = new Point(400, 400);
RedPiece.BackgroundImage = pb1.Image;
RedPiece.Parent = pb1;
RedPiece.BackColor = Color.FromArgb(0, 0, 0, 0); */
