using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            pb.Image = null;
        }

        private void DrawBaseC_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height); g = Graphics.FromImage(bmp);
            Ship.Draw(g, new Pen(Color.Black, 2), Ship.coordinateMatrixF, Ship.coordinateMatrixB);
            pb.Image = bmp;

        }

        private void RotateX_Click(object sender, EventArgs e)
        {
            int angle = (int)rx.Value;
            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);

            g = Graphics.FromImage(bmp);
            Ship.RotateShipX(g, angle);
            pb.Image = bmp;
        }

        private void RotateY_Click(object sender, EventArgs e)
        {
            int angle = (int)ry.Value;
            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);

            g = Graphics.FromImage(bmp);
            Ship.RotateShipY(g, angle);
            pb.Image = bmp;
        }

        private void RotateZ_Click(object sender, EventArgs e)
        {
            int angle = (int)rz.Value;
            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);

            g = Graphics.FromImage(bmp);
            Ship.RotateShipZ(g, angle);
            pb.Image = bmp;
        }

        private void Scale_Click(object sender, EventArgs e)
        {
            double x = (double)scx.Value;
            double y = (double)scy.Value;
            double z = (double)scz.Value;

            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);

            g = Graphics.FromImage(bmp);
            Ship.Scale(g, x, y, z);
            pb.Image = bmp;
        }

        private void Move_Click(object sender, EventArgs e)
        {
            int x = (int)mx.Value;
            int y = (int)my.Value;
            int z = (int)mz.Value;

            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);
            g = Graphics.FromImage(bmp);
            Ship.Move(g, x, y, z);
            pb.Image = bmp;
        }

        private void Projection_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);
            g = Graphics.FromImage(bmp);
            Ship.DrawCavalier(g);
            pb.Image = bmp;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            pb.Image = null;

            Bitmap bmp = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);
            g = Graphics.FromImage(bmp);
            Ship.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap mainImage = new Bitmap(pb.Image);
            Bitmap extraImage = new Bitmap(pb.Width, pb.Height);
            Cleaner.DeleteLines(mainImage, extraImage);
            pb.Image = null;
            pb.Image = mainImage;
        }
    }
}
