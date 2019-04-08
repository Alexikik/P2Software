using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBoard
{
    public class GameManager
    {
        public List<AllPlayers> players = new List<AllPlayers>();
        public GameBoard Ludo;
        public AllPlayers currentPlayer;
        public int diceRollsForCurrentPlayer;
        public bool currentPlayerExtraTurn;
        public Dice dice;
        public int diceValue;
        public bool turnDone;
        public int turnCount;
        public bool gameDone;
        public bool endScreenShown;
        bool noMessageBox { get; }
        public bool writeToFile = true;


        public GameManager(int state, bool noMessageBox)
        {
            Ludo = new GameBoard(players, this);
            dice = new Dice();
            turnCount = 1;
            diceRollsForCurrentPlayer = 0;
            currentPlayerExtraTurn = false;
            gameDone = false;
            endScreenShown = false;
            this.noMessageBox = noMessageBox;

            if (state == 0)
                setupGame();
            if (state == 1)
                setupGameWithXela();
        }


        private void setupGame()
        {
            for (int i = 0; i < 4; i++)
            {
                players.Add(new HumanPlayer(i + 1, Ludo));
            }

            Ludo.SetupControls();

            currentPlayer = chooseStartingPlayer();

            Ludo.ControlPanel.piecebtnOne.Enabled = false;
            Ludo.ControlPanel.piecebtnTwo.Enabled = false;
            Ludo.ControlPanel.piecebtnThree.Enabled = false;
            Ludo.ControlPanel.piecebtnFour.Enabled = false;
            Ludo.ControlPanel.turnCount.Text = $"Turn: {turnCount}";
        }

        private void setupGameWithXela()
        {
            players.Add(new Xela(1, Ludo, Xela.Behavior.Passive));
            players.Add(new Xela(2, Ludo, Xela.Behavior.Passive));
            players.Add(new Xela(3, Ludo, Xela.Behavior.Passive));
            players.Add(new Xela(4, Ludo, Xela.Behavior.Aggresive));

            Ludo.SetupControls();

            currentPlayer = chooseStartingPlayer();

            Ludo.ControlPanel.piecebtnOne.Enabled = false;
            Ludo.ControlPanel.piecebtnTwo.Enabled = false;
            Ludo.ControlPanel.piecebtnThree.Enabled = false;
            Ludo.ControlPanel.piecebtnFour.Enabled = false;
            Ludo.ControlPanel.turnCount.Text = $"Turn: {turnCount}";

            if (currentPlayer is Xela)
                giveTurnToXela();
        }

        public void playGame()
        {
            Application.EnableVisualStyles();
            Ludo.ControlPanel.currentPlaytxt.Text = currentPlayerString(currentPlayer);

            #region This region is used for testing
            // One of players pieces starts on path
            //players[0].pieces[0].newField(Ludo.pathPlayerGreen[3]);
            //players[1].pieces[0].newField(Ludo.pathPlayerRed[3]);
            //players[2].pieces[0].newField(Ludo.pathPlayerBlue[3]);
            //players[3].pieces[0].newField(Ludo.pathPlayerYellow[3]);

            // One of players pieces starts right outside of path
            //players[0].pieces[0].newField(Ludo.boardFields[50]);
            //players[1].pieces[0].newField(Ludo.boardFields[11]);
            //players[2].pieces[0].newField(Ludo.boardFields[24]);
            //players[3].pieces[0].newField(Ludo.boardFields[37]);

            // Three of all players pieces start at home
            //for (int i = 1; i < 4; i++)
            //    Ludo.movePieceGoal(players[0].pieces[i]);

            //for (int i = 1; i < 4; i++)
            //    Ludo.movePieceGoal(players[1].pieces[i]);

            //for (int i = 1; i < 4; i++)
            //    Ludo.movePieceGoal(players[2].pieces[i]);

            //for (int i = 1; i < 4; i++)
            //    Ludo.movePieceGoal(players[3].pieces[i]);

            // Testing all players starting right outside of path
            //for (int i = 0; i < 4; i++)
            //    players[0].pieces[i].newField(Ludo.boardFields[50]);
            //for (int i = 0; i < 4; i++)
            //    players[1].pieces[i].newField(Ludo.boardFields[11]);
            //for (int i = 0; i < 4; i++)
            //    players[2].pieces[i].newField(Ludo.boardFields[24]);
            //for (int i = 0; i < 4; i++)
            //    players[3].pieces[i].newField(Ludo.boardFields[37]);
            #endregion This region is used for testing

            Application.Run(Ludo);
            Ludo.Close();
            //Application.Exit();
        }

        private AllPlayers chooseStartingPlayer()
        {
            Random seed = new Random();

            int randValue = seed.Next(4);

            //return players[randValue];
            return players[0];
        }

        public string currentPlayerString(AllPlayers player)
        {
            switch (player.team)
            {
                case 1:
                    Ludo.ControlPanel.currentPlayer.BackColor = Color.FromArgb(65, 147, 73);
                    return "Green";
                case 2:
                    Ludo.ControlPanel.currentPlayer.BackColor = Color.FromArgb(202, 5, 32);
                    return "Red";
                case 3:
                    Ludo.ControlPanel.currentPlayer.BackColor = Color.FromArgb(1, 126, 177);
                    return "Blue";
                case 4:
                    Ludo.ControlPanel.currentPlayer.BackColor = Color.FromArgb(221, 189, 34);
                    return "Yellow";
                default:
                    return "Error";
            }
        }

        public void turnEnd(int pieceNum)
        {
            if (pieceNum != 99)     // pieceNum is 99 if no pieces can move
                Ludo.movePiece(currentPlayer.pieces[pieceNum - 1], diceValue);

            if (currentPlayerExtraTurn && currentPlayer.placement == 0) // If the player has an extra turn
            {
                currentPlayerExtraTurn = false;

                Ludo.ControlPanel.dicebtn.Enabled = true;
                Ludo.ControlPanel.piecebtnOne.Enabled = false;
                Ludo.ControlPanel.piecebtnTwo.Enabled = false;
                Ludo.ControlPanel.piecebtnThree.Enabled = false;
                Ludo.ControlPanel.piecebtnFour.Enabled = false;

                if (players[currentPlayer.team - 1] is Xela)
                    giveTurnToXela();
            }
            else    // If the player doesn't have an extra turn
            {
                // Gives the turn to the next player
                currentPlayer = nextPlayer(currentPlayer);
                
                if (gameDone == false)
                {
                    diceRollsForCurrentPlayer = 0;
                    currentPlayerExtraTurn = false;

                    Ludo.ControlPanel.currentPlaytxt.Text = currentPlayerString(currentPlayer);
                    Ludo.ControlPanel.dicebtn.Enabled = true;
                    Ludo.ControlPanel.piecebtnOne.Enabled = false;
                    Ludo.ControlPanel.piecebtnTwo.Enabled = false;
                    Ludo.ControlPanel.piecebtnThree.Enabled = false;
                    Ludo.ControlPanel.piecebtnFour.Enabled = false;
                    Ludo.ControlPanel.dice.Image = Image.FromFile("Images/Dice/DiceBlank.png");

                    turnCount++;
                    Ludo.ControlPanel.turnCount.Text = $"Turn: {turnCount}";
                    
                    if (players[currentPlayer.team - 1] is Xela)
                        giveTurnToXela();
                }
                else    // If the game is done (All players have gotten their pieces into goal)
                {
                    endScreenShown = true;
                    diceRollsForCurrentPlayer = 0;
                    currentPlayerExtraTurn = false;
                    
                    Ludo.ControlPanel.dicebtn.Enabled = false;
                    Ludo.ControlPanel.piecebtnOne.Enabled = false;
                    Ludo.ControlPanel.piecebtnTwo.Enabled = false;
                    Ludo.ControlPanel.piecebtnThree.Enabled = false;
                    Ludo.ControlPanel.piecebtnFour.Enabled = false;
                    Ludo.ControlPanel.dice.Image = Image.FromFile("Images/Dice/DiceBlank.png");
                    
                    if (!noMessageBox)
                        MessageBox.Show(getGameEndText());
                    Ludo.Close();   // Closes the ludo screen, after pressing OK to the messageBox
                }
            }
        }
        private string getGameEndText()
        {
            AllPlayers p1 = players[1], p2 = players[1], p3 = players[1], p4 = players[1];  // Assignments are only temporary!
            string text;
            bool notDone = true;

            int i = 0;
            while (notDone)
            {
                if (players[i].placement == 1)
                {
                    p1 = players[i];
                    notDone = false;
                }
                else
                    i++;
            }

            notDone = true;
            i = 0;
            while (notDone)
            {
                if (players[i].placement == 2)
                {
                    p2 = players[i];
                    notDone = false;
                }
                else
                    i++;
            }

            notDone = true;
            i = 0;
            while (notDone)
            {
                if (players[i].placement == 3)
                {
                    p3 = players[i];
                    notDone = false;
                }
                else
                    i++;
            }

            notDone = true;
            i = 0;
            while (notDone)
            {
                if (players[i].placement == 4)
                {
                    p4 = players[i];
                    notDone = false;
                }
                else
                    i++;
            }

            text = $"The game is done! \n" +
                $"Congratulations to player {currentPlayerString(p1)} for winning! With Behavior:  \n" +
                $"Player {currentPlayerString(p2)} got second place \n" +
                $"Player {currentPlayerString(p3)} got third place \n" +
                $"Player {currentPlayerString(p4)} got fourth place \n \n" +
                $"Thanks for playing!";

            

            Ludo.ControlPanel.currentPlaytxt.Text = currentPlayerString(p1);    // Sets display to winning player

            return text;
        }

        private AllPlayers nextPlayer(AllPlayers currentPlayer)
        {
            AllPlayers nextPlayer = currentPlayer;
            bool notDone = true;
            
            while (notDone)
            {
                // Gives turn to next player
                if (nextPlayer.team == 4)    // If it's player four, then start with player one
                    nextPlayer = players[0];
                else
                    nextPlayer = players[nextPlayer.team];

                // Checks if the next player is done, if not it's done, if not then repeat
                if (nextPlayer.placement == 0)  
                    notDone = false;
                else if (nextPlayer.team == currentPlayer.team)   // if all players are done
                {
                    notDone = false;
                    gameDone = true;
                }
            }

            return nextPlayer;
        }

        public void rollDice()
        {
            int unmovablePiecesAtHome = 0;
            List<bool> canMovePiece = new List<bool>();
            for (int i = 0; i < 4; i++)
                canMovePiece.Add(true);

            diceRollsForCurrentPlayer++;

            diceValue = dice.Roll();
            Ludo.ControlPanel.dice.Image = Image.FromFile($"Images/Dice/Dice{diceValue}.png"); // Update dice image

            Ludo.ControlPanel.dicebtn.Enabled = false;

            if (currentPlayer is HumanPlayer)   // Enables buttons for humanplayer
            {
                foreach (Button btn in Ludo.ControlPanel.btnList)
                    btn.Enabled = true;
            }
            else    // Disables buttons when Xela plays
            {
                foreach (Button btn in Ludo.ControlPanel.btnList)
                    btn.Enabled = false;
            }

            // Finds the amount of unmovable pieces
            for (int i = 0; i < 4; i++)
            {
                if (currentPlayer.pieces[i].placement is goalField)
                {
                    canMovePiece[i] = false;
                    unmovablePiecesAtHome++;
                }
            }
            
            // Disables buttons for pieces that are at home, if the player didn't get a globus or 6
            if ((diceValue == 5 || diceValue == 6) == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (currentPlayer.pieces[i].placement is homeField)
                    {
                        canMovePiece[i] = false;
                        unmovablePiecesAtHome++;
                    }
                }
            }

            if (currentPlayer is Xela)
            {
                Xela currentXela = (Xela)currentPlayer;

                for (int i = 0; i < 4; i++)
                    currentXela.canMovePiece[i] = canMovePiece[i];
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    Ludo.ControlPanel.btnList[i].Enabled = canMovePiece[i];
            }
            

            // If the player has all pieces at home, then the player can roll the dice 3 times in total
            if (unmovablePiecesAtHome == 4 && diceRollsForCurrentPlayer < 3)     
            {
                if (currentPlayer is HumanPlayer)
                    Ludo.ControlPanel.dicebtn.Enabled = true;
                for (int i = 0; i < 4; i++)
                    Ludo.ControlPanel.btnList[i].Enabled = false;
            }
            else if (diceValue == 5)  // If the player got a globus it gets an extra turn
            {
                currentPlayerExtraTurn = true;
            }
            else    // Ends turn if the player has no possible moves to do
            {
                if (unmovablePiecesAtHome == 4)
                    turnEnd(99);
            }
        }

        private void giveTurnToXela()
        {
            //Console.WriteLine(turnCount + ": " + currentPlayer.team);
            Ludo.ControlPanel.dicebtn.Enabled = false;
            players[currentPlayer.team - 1].takeTurn();
        }
    }
}