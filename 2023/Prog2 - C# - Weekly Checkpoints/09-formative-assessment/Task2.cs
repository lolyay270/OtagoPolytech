using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _09_formative_assessment
{
    public partial class Task2 : Form
    {
        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;
        private Graphics graphics;
        private Controller controller;

        public Task2()
        {
            InitializeComponent();

            offScreenBitmap = new Bitmap(Width, Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);
            graphics = CreateGraphics();
            controller = new Controller(offScreenGraphics, ClientSize);
            timer1.Enabled = true;
            WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            offScreenGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.Run();
            graphics.DrawImage(offScreenBitmap, 0, 0);
        }
    }
}
