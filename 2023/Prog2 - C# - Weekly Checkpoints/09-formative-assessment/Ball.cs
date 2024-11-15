using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_formative_assessment
{
    public class Ball
    {
        private const int SIZE = 25;
        private Point speed, position;
        private Color colour;
        private Graphics graphics;
        private Brush brush;
        private Size clientSize;

        public Ball(Point speed, Point position, Color colour, Graphics graphics, Size clientSize)
        {
            this.speed = speed;
            this.position = position;
            this.colour = colour;
            this.graphics = graphics;
            this.clientSize = clientSize;
            brush = new SolidBrush(colour);
        }

        public void Draw()
        {
            graphics.FillEllipse(brush, new Rectangle(position.X, position.Y, SIZE, SIZE));
        }

        public void Move()
        {
            position.X += speed.X;
            position.Y += speed.Y;
        }

        public void BounceSide() 
        {
            if (position.X <= 0 || position.X >= clientSize.Width-SIZE)
            {
                speed.X = -speed.X;
            }

            if (position.Y <= 0 || position.Y >= clientSize.Height-SIZE)
            {
                speed.Y = -speed.Y;
            }
        }
    }
}
