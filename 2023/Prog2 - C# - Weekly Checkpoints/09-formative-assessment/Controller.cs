using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_formative_assessment
{
    public class Controller
    {
        private Ball ball;

        public Controller(Graphics graphics, Size clientSize)
        {
            ball = new Ball(new Point(10, 10), new Point(clientSize.Width / 2, clientSize.Height / 2), Color.Blue, graphics, clientSize);
        }

        public void Run()
        {
            ball.Move();
            ball.BounceSide();
            ball.Draw();
        }
    }
}