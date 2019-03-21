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
        //public List<Piece> redPieces = new List<Piece>();
        public List<allFields> boardFields = new List<allFields>();
        public List<homeField> allHomeFields = new List<homeField>();
        public List<pathField> pathPlayerGreen = new List<pathField>();
        public List<pathField> pathPlayerRed = new List<pathField>();
        public List<pathField> pathPlayerBlue = new List<pathField>();
        public List<pathField> pathPlayerYellow = new List<pathField>();
        public List<Player> players;
        PictureBox gameBoard;
        public ControlPanel ControlPanel;

        public GameBoard(List<Player> players)
        {
            // Setup
            this.Height = 804;
            this.Width = 1204;
            this.players = players;
            SetupFields();
            SetupAllHomeFields();
            //SetupAllPathFields();
            ControlPanel = new ControlPanel(new Point(766, 0));

            // Ludo image
            gameBoard = new PictureBox();
            gameBoard.SizeMode = PictureBoxSizeMode.StretchImage;    // Streches the image
            gameBoard.Image = Image.FromFile("Images/LudoPlade.png");
            gameBoard.Size = new Size(765, 765);

            /*
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
            //redPieces.Add(new Piece(2, boardFields[1]));
            //redPieces.Add(new Piece(2, allHomeFields[6]));

            // Controls
            Controls.Add(btn);
            Controls.Add(btnBack);

            // Events
            btn.Click += btn_Click;
            btnBack.Click += btnBack_Click;
            */
        }


        private void btn_Click(object sender, EventArgs e)
        {
            movePiece(players[0].pieces[0], 1);

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            movePiece(players[0].pieces[0], -1);
        }

        public void SetupControls()
        {
            //Controls.Add(redPieces[0].piece);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Controls.Add(players[i].pieces[j].piece);
                }
            }

            /*
            Controls.Add(ControlPanel.dicebtn);
            Controls.Add(ControlPanel.dice);
            Controls.Add(ControlPanel.currentPlayer);    
            */

            Controls.Add(ControlPanel);
            Controls.Add(gameBoard);
            
        }


        private void SetupFields()
        {
            int index = 0;
            int offset = 10;

            for (int i = 0; i < 6; i++) // Insert globefield
                boardFields.Add(new normalField(409 + offset, 13 + offset + i * 50, index++));
            boardFields.Add(new starField(458 + offset, 310 + offset, index++));                // green/red star
            boardFields.Add(new normalField(508 + offset, 310 + offset, index++));
            boardFields.Add(new normalField(558 + offset, 310 + offset, index++));
            boardFields.Add(new globeField(608 + offset, 310 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(657 + offset + i * 50, 310 + offset, index++));         
            boardFields.Add(new starField(706 + offset, 360 + offset, index++));         // red star
            boardFields.Add(new normalField(706 + offset, 409 + offset, index++));
            boardFields.Add(new globeField(657 + offset, 409 + offset, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(607 + offset - i * 50, 409 + offset, index++));
            boardFields.Add(new normalField(458 + offset, 409 + offset, index++));
            boardFields.Add(new starField(409 + offset, 458 + offset, index++));              // red/blue star
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(409 + offset, 508 + offset + i * 50, index++));
            boardFields.Add(new globeField(409 + offset, 607 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(409 + offset, 657 + offset + i * 50, index++));
            boardFields.Add(new starField   (360 + offset, 706 + offset, index++));         // blue star
            boardFields.Add(new normalField (310 + offset, 706 + offset, index++));
            boardFields.Add(new globeField  (310 + offset, 656 + offset, index++));        // blue globe
            boardFields.Add(new normalField (310 + offset, 607 + offset, index++));
            boardFields.Add(new normalField (310 + offset, 557 + offset, index++));
            boardFields.Add(new normalField (310 + offset, 508 + offset, index++));
            boardFields.Add(new normalField (310 + offset, 457 + offset, index++));
            boardFields.Add(new starField   (261 + offset, 409 + offset, index++));         //blue/yellow star
            boardFields.Add(new normalField (211 + offset, 409 + offset, index++));
            boardFields.Add(new normalField (162 + offset, 409 + offset, index++));
            boardFields.Add(new globeField  (112 + offset, 409 + offset, index++));         //blue/yellow globe
            boardFields.Add(new normalField (63  + offset, 409 + offset, index++));
            boardFields.Add(new normalField (13  + offset, 409 + offset, index++));
            boardFields.Add(new starField   (13  + offset, 360 + offset, index++));
            boardFields.Add(new normalField (13  + offset, 310 + offset, index++));
            boardFields.Add(new globeField  (63  + offset, 310 + offset, index++));         //yellow globe
            boardFields.Add(new normalField (112 + offset, 310 + offset, index++));
            boardFields.Add(new normalField (162 + offset, 310 + offset, index++));
            boardFields.Add(new normalField (211 + offset, 310 + offset, index++));
            boardFields.Add(new normalField (261 + offset, 310 + offset, index++));
            boardFields.Add(new starField   (310 + offset, 261 + offset, index++));         //yellow/green star
            boardFields.Add(new normalField (310 + offset, 211 + offset, index++));
            boardFields.Add(new normalField (310 + offset, 162 + offset, index++));
            boardFields.Add(new globeField  (310 + offset, 112 + offset, index++));         //yellow/green globe
            boardFields.Add(new normalField (310 + offset, 63  + offset, index++));
            boardFields.Add(new normalField (310 + offset, 13  + offset, index++));
            boardFields.Add(new starField   (360 + offset, 13  + offset, index++));         //green star
            //boardFields.Add(new normalField (409 + offset, 13  + offset, index++));
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
                allHomeFields.Add(new homeField(263 + offset + i * 49, 600 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(263 + offset + i * 49, 650 + offset, index++));
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
                if (boardFields[p.placement.index + moves] is starField && boardFields[p.placement.index + moves] is globeField != true)
                    p.newField(boardFields[nextStar(p)]);
                else
                    p.newField(boardFields[p.placement.index + moves]);
            }
        }

        private int nextStar(Piece p)
        {
            int index = p.placement.index + 2;

            if (index == boardFields.Count)
                return 0;

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
