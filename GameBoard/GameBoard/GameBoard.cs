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
        public List<allFields> boardFields = new List<allFields>();
        public List<homeField> allHomeFields = new List<homeField>();
        public List<pathField> pathPlayerGreen = new List<pathField>();
        public List<pathField> pathPlayerRed = new List<pathField>();
        public List<pathField> pathPlayerBlue = new List<pathField>();
        public List<pathField> pathPlayerYellow = new List<pathField>();
        public List<Player> players;
        GameManager gameManager;
        PictureBox gameBoard;
        public ControlPanel ControlPanel;

        public GameBoard(List<Player> players, GameManager gameManagerX)
        {
            // Setup
            this.Height = 804;
            this.Width = 1020;
            this.players = players;
            this.gameManager = gameManagerX;
            SetupFields();
            SetupAllHomeFields();
            //SetupAllPathFields();
            ControlPanel = new ControlPanel(new Point(766, 0), gameManager);

            // Ludo image
            gameBoard = new PictureBox();
            gameBoard.SizeMode = PictureBoxSizeMode.StretchImage;    // Streches the image
            gameBoard.Image = Image.FromFile("Images/LudoPlade.png");
            gameBoard.Size = new Size(765, 765);
        }
        

        public void SetupControls()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Controls.Add(players[i].pieces[j].picture);
                }
            }

            Controls.Add(ControlPanel);
            Controls.Add(gameBoard);
        }


        private void SetupFields()
        {
            int index = 0;
            int offset = 10;

            boardFields.Add(new normalField(409 + offset, 13 + offset, index++));
            boardFields.Add(new globeField(409 + offset, 63 + offset, index++));            // green globe
            for (int i = 0; i < 4; i++) 
                boardFields.Add(new normalField(409 + offset, 113 + offset + i * 50, index++));
            boardFields.Add(new starField(458 + offset, 310 + offset, index++));                // green/red star
            boardFields.Add(new normalField(508 + offset, 310 + offset, index++));
            boardFields.Add(new normalField(558 + offset, 310 + offset, index++));
            boardFields.Add(new globeField(608 + offset, 310 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(657 + offset + i * 50, 310 + offset, index++));         
            boardFields.Add(new starField(706 + offset, 360 + offset, index++));               // red star
            boardFields.Add(new normalField(706 + offset, 409 + offset, index++));
            boardFields.Add(new globeField(657 + offset, 409 + offset, index++));
            for (int i = 0; i < 3; i++)
                boardFields.Add(new normalField(607 + offset - i * 50, 409 + offset, index++));
            boardFields.Add(new normalField(458 + offset, 409 + offset, index++));
            boardFields.Add(new starField(409 + offset, 458 + offset, index++));                // red/blue star
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(409 + offset, 508 + offset + i * 50, index++));
            boardFields.Add(new globeField(409 + offset, 607 + offset, index++));
            for (int i = 0; i < 2; i++)
                boardFields.Add(new normalField(409 + offset, 657 + offset + i * 50, index++));
            boardFields.Add(new starField   (360 + offset, 706 + offset, index++));         // blue star
            boardFields.Add(new normalField (310 + offset, 706 + offset, index++));
            boardFields.Add(new globeField  (310 + offset, 656 + offset, index++));         // blue globe
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
                allHomeFields.Add(new homeField(201 + offset + i * 49, 607 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(201 + offset + i * 49, 656 + offset, index++));
            // Yellow
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(63 + offset + i * 49, 200 + offset, index++));
            for (int i = 0; i < 2; i++)
                allHomeFields.Add(new homeField(63 + offset + i * 49, 250 + offset, index++));
        }

        private void SetupAllPathFields()
        {
            int index = 0;
            int offset = 10;

            //Green
            pathPlayerGreen.Add(new pathField(360 + offset, 63  + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 112 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 162 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 211 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 261 + offset, index++));

            //Red
            pathPlayerGreen.Add(new pathField(656 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(607 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(557 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(508 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(459 + offset, 360 + offset, index++));

            //Blue
            pathPlayerGreen.Add(new pathField(360 + offset, 656 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 607 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 557 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 508 + offset, index++));
            pathPlayerGreen.Add(new pathField(360 + offset, 459 + offset, index++));

            //Yellow
            pathPlayerGreen.Add(new pathField(63 + offset,  360 + offset, index++));
            pathPlayerGreen.Add(new pathField(112 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(162 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(211 + offset, 360 + offset, index++));
            pathPlayerGreen.Add(new pathField(261 + offset, 360 + offset, index++));
        }

        public void movePiece(Piece p, int moves)
        {
            switch (moves)
            {
                case 1:
                case 2:
                case 4:
                    moveXfields(p, moves);
                    break;
                case 3:
                    int nextStarIndex = findNextStar(p, 0) - p.placement.index; 
                    moveToNextStar(p, nextStarIndex);
                    break;
                case 5:
                    moveToNextGlobus(p);
                    break;
                case 6:
                    if (p.placement is homeField)
                        moveToNextGlobus(p);
                    else
                        moveXfields(p, moves);
                    break;
            }
        }

        private int checkIfHomefield(Piece p)
        {
            if (p.placement is homeField)
                switch (p.player.team)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 14;
                    case 3:
                        return 27;
                    case 4:
                        return 40;
                    default:
                        return 0;
                }
            else
            {
                return 0;
            }
        }

        private void moveXfields(Piece p, int moves)
        {
            if (p.placement.index + moves >= boardFields.Count) // Starts back from the start
            {
                int remainingMoves = moves - (boardFields.Count - p.placement.index);
                returnOtherPlayerHome(p, boardFields[0 + remainingMoves]);
            }
            else   
            {
                if (boardFields[p.placement.index + moves] is starField)   // Check if it's a starfield
                    moveToNextStar(p, moves);
                else
                    returnOtherPlayerHome(p, boardFields[p.placement.index + moves]);
            }
        }

        private void moveToNextGlobus(Piece p)
        {
            int ifHomefield = checkIfHomefield(p);

            if (ifHomefield != 0)
            {
                returnOtherPlayerHome(p, boardFields[ifHomefield]);
            }
            else
            {
                returnOtherPlayerHome(p, boardFields[findNextGlobus(p)]);
            }
        }
        private int findNextGlobus(Piece p)
        {
            int index = p.placement.index + 1;
            bool notFound = true;

            while (notFound)
            {
                if (index >= boardFields.Count)
                    index = 0;
                else
                {
                    if (boardFields[index] is globeField)
                        return index;
                    index++;
                }
            }
            return index;

        }

        private void moveToNextStar(Piece p, int moves)
        {
            int index = findNextStar(p, moves);
            returnOtherPlayerHome(p, boardFields[index]);
        }
        
        private int findNextStar(Piece p, int moves)
        {
            int index = p.placement.index + moves + 1;      // +1 so it doesn't "find" the star it's already on
            bool notFound = true;

            while (notFound)
            {
                if (index >= boardFields.Count)
                    index = 0;
                else
                {
                    if (boardFields[index] is starField)
                        notFound = false;
                    else
                        index++;
                }
            }
            return index;
        }

        private void returnOtherPlayerHome(Piece p, allFields newPlacement)
        {
            List<Piece> piecesAtField = findPiecesAtField(newPlacement);

            // Reacts to how many pieces is in piecesAtField list
            switch (piecesAtField.Count)
            {
                case 0:
                    resetSizeBeforeMove(p, newPlacement);
                    break;
                case 1:
                    if (piecesAtField[0].player == p.player)    // If the pieces are on the same team
                    {
                        resetSizeBeforeMove(p, newPlacement);

                        // Sets size and location of piece already on piece
                        piecesAtField[0].picture.Size = new Size(15, 15);
                        piecesAtField[0].picture.Location = new Point(piecesAtField[0].picture.Location.X - 4, piecesAtField[0].picture.Location.Y + 5);

                        // Sets size and location of new piece 
                        p.picture.Size = new Size(15, 15);
                        p.picture.Location = new Point(piecesAtField[0].picture.Location.X + 19, piecesAtField[0].picture.Location.Y);
                    }
                    else
                    {
                        movePieceHome(piecesAtField[0]);    // Moves other player home
                        resetSizeBeforeMove(p, newPlacement);           // Moves current player to field
                    }
                    break;
                case 2:     // Two pieces at the field
                    if (piecesAtField[0].player == p.player)    // If the pieces are on the same team
                    {
                        int dx;
                        Piece pLeft, pRight;

                        resetSizeBeforeMove(p, newPlacement);

                        // Find piece on the left
                        dx = piecesAtField[0].picture.Location.X - piecesAtField[1].picture.Location.X;
                        if (dx > 0)
                        {
                            pRight = piecesAtField[0];
                            pLeft = piecesAtField[1];
                        }
                        else
                        {
                            pRight = piecesAtField[1];
                            pLeft = piecesAtField[0];
                        }

                        // Relocate the two pieces already on the field
                        pLeft.picture.Location = new Point(pLeft.picture.Location.X, pLeft.picture.Location.Y - 9);
                        pRight.picture.Location = new Point(pRight.picture.Location.X, pRight.picture.Location.Y - 9);

                        // Sets size and location for new piece at field
                        p.picture.Size = new Size(15, 15);
                        p.picture.Location = new Point(pLeft.picture.Location.X + 9, pLeft.picture.Location.Y + 19);
                    }
                    else  // If not on the same team
                        movePieceHome(p);   // Moves current player home
                    break;
                case 3:     // Three pieces at the field
                    if (piecesAtField[0].player == p.player)    // If the pieces are on the same team
                    {
                        int dy;
                        Piece pLeft, pRight, pBottomL;

                        resetSizeBeforeMove(p, newPlacement);

                        // Find piece on the bottom, left and right
                        dy = piecesAtField[0].picture.Location.Y - piecesAtField[1].picture.Location.Y;
                        if (dy > 0)
                        {
                            pBottomL = piecesAtField[0];
                            piecesAtField.Remove(pBottomL);
                            pLeft = piecesAtField[findPieceOnLeft(piecesAtField)];
                            piecesAtField.Remove(pLeft);
                            pRight = piecesAtField[0];
                        }
                        else if (dy < 0)
                        {
                            pBottomL = piecesAtField[1];
                            piecesAtField.Remove(pBottomL);
                            pLeft = piecesAtField[findPieceOnLeft(piecesAtField)];
                            piecesAtField.Remove(pLeft);
                            pRight = piecesAtField[0];
                        }
                        else   // If the two piece[0] and piece[1] have the same y, then piece[2] is the bottom one
                        {
                            pBottomL = piecesAtField[2];
                            piecesAtField.Remove(pBottomL);
                            pLeft = piecesAtField[findPieceOnLeft(piecesAtField)];
                            piecesAtField.Remove(pLeft);
                            pRight = piecesAtField[0];
                        }

                        // Relocate the bottom piece
                        pBottomL.picture.Location = new Point(pLeft.picture.Location.X, pBottomL.picture.Location.Y);

                        // Sets size and location for new piece at field
                        p.picture.Size = new Size(15, 15);
                        p.picture.Location = new Point(pLeft.picture.Location.X + 19, pBottomL.picture.Location.Y);
                    }
                    else  // If not on the same team
                        movePieceHome(p);   // Moves current player home
                    break;
            }
        }
        private void movePieceHome(Piece p)
        {
            List<allFields> occupiedFields = new List<allFields>();
            int fieldIndex;
            bool notFound = true;

            foreach (Piece piece in p.player.pieces)
            {
                if (piece.placement is homeField)
                    occupiedFields.Add(piece.placement);
            }

            switch (p.player.team)  // Sets start fieldIndex for the player
            {
                case 1:
                    fieldIndex = 0;
                    break;
                case 2:
                    fieldIndex = 4;
                    break;
                case 3:
                    fieldIndex = 8;
                    break;
                case 4:
                    fieldIndex = 12;
                    break;
                default:
                    fieldIndex = 99;
                    break;
            }

            while (notFound)
            {
                if (occupiedFields.Contains(allHomeFields[fieldIndex]) == false)
                    notFound = false;
                else
                    fieldIndex++;
            }

            resetSizeBeforeMove(p, allHomeFields[fieldIndex]);
        }

        private void resetSizeBeforeMove(Piece p, allFields newPlacement)
        {
            List<Piece> piecesAtField = findPiecesAtField(p.placement);

            // Removes itself from the list
            int index = piecesAtField.IndexOf(p);
            piecesAtField.Remove(p);

            // Updates size and location of pieces at old field
            switch (piecesAtField.Count)    
            {
                case 1:
                    piecesAtField[0].picture.Size = new Size(26, 26);
                    piecesAtField[0].picture.Location = new Point(piecesAtField[0].placement.x, piecesAtField[0].placement.y);
                    break;
                case 2:     // Set size acording to two pieces at field
                    piecesAtField[0].picture.Location = new Point(piecesAtField[0].placement.x - 4, piecesAtField[0].placement.y + 5);
                    piecesAtField[1].picture.Location = new Point(piecesAtField[0].picture.Location.X + 19, piecesAtField[0].picture.Location.Y);
                    break;
                case 3:     // Set size acording to three pieces at field
                    piecesAtField[0].picture.Location = new Point(piecesAtField[0].placement.x - 4, piecesAtField[0].placement.y + 5 - 9);
                    piecesAtField[1].picture.Location = new Point(piecesAtField[0].picture.Location.X + 19, piecesAtField[0].picture.Location.Y);
                    piecesAtField[2].picture.Location = new Point(piecesAtField[0].picture.Location.X + 9, piecesAtField[0].picture.Location.Y + 19);
                    break;
                default:
                    break;
            }

            // Resets size and moves it
            p.picture.Size = new Size(26, 26);    // This resets the size, eg if it came from a field with more pieces on it
            p.newField(newPlacement);
        }

        private List<Piece> findPiecesAtField(allFields field)
        {
            List<Piece> piecesAtField = new List<Piece>();

            // Adds all pieces at newPlacement to piecesAtField list
            foreach (Player player in players)
            {
                foreach (Piece piece in player.pieces)
                {
                    if (piece.placement == field)
                        piecesAtField.Add(piece);
                }
            }

            return piecesAtField;
        }

        private int findPieceOnLeft(List<Piece> pieces) // Returns index of left piece
        {
            int dx;
            Piece pLeft, pRight;

            // Find piece on the left
            dx = pieces[0].picture.Location.X - pieces[1].picture.Location.X;
            if (dx > 0)
                return 1;
            else
                return 0;
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
