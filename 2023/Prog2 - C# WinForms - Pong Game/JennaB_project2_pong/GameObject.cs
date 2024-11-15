using System.Drawing;

/*
 * Jenna Boyes 
 * This class is the parent for all things that show on the form.
 * It allowed me to code inheritance into Ball, Paddle and Score.
 */

namespace JennaB_project2_pong
{
    internal abstract class GameObject
    {
        //fields
        protected Graphics graphics;
        protected Size clientSize;
        protected Brush brush;
        protected Color colour;

        //constructor
        public GameObject(Graphics graphics, Size clientSize, Color colour)
        {
            this.graphics = graphics;
            this.clientSize = clientSize;
            this.colour = colour;
            Brush = new SolidBrush(colour);
        }

        //encapsulations
        public Graphics Graphics { get => graphics; set => graphics = value; }
        public Size ClientSize { get => clientSize; set => clientSize = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Color Colour { get => colour; set => colour = value; }

        //methods
        public abstract void Draw();
        public abstract void Reset();
    }
}
