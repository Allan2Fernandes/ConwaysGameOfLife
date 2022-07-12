using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automata2DSimulation
{
    public partial class Form1 : Form
    {
        Graph graph;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.CadetBlue;

            graph = new Graph(pictureBox1.Width/Cell.length, pictureBox1.Height/Cell.length, pictureBox1, trackBar1.Value);
            simulationTimer.Start();

            
        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            graph.upedateGraph();
            Debug.WriteLine("Tick");
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if(pauseButton.Text == "Pause")
            {
                simulationTimer.Stop();
                pauseButton.Text = "Play";
            }
            else
            {
                simulationTimer.Start();
                pauseButton.Text = "Pause";
            }
           
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            graph = new Graph(pictureBox1.Width / Cell.length, pictureBox1.Height / Cell.length, pictureBox1, trackBar1.Value);
            simulationTimer.Start();
        }
    }
}
