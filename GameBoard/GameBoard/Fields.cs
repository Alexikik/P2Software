using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    abstract class allFields
    {
        public int x;
        public int y;
        public int team;
        public int amountOfPieces;
        public Piece p1, p2, p3, p4;
        public int index;

        public allFields(int x1, int y1, int i)
        {
            x = x1;
            y = y1;
            index = i;
        }
        //public allFields(Piece p)
        //{
        //    if (p1 == null) 
        //        p1 = p;
        //} 
    }

    class normalField : allFields
    {
        public normalField(int x1, int y1, int i) : base(x1, y1, i) { }
    }

    class starField : allFields
    {
        public starField(int x1, int y1, int i) : base(x1, y1, i) { }
    }
}
