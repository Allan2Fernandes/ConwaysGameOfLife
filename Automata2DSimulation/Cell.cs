using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automata2DSimulation
{
    internal class Cell
    {
        Coordinate coordinate;
        public static int length = 30;
        Boolean isAlive;
       
        public Cell(Coordinate coordinate, Boolean isAlive)
        {
            this.coordinate = coordinate;
            
            this.isAlive = isAlive; 
        }


        public int getLength()
        {
            return length;
        }

        public int getXPos()
        {
            return coordinate.getXPos();
        }

        public int getYPos()
        {
            return coordinate.getYPos();
        }

        public Rectangle getRectangleToDraw()
        {
            return new Rectangle(coordinate.getXPos(), coordinate.getYPos(), length, length);
        }

        public Boolean isItAlive()
        {
            return isAlive;
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }

        public void resurrectIt()
        {
            isAlive = true;
        }

        public void killIt()
        {
            isAlive = false;
        }
    }
}
