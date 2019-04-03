using System;
using System.IO;
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
        public PictureBox finishRed, finishBlue, finishGreen, finishYellow;
        public TextBox currentPlayer;
        public TextBox currentStatus;
        public GameManager gameManager;
        public Label turnCount;
        public Label currentPlaytxt;
        public List<Button> btnList;

        public ControlPanel(Point coordinates, GameManager gameManagerX)
        {
            this.Width = 240;
            this.Height = 765;
            this.Location = coordinates;
            this.gameManager = gameManagerX;

            //string filename = @"C:DiceBlank.png";
            //FileInfo fileInfo = new FileInfo(filename);
            //string directoryFullPath = fileInfo.DirectoryName; // contains "C:\MyDirectory"
            //Console.WriteLine(directoryFullPath);
            //dice.Image = Image.FromFile(directoryFullPath + @"\Images\Dice\DiceBlank.png");

            string filename = @"C:DiceBlank.png";
            FileInfo fileInfo = new FileInfo(filename);
            string directoryFullPath = fileInfo.DirectoryName; // contains "C:\MyDirectory"
            Console.WriteLine(directoryFullPath);
            

            dice = new PictureBox();
            dice.SizeMode = PictureBoxSizeMode.StretchImage;
            Console.WriteLine(directoryFullPath + @"\Images\Dice\DiceBlank.png");
            dice.Image = Image.FromFile(directoryFullPath + @"\Images\Dice\DiceBlank.png");
            dice.Size = new Size(120, 120);
            dice.Location = new Point(20, 20);

            //dice = new PictureBox();
            //dice.SizeMode = PictureBoxSizeMode.StretchImage;
            //dice.Image = Image.FromFile("Images/Dice/DiceBlank.png");
            //dice.Size = new Size(120, 120);
            //dice.Location = new Point(20, 20);

            dicebtn = new Button();
            dicebtn.Size = new Size(60, dice.Height);
            dicebtn.Location = new Point(dice.Location.X + dicebtn.Width * 2 + 20, dice.Location.Y);
            dicebtn.Text = "Roll The Dice";

            currentPlayer = new TextBox();
            currentPlayer.Size = new Size((dice.Size.Width + dicebtn.Width + 20), 100);
            currentPlayer.Font = new Font("Arial", 25);
            currentPlayer.Location = new Point(dice.Location.X, dicebtn.Location.Y + dicebtn.Height + 20);
            currentPlayer.Enabled = false;
            currentPlayer.ForeColor = Color.White;
            currentPlayer.TextAlign = HorizontalAlignment.Center;
            currentPlayer.Text = "";

            currentPlaytxt = new Label();
            currentPlaytxt.Location = new Point(dice.Location.X + 50, dicebtn.Location.Y + dicebtn.Height + 30);
            currentPlaytxt.Text = "";
            currentPlaytxt.Size = new Size(100, 30);
            currentPlaytxt.Font = new Font("Arial", 20);
            currentPlaytxt.ForeColor = Color.White;
            currentPlaytxt.BackColor = currentPlayer.BackColor;

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

            finishYellow = new PictureBox();
            finishYellow.SizeMode = PictureBoxSizeMode.StretchImage;
            finishYellow.Image = Image.FromFile("Images/GoalOutlines/yellow.png");
            finishYellow.Size = new Size(97, 97);
            finishYellow.Location = new Point(dice.Location.X, currentStatus.Location.Y + currentStatus.Height + 20);

            finishGreen = new PictureBox();
            finishGreen.SizeMode = PictureBoxSizeMode.StretchImage;
            finishGreen.Image = Image.FromFile("Images/GoalOutlines/green.png");
            finishGreen.Size = new Size(97, 97);
            finishGreen.Location = new Point(dice.Location.X + finishYellow.Width + 6, currentStatus.Location.Y + currentStatus.Height + 20);

            finishBlue = new PictureBox();
            finishBlue.SizeMode = PictureBoxSizeMode.StretchImage;
            finishBlue.Image = Image.FromFile("Images/GoalOutlines/blue.png");
            finishBlue.Size = new Size(97, 97);
            finishBlue.Location = new Point(dice.Location.X, finishYellow.Location.Y + finishYellow.Height + 6);

            finishRed = new PictureBox();
            finishRed.SizeMode = PictureBoxSizeMode.StretchImage;
            finishRed.Image = Image.FromFile("Images/GoalOutlines/red.png");
            finishRed.Size = new Size(97, 97);
            finishRed.Location = new Point(dice.Location.X + finishYellow.Width + 6, finishYellow.Location.Y + finishYellow.Height + 6);

            turnCount = new Label();
            turnCount.Size = new Size(200, 100);
            turnCount.Font = new Font("Arial", 25);
            turnCount.Text = $"Turn: {gameManager.turnCount}";
            turnCount.Location = new Point(dice.Location.X, finishBlue.Location.Y + finishBlue.Height + 5);

            btnList = new List<Button>();
            btnList.Add(piecebtnOne);
            btnList.Add(piecebtnTwo);
            btnList.Add(piecebtnThree);
            btnList.Add(piecebtnFour);

            Controls.Add(currentPlaytxt);
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

            Controls.Add(turnCount);

            piecebtnOne.Click += PiecebtnOne_Click;
            piecebtnTwo.Click += PiecebtnTwo_Click;
            piecebtnThree.Click += PiecebtnThree_Click;
            piecebtnFour.Click += PiecebtnFour_Click;
            dicebtn.Click += Dicebtn_Click;
            currentPlaytxt.TextChanged += CurrentPlaytxt_TextChanged;
        }

        private void CurrentPlaytxt_TextChanged(object sender, EventArgs e)
        {
            currentPlaytxt.Location = new Point(currentPlayer.Location.X + currentPlayer.Width/2 - TextRenderer.MeasureText(currentPlaytxt.Text, currentPlaytxt.Font).Width/2, currentPlayer.Location.Y + 8);
            currentPlaytxt.BackColor = currentPlayer.BackColor;
        }

        private void Dicebtn_Click(object sender, EventArgs e)
        {
            gameManager.rollDice();
        }

        private void PiecebtnOne_Click(object sender, EventArgs e)
        {
            gameManager.turnEnd(1);
        }

        private void PiecebtnTwo_Click(object sender, EventArgs e)
        {
            gameManager.turnEnd(2);
        }

        private void PiecebtnThree_Click(object sender, EventArgs e)
        {
            gameManager.turnEnd(3);
        }

        private void PiecebtnFour_Click(object sender, EventArgs e)
        {
            gameManager.turnEnd(4);
        }
    }
}
