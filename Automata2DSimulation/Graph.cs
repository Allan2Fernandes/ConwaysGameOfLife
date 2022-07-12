using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automata2DSimulation
{
    internal class Graph
    {
        Cell[,] currentGenCellArray;
        Cell[,] nextGenCellArray;
        PictureBox pictureBox;
        int xNumberOfCells;
        int yNumberOfCells;
        Bitmap bitmap;
        Graphics graphics;
        int initialAliveCells;
        public Graph(int xNumberOfCells, int yNumberOfCells, PictureBox pictureBox, int initialAliveCells)
        {
            this.xNumberOfCells = xNumberOfCells;
            this.yNumberOfCells = yNumberOfCells;
            this.pictureBox = pictureBox;
            currentGenCellArray = new Cell[xNumberOfCells, yNumberOfCells];
            nextGenCellArray = new Cell[xNumberOfCells, yNumberOfCells];
            this.initialAliveCells = initialAliveCells;

            for (int i = 0; i < xNumberOfCells; i++)
            {
                for (int j = 0; j < yNumberOfCells; j++)
                {
                    currentGenCellArray[i, j] = new Cell(new Coordinate(i, j), false);
                }
            }

            //Initialise about 50 cells
            Random random = new Random();
            for (int i = 0; i < initialAliveCells; i++)
            {
                int x = random.Next(0, xNumberOfCells);
                int y = random.Next(0, yNumberOfCells);
                currentGenCellArray[x, y].resurrectIt();
            }

            
        }

        public void drawGraph()
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Gray, 0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.BackgroundImage = bitmap;

            //Draw the cells
            for (int i = 0; i < currentGenCellArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentGenCellArray.GetLength(1); j++)
                {
                    drawCell(currentGenCellArray[i, j]);
                }
            }
            
        }

        public void drawCell(Cell cell)
        {
            if (cell.isItAlive())
            {
                graphics.FillRectangle(Brushes.AliceBlue, cell.getXPos(), cell.getYPos(), cell.getLength(), cell.getLength());
            }
            else
            {
                graphics.DrawRectangle(new Pen(Color.Black), cell.getRectangleToDraw());
            }
        }

        public void determineNextGenArray()
        {
            //Copy the array
            for (int i = 0; i < currentGenCellArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentGenCellArray.GetLength(1); j++)
                {
                    nextGenCellArray[i, j] = new Cell(currentGenCellArray[i,j].getCoordinate(), currentGenCellArray[i,j].isItAlive());
                }
            }
            //Determine next gen array
            for (int i = 0; i < currentGenCellArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentGenCellArray.GetLength(1); j++)
                {
                    updatenextGenArrayForSingleCell(currentGenCellArray[i, j]);
                }
            }

            //Set the current gen array to next gen array. Deep clone it here
            for (int i = 0; i < currentGenCellArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentGenCellArray.GetLength(1); j++)
                {
                    currentGenCellArray[i, j] = new Cell(nextGenCellArray[i, j].getCoordinate(), nextGenCellArray[i,j].isItAlive());
                }
            }

        }

        public void upedateGraph()
        {
            determineNextGenArray();
            drawGraph();
        }

        public void updatenextGenArrayForSingleCell(Cell cell)
        {
            int xCoord = cell.getCoordinate().getXCoord();
            int yCoord = cell.getCoordinate().getYCoord();
            int numOfAliveNeighbours = 0;
            int numOfDeadNeighbours = 0;

            //left top
            if (xCoord > 1 && yCoord > 1 && currentGenCellArray[xCoord - 1, yCoord - 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //top
            if (yCoord > 1 && currentGenCellArray[xCoord, yCoord - 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //right top
            if (xCoord < xNumberOfCells - 1 && yCoord > 1 && currentGenCellArray[xCoord + 1, yCoord - 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //right
            if (xCoord < xNumberOfCells - 1 && currentGenCellArray[xCoord + 1, yCoord].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //right bottom
            if (xCoord < xNumberOfCells - 1 && yCoord < yNumberOfCells - 1 &&currentGenCellArray[xCoord + 1, yCoord + 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //bottom
            if (yCoord < yNumberOfCells - 1 && currentGenCellArray[xCoord, yCoord + 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //bottom left
            if (xCoord > 1 && yCoord < yNumberOfCells - 1 && currentGenCellArray[xCoord - 1, yCoord + 1].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }

            //left
            if (xCoord > 1 && currentGenCellArray[xCoord - 1, yCoord].isItAlive())
            {
                numOfAliveNeighbours++;
            }
            else
            {
                numOfDeadNeighbours++;
            }




            if (numOfAliveNeighbours != 0)
            {
                Debug.WriteLine(numOfAliveNeighbours);
                Debug.WriteLine(cell.isItAlive());
            }

            if (cell.isItAlive())
            {
                if(numOfAliveNeighbours < 2)
                {
                    nextGenCellArray[xCoord, yCoord].killIt();
                }else if (numOfAliveNeighbours > 3)
                {
                    nextGenCellArray[xCoord, yCoord].killIt();
                }
                //The else case do nothing. It stays alive if 2 or 3 alive neighbours
            }
            else
            {
                if (numOfAliveNeighbours == 3)
                {
                    
                    nextGenCellArray[xCoord, yCoord].resurrectIt();
                }
                
            }

        }


    }
}
