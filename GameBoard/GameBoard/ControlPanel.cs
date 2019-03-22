using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameBoard
{
    public class ControlPanel : UserControl
    {
        public Button dicebtn;
        public Button piecebtnOne, piecebtnTwo, piecebtnThree, piecebtnFour;
        public PictureBox dice;
        public PictureBox finishRed, finishBlue, finishGreen, finishYellow, finishSimon;
        public TextBox currentPlayer;
        public TextBox currentStatus;
        public GameManager gameManager;

        public ControlPanel(Point coordinates, GameManager gameManagerX)
        {
            this.Width = 240;
            this.Height = 765;
            this.Location = coordinates;
            this.gameManager = gameManagerX;

            dice = new PictureBox();
            dice.Image = Image.FromFile("Images/Red.png");
            dice.Size = new Size(120, 120);
            dice.Location = new Point(20, 20);

            dicebtn = new Button();
            dicebtn.Size = new Size(60, dice.Height);
            dicebtn.Location = new Point(dice.Location.X + dicebtn.Width * 2 + 20, dice.Location.Y);
            dicebtn.Text = "Roll The Dice";

            currentPlayer = new TextBox();
            currentPlayer.Size = new Size((dice.Size.Width + dicebtn.Width + 20), 100);
            currentPlayer.Font = new Font("Arial", 25);
            currentPlayer.Location = new Point(dice.Location.X, dicebtn.Location.Y + dicebtn.Height + 20);
            currentPlayer.Enabled = false;
            currentPlayer.TextAlign = HorizontalAlignment.Center;
            currentPlayer.Text = "Thomas Tog";

            piecebtnOne = new Button();
            piecebtnOne.Size = new Size(100, 100);
            piecebtnOne.Text = "Piece 1"; 
            piecebtnOne.Location = new Point(dice.Location.X, currentPlayer.Location.Y + currentPlayer.Height + 20);

            piecebtnTwo = new Button();
            piecebtnTwo.Size = new Size(100, 100);
            piecebtnTwo.Text = "Piece 2";
            piecebtnTwo.Location = new Point(dice.Location.X + piecebtnOne.Width, currentPlayer.Location.Y + currentPlayer.Height + 20);

            piecebtnThree = new Button();
            piecebtnThree.Size = new Size(100, 100);
            piecebtnThree.Text = "Piece 3";
            piecebtnThree.Location = new Point(dice.Location.X, piecebtnOne.Location.Y + piecebtnOne.Height);

            piecebtnFour = new Button();
            piecebtnFour.Size = new Size(100, 100);
            piecebtnFour.Text = "Piece 4";
            piecebtnFour.Location = new Point(dice.Location.X + piecebtnThree.Width, piecebtnOne.Location.Y + piecebtnOne.Height);

            currentStatus = new TextBox();
            currentStatus.Size = new Size((dice.Size.Width + dicebtn.Width + 20), 100);
            currentStatus.Font = new Font("Arial", 25);
            currentStatus.Location = new Point(dice.Location.X, piecebtnFour.Location.Y + piecebtnFour.Height + 20);
            currentStatus.Enabled = false;
            currentStatus.TextAlign = HorizontalAlignment.Center;
            currentStatus.Text = "Finished";

            finishRed = new PictureBox();
            finishRed.Image = Image.FromFile("Images/Red.png");
            finishRed.Size = new Size(100, 100);
            finishRed.Location = new Point(dice.Location.X, currentStatus.Location.Y + currentStatus.Height + 20);

            finishBlue = new PictureBox();
            finishBlue.Image = Image.FromFile("Images/Red.png");
            finishBlue.Size = new Size(100, 100);
            finishBlue.Location = new Point(dice.Location.X + finishRed.Width + 5, currentStatus.Location.Y + currentStatus.Height + 20);

            finishGreen = new PictureBox();
            finishGreen.Image = Image.FromFile("Images/Red.png");
            finishGreen.Size = new Size(100, 100);
            finishGreen.Location = new Point(dice.Location.X, finishRed.Location.Y + finishRed.Height + 5);

            finishYellow = new PictureBox();
            finishYellow.Image = Image.FromFile("Images/Red.png");
            finishYellow.Size = new Size(100, 100);
            finishYellow.Location = new Point(dice.Location.X + finishRed.Width + 5, finishRed.Location.Y + finishRed.Height + 5);

            Controls.Add(dicebtn);
            Controls.Add(dice);
            Controls.Add(currentPlayer);
            Controls.Add(currentStatus);

            Controls.Add(piecebtnOne);
            Controls.Add(piecebtnTwo);
            Controls.Add(piecebtnThree);
            Controls.Add(piecebtnFour);

            Controls.Add(finishRed);
            Controls.Add(finishBlue);
            Controls.Add(finishGreen);
            Controls.Add(finishYellow);

            piecebtnOne.Click += PiecebtnOne_Click;
            piecebtnTwo.Click += PiecebtnTwo_Click;
            piecebtnThree.Click += PiecebtnThree_Click;
            piecebtnFour.Click += PiecebtnFour_Click;
            dicebtn.Click += Dicebtn_Click;
        }

        private void Dicebtn_Click(object sender, EventArgs e)
        {
            gameManager.rollDice();
        }

        private void PiecebtnOne_Click(object sender, EventArgs e)
        {
            gameManager.turnEnd();
        }

        private void PiecebtnTwo_Click(object sender, EventArgs e)
        {
            currentPlayer.Text = this.ActiveControl.Text;
        }

        private void PiecebtnThree_Click(object sender, EventArgs e)
        {
            currentPlayer.Text = this.ActiveControl.Text;
        }

        private void PiecebtnFour_Click(object sender, EventArgs e)
        {
            currentPlayer.Text = this.ActiveControl.Text;
        }
    }
}
