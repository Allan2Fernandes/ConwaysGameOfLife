using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata2DSimulation
{
    internal class Coordinate
    {
        int xCoord;
        int yCoord;
        int xPos;
        int yPos;

        public Coordinate(int xCoord, int yCoord)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            xPos = xCoord*Cell.length;
            yPos = yCoord * Cell.length;
        }

        public int getXPos()
        {
            return xPos;
        }

        public int getYPos()
        {
            return yPos;
        }

        public int getXCoord()
        {
            return xCoord;
        }

        public int getYCoord()
        {
            return yCoord;
        }
    }
}
