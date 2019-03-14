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
            this.Height = 839;
            this.Width = 1200;
            SetupFields();
            SetupAllHomeFields();
            //SetupAllPathFields();

            // Ludo image
            PictureBox gameBoard = new PictureBox();
            gameBoard.SizeMode = PictureBoxSizeMode.StretchImage;    // Streches the image
            gameBoard.Image = Image.FromFile("Images/LudoPlade.png");
            gameBoard.Size = new Size(800, 800);


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
            redPieces.Add(new Piece(2, boardFields[2]));
            //redPieces.Add(new Piece(2, allHomeFields[3]));

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

            for (int i = 0; i < 6; i++)
                boardFields.Add(new normalField(535 + 15, 18 + 15 + i * 64, index++));
            boardFields.Add(new starField(600 + 15, 406 + 15, index++));                // green/red star
            boardFields.Add(new normalField(600 + 15 + 65, 406 + 15, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(730 + 15 + i * 65, 406 + 15, index++));
            boardFields.Add(new normalField(923 + 15, 406 + 15, index++));                
            boardFields.Add(new starField(923 + 15, 406 + 15 + 64, index++));         // red star
            boardFields.Add(new normalField(923 + 15, 535 + 15, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(859 + 15 - i * 65, 535 + 15, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(665 + 15 - i * 65, 535 + 15, index++));
            boardFields.Add(new starField(535 + 15, 600 + 15, index++));              // red/blue star
            boardFields.Add(new normalField(535 + 15, 600 + 15 + 65, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(535 + 15, 729 + 15 + i * 65, index++));
            boardFields.Add(new normalField(535 + 15, 923 + 15, index++));
            boardFields.Add(new starField(535 + 15 - 65, 923 + 15, index++));         // blue star
        }

        private void SetupAllHomeFields()
        {
            int index = 0;

            // Green
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(613 + 15 + i * 65, 82 + 15, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(613 + 15 + i * 65, 147 + 15, index++));
            // Red
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(794 + 15 + i * 65, 613 + 15, index++)); 
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(794 + 15 + i * 65, 678 + 15, index++));
            // Blue
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(263 + 15 + i * 65, 794 + 15, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(263 + 15 + i * 65, 859 + 15, index++));
            // Yellow
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(82 + 15 + i * 65, 262 + 15, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(82 + 15 + i * 65, 327 + 15, index++));
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
