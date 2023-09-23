﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab23
{
    public partial class Form1 : Form
    {
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();

            // Преобразование системы отсчета
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.ScaleTransform(1.0F, -1.0F);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void DrawBaseC_Click(object sender, EventArgs e)
        {
            Ship.DrawBase(g);
        }

        private void RotateX_Click(object sender, EventArgs e)
        {
            int angle = (int)rx.Value;
            g.Clear(Color.White);
            Ship.RotateShipX(g, angle);
        }

        private void RotateY_Click(object sender, EventArgs e)
        {
            int angle = (int)ry.Value;
            g.Clear(Color.White);
            Ship.RotateShipY(g, angle);
        }

        private void RotateZ_Click(object sender, EventArgs e)
        {
            int angle = (int)rz.Value;
            g.Clear(Color.White);
            Ship.RotateShipZ(g, angle);
        }

        private void Scale_Click(object sender, EventArgs e)
        {
            double x = (double)scx.Value;
            double y = (double)scy.Value;
            double z = (double)scz.Value;
            g.Clear(Color.White);
            Ship.Scale(g, x, y, z);
        }

        private void Move_Click(object sender, EventArgs e)
        {
            int x = (int)mx.Value;
            int y = (int)my.Value;
            int z = (int)mz.Value;
            g.Clear(Color.White);
            Ship.Move(g, x, y, z);
        }

        private void Projection_Click(object sender, EventArgs e)
        {
            Ship.DrawCavalier(g);
        }
    }
}