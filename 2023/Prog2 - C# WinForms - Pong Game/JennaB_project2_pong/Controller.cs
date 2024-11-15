using System;
using System.Drawing;
using System.Linq;

/*
 * Jenna Boyes 
 * This class instances all visible object in the game.
 * It also calls methods for each object, so that they can run each tick and reset when required.
 */

namespace JennaB_project2_pong
{
    internal class Controller
    {
        private Ball ball;
        private Paddle playerPad, aiPad;
        private Score score;
        private Random random = new Random();

        public Controller(Graphics graphics, Size clientSize, System.Windows.Forms.Timer timer)
        {
            //i would get some of these values from the classes if i knew how to without instancing an object first
            Size paddleSize = new Size(20, 100);
            int paddleMidPlace = (clientSize.Height - paddleSize.Height) / 2;
            int ballSpeed = 10;
            int paddleSpaceAround = 10;

            score = new Score(RandomColour(), graphics, clientSize, timer);
            ball = new Ball(new Point(ballSpeed, ballSpeed), RandomColour(), graphics, clientSize, score);
            playerPad = new Paddle(new Point(paddleSpaceAround, paddleMidPlace), RandomColour(), graphics, clientSize); ;
            aiPad = new Paddle(new Point(clientSize.Width - paddleSize.Width - paddleSpaceAround, paddleMidPlace), RandomColour(), graphics, clientSize);

            ball.Reset(); //using this for random ball direction
        }

        internal Paddle PlayerPad { get => playerPad; set => playerPad = value; }

        public void Run()
        {
            //ball each timer tick
            ball.Move();
            ball.BounceSide();
            ball.Draw();
            ball.BouncePaddles(playerPad, aiPad);

            //paddles each timer tick
            aiPad.Draw();
            playerPad.Draw();
            aiPad.AiMove(ball);

            //score
            score.Draw();
            score.CheckScores();
        }

        private Color RandomColour()
        {
            int[] num = new int[3];
            do
            {
                for (int i = 0; i < num.Length; i++)
                {
                    num[i] = random.Next(256);
                }
            } while (num.Sum() < 200); //do not want dark colours that blend into background
            return Color.FromArgb(num[0], num[1], num[2]);
        }

        public void Reset() 
        {
            score.Reset();
            ball.Reset();
            aiPad.Reset();
            playerPad.Reset();
        }
    }
}
