using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
  public class Dice
  {
    Random rnd = new Random();  // The seed for the dice is made here

    public int Roll()
    {
      return rnd.Next(6) + 1;
    }
  }
}