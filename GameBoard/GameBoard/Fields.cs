using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public abstract class allFields
    {
        public int x;
        public int y;
        public int team;    // 1:green, 2:red, 3:blue, 4:yellow
        public int amountOfPieces;
        public Piece p1, p2, p3, p4;
        public int index;

        public allFields(int x1, int y1, int i)
        {
            x = x1;
            y = y1;
            index = i;
        }
    }

    public class normalField : allFields
    {
        public normalField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    public class starField : allFields
    {
        public starField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    public class homeField : allFields
    {
        public homeField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    public class pathField : allFields
    {
        public pathField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    public class globeField : allFields
    {
        public globeField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    public class goalField : allFields
    {
        public goalField(int x1, int y1, int i) : base(x1, y1, i) { }
    }
}
