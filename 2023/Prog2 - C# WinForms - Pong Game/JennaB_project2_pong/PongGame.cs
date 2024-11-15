using System;
using System.Drawing;
using System.Windows.Forms;

/*
 * Jenna Boyes 
 * This class is the brains of it all. 
 * It allows the user to press keys and runs the timer for the form.
 */

namespace JennaB_project2_pong
{
    public partial class PongGame : Form
    {
        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;
        private Graphics graphics;
        private Controller controller;

        public PongGame()
        {
            InitializeComponent();

            offScreenBitmap = new Bitmap(Width, Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);
            graphics = CreateGraphics();
            controller = new Controller(offScreenGraphics, ClientSize, timer1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            offScreenGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.Run();
            graphics.DrawImage(offScreenBitmap, 0, 0);
        }

        private void PongGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!timer1.Enabled) //start game
                    {
                        timer1.Enabled = true;
                    }
                    break;
                case Keys.Up:
                    controller.PlayerPad.PlayerMove('u');
                    break;
                case Keys.Down:
                    controller.PlayerPad.PlayerMove('d');
                    break;
                case Keys.P:
                    if (timer1.Enabled) //playing
                    {
                        PauseMenu();
                        timer1.Enabled = false;
                    }
                    else if (!timer1.Enabled) //paused
                    {
                        timer1.Enabled = true;
                    }
                    break;
                case Keys.R:
                    timer1.Enabled = true;
                    controller.Reset();
                    break;

            }
        }

        private void PongGame_Paint(object sender, PaintEventArgs e)
        {
            graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Width, Height));
            PauseMenu();
        }

        private void PauseMenu()
        {
            //search query "windows forms fill shape transparent colour c#"
            //i want to tint the screen a colour to show the users that the screen is plaused and still show where the ball/paddles are
            //I altered this line of code:    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
            //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-draw-with-opaque-and-semitransparent-brushes?view=netframeworkdesktop-4.8
            Font font = new Font("Bazooka Regular", 20, FontStyle.Regular);
            SolidBrush grayTransparent = new SolidBrush(Color.FromArgb(150, 100, 100, 100));
            graphics.FillRectangle(grayTransparent, new Rectangle(0, 0, Width, Height));
            graphics.FillRectangle(grayTransparent, new Rectangle(330, 450, 340, 200));
            graphics.DrawString("    GAME PAUSED \n\nPress SPACE to start", font, Brushes.DarkRed, new Point(360, 480));
            graphics.DrawString("CONTROLS: \nUp Arrow = move paddle up \nDown Arrow = move paddle down \nP key = pause \nR key = reset", font, Brushes.DarkBlue, new Point(10, 10));
        }

        private void PongGame_MouseClick(object sender, MouseEventArgs e)
        {
            Font font = new Font("Tahoma", 6, FontStyle.Regular);
            for (int x = 0; x < Width; x += 20)
            {
                graphics.DrawLine(Pens.Black, new Point(x, 0), new Point(x, Height));
                graphics.DrawString(x.ToString(), font, Brushes.Black, new Point(x, 0));
            }
            for (int y = 0; y < Height; y += 20)
            {
                graphics.DrawString(y.ToString(), font, Brushes.Black, new Point(0, y));
                graphics.DrawLine(Pens.Black, new Point(0, y), new Point(Width, y));
            }
        }
    }
}
