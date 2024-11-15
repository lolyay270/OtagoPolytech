using System;
using System.Drawing;
using System.Media;

/*
 * Jenna Boyes 
 * This class holds all information for the ball, how it collides and when to reset.
 * It also makes noise on each collision and tells Score.cs when and what to do.
 */

namespace JennaB_project2_pong
{
    internal class Ball : GameObject
    {
        private const int SIZE = 25;
        private Point speed;
        private Point position;
        private Score score;
        private SoundPlayer bounceSound = new SoundPlayer(Properties.Resources.bounceSound);
        private SoundPlayer playerPoint = new SoundPlayer(Properties.Resources.playerPoint);
        private SoundPlayer aiPoint = new SoundPlayer(Properties.Resources.aiPoint);

        public int Size { get => SIZE; }
        public Point Speed { get => speed; set => speed = value; }
        public Point Position { get => position; set => position = value; }

        public Ball(Point speed, Color colour, Graphics graphics, Size clientSize, Score score) : base (graphics, clientSize, colour)
        {
            this.speed = speed;
            this.score = score;
            position = new Point((clientSize.Width - SIZE) / 2, (clientSize.Height - SIZE) / 2);
        }

        public override void Draw()
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
            if (position.X <= 0) //hit left wall
            {
                speed.X = -speed.X;
                aiPoint.Play();
                score.AiPoint();
                Reset();
            }

            if (position.X >= clientSize.Width - SIZE) //hit right wall
            {
                speed.X = -speed.X;
                playerPoint.Play();
                score.PlayerPoint();
                Reset();
            }

            if (position.Y <= 0 || position.Y >= clientSize.Height - SIZE) //hit top or bottom walls
            {
                speed.Y = -speed.Y;
                bounceSound.Play();
            }
        }
        
        public void BouncePaddles(Paddle playerPad, Paddle aiPad)
        {
            //values to say if ball is within the vari name
            bool playerY = false, playerX = false, aiY = false, aiX = false;

            //  left ball  <=                  right of pad
            if (position.X <= playerPad.Position.X + playerPad.Size.Width) playerX = true;

            //       ball bot     >     pad top          &&  ball top  <                   pad bottom
            if (position.Y + SIZE > playerPad.Position.Y && position.Y < playerPad.Position.Y + playerPad.Size.Height) playerY = true;


            //       right ball   >=     left pad
            if (position.X + SIZE >= aiPad.Position.X) aiX = true;

            //       ball bot     >     pad top      &&  ball top  <            pad bottom
            if (position.Y + SIZE > aiPad.Position.Y && position.Y < aiPad.Position.Y + aiPad.Size.Height) aiY = true;


            //player paddle bounce
            if (playerY && playerX)
            {
                speed.X = Math.Abs(speed.X);
                bounceSound.Play();
                aiPad.AiSpeed(); //change ai speed to random num when ball change direction
            }

            //ai paddle bounce
            if (aiY && aiX)
            {
                speed.X = -Math.Abs(speed.X);
                bounceSound.Play();
            }
        }

        public override void Reset()
        {
            //reset to center of form
            position = new Point((clientSize.Width - SIZE) / 2, (clientSize.Height - SIZE) / 2);

            //random direction
            Random rand = new Random();
            int[] direction = new int[2];
            for (int i = 0; i < 2; i++)
            {
                direction[i] = rand.Next(2);
                if (direction[i] == 0) direction[i] = -1;
            }
            speed = new Point(speed.X * direction[0], speed.Y * direction[1]);
        }
    }
}
