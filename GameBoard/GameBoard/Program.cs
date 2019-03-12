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
            Ludo.Height = 1039;
            Ludo.Width = 1400;

            

            //Ludo.movePiece(Ludo.redPieces[0], 1, Ludo.boardFields);

            Application.EnableVisualStyles();
            Application.Run(Ludo);
        }



        class GameBoard : Form
        {
                public List<Piece> redPieces = new List<Piece>();
                public List<allFields> boardFields = new List<allFields>();

            public GameBoard()
            {
                boardFields = SetupFields();

                // Move one field forward button
                Button btn = new Button();
                btn.Size = new Size(60, 30);
                btn.Location = new Point(1200, 200);
                btn.Text = "Next";
                // Move one field back button
                Button btnBack = new Button();
                btnBack.Size = new Size(60, 30);
                btnBack.Location = new Point(1100, 200);
                btnBack.Text = "Back";

                // Ludo image
                PictureBox gameBoard = new PictureBox();
                gameBoard.Image = Image.FromFile(@"C:\Users\Alex\OneDrive - Aalborg Universitet\VisualStudio\Projekt\GameBoard\GameBoard\Images\LudoPlade.png");
                gameBoard.Size = new Size(1000,1000);
                //gameBoard.BackgroundImage = gameBoard.Image;

                // Add one red piece
                redPieces.Add(new Piece(2, boardFields[2]));
                
                /*DrawEllipse();
                Pen myPen = new Pen(Color.Red);
                Graphics formGraphics;
                formGraphics = CreateGraphics();
                formGraphics.DrawEllipse(myPen, new Rectangle(200, 200, 200, 300));
                myPen.Dispose();
                formGraphics.Dispose();*/

                Controls.Add(btn);
                Controls.Add(btnBack);
                Controls.Add(redPieces[0].piece);
                Controls.Add(gameBoard);


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




            private List<allFields> SetupFields()
            {
                List<allFields> Fields = new List<allFields>();
                int index = 0;

                for (int i = 0; i < 5; i++)
                {
                    Fields.Add(new normalField(534 + 32 - 15, 6 + 26 - 15 + i * 62, index++));
                }
                Fields.Add(new normalField(559, 343, index++));
                Fields.Add(new normalField(626, 411, index++));
                for (int i = 0; i < 5; i++)
                {
                    Fields.Add(new normalField(690 + 29 - 15 + i*62, 406+30-15, index++));
                }

                return Fields;
            }

            public void movePiece(Piece p, int moves)
            {
                p.newField(boardFields[p.placement.index + moves]);
            }


            private void DrawEllipse()
            {
                Pen myPen = new Pen(Color.Red);
                Graphics formGraphics;
                formGraphics = CreateGraphics();
                formGraphics.DrawEllipse(myPen, new Rectangle(0, 0, 200, 300));
                myPen.Dispose();
                formGraphics.Dispose();
            }
            /*private void form_paint(PaintEventArgs e)
            {
                int centerX = 200;
                int centerY = 200;
                int radius = 15;

                e.Graphics.DrawEllipse(Pens.Green, centerX, centerY, radius, radius);
            }*/


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