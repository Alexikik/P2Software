using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
  class Dice
  {
    Random rnd = new Random();

    public int Roll()
    {
      return rnd.Next(6) + 1;
    }
  }
}