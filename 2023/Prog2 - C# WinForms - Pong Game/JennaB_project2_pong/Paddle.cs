using System;
using System.Drawing;

/*
 * Jenna Boyes 
 * This class hold the information for both paddles, and how the user and "ai" interact with their respective paddles.
 * The paddles are both confined to the form with a small amount of padding around all sides.
 */

namespace JennaB_project2_pong
{
    internal class Paddle : GameObject
    {
        //paddle only fields
        private Size size;
        private Point position;
        private int padding = 10, playerMoveSpeed = 15, aiMoveSpeed = 7;

        //constructor
        public Paddle(Point position, Color colour, Graphics graphics, Size clientSize) : base (graphics, clientSize, colour)
        {
            size = new Size(20, 100);
            this.position = position;
        }

        //encapsulation
        public Size Size { get => size; }
        public Point Position { get => position; set => position = value; }

        //methods
        public override void Draw()
        {
            graphics.FillRectangle(brush, new Rectangle(position.X, position.Y, size.Width, size.Height));
        }
        public void AiMove(Ball ball)
        {
            if (ball.Speed.X > 0) //move only if ball is moving towards right paddle
            {
                if (ball.Position.Y >= position.Y + (size.Height / 2)) //ball is below paddle middle
                {
                    if (position.Y + size.Height < clientSize.Height - aiMoveSpeed - padding) //1 move above bottom of form
                    {
                        position.Y += aiMoveSpeed;
                    }
                    else //stop moving below padding at bottom
                    {
                        position.Y = clientSize.Height - padding - size.Height;
                    }
                }
                else if (ball.Position.Y + ball.Size <= position.Y + (size.Height / 2)) //ball is above paddle middle
                {
                    if (position.Y >= padding + aiMoveSpeed) //1 movement below top of movement area
                    {
                        position.Y -= aiMoveSpeed;
                    }
                    else //stop from moving above padding size at top
                    {
                        position.Y = padding; 
                    }
                }
            }
        }

        public void AiSpeed()
        {
            Random rnd = new Random();
            aiMoveSpeed = rnd.Next(6, 11); //speeds are 6 (never catches ball) to 10 (always catches ball)
        }

        public void PlayerMove(char move)
        {
            if (move == 'u') //up button pressed
            {
                if (position.Y > padding + playerMoveSpeed) //paddle is 1 move below top padding
                {
                    position.Y -= playerMoveSpeed;
                }
                else
                {
                    position.Y = padding;
                }
            }
            else if (move == 'd') //down button pressed
            {
                if (position.Y + size.Height < clientSize.Height - playerMoveSpeed - padding) //bot of paddle is 1 move above bottom padding
                {
                    position.Y += playerMoveSpeed;
                }
                else
                {
                    position.Y = clientSize.Height - padding - size.Height;
                }
            }
            else
            {
                throw new Exception("You have given playerMove an incorrect char value");
            }
        }

        public override void Reset()
        {
            //reset paddle to centre of form height
            position.Y = (clientSize.Height - Size.Height) / 2;
        }
    }
}
