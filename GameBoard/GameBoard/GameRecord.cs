using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public class GameRecord
    {
        List<List<int>> playersPlacements { get; set; }
        
        public GameRecord()
        {
            playersPlacements = new List<List<int>>();

            for (int i = 0; i < 4; i++)
                playersPlacements.Add(new List<int>());

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    playersPlacements[i].Add(0);
            }
        }

        public void addGame(int p1Placement, int p2Placement, int p3Placement, int p4Placement)
        {
            List<int> placements = new List<int>() { p1Placement, p2Placement, p3Placement, p4Placement };
            for (int i = 0; i < 4; i++)
            {
                playersPlacements[i][placements[i]-1]++;
            }
        }

        public override string ToString()
        {
            string text = "";

            for (int i = 0; i < 4; i++)
            {
                text += $"{currentPlayerString(i)}:\n" +
                    $"  1'st: {playersPlacements[i][0]}\n" +
                    $"  2'nd: {playersPlacements[i][1]}\n" +
                    $"  3'rd: {playersPlacements[i][2]}\n" +
                    $"  4'th: {playersPlacements[i][3]}\n";
            }
            
            return text;
        }

        private string currentPlayerString(int player)
        {
            switch (player)
            {
                case 0:
                    return "Green";
                case 1:
                    return "Red";
                case 2:
                    return "Blue";
                case 3:
                    return "Yellow";
                default:
                    return "Error";
            }
        }
    }
}
