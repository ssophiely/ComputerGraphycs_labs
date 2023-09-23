using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.ScaleTransform(1.0F, -1.0F);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ship.DrawShip(g);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ship.RotateShip(g, 30);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ship.ScaleShip(g, 0.5F);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Ship.MoveShip(g, 15, 3);
        }
    }
}
