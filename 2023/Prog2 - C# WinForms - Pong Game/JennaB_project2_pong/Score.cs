using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;


/*
 * Jenna Boyes 
 * This class records the current scores and allows them to update. 
 * It also stops the game when someone wins, and show the correct end screen.
 * The scores at the end of each game are saved to file and shown to user.
 */


namespace JennaB_project2_pong
{
    internal class Score : GameObject
    {
        private int playerScore, aiScore;
        private System.Windows.Forms.Timer timer;
        private Font font = new Font("Bazooka Regular", 30, FontStyle.Regular);
        private Size form3rds;
        private string filepath = "../../Highscores.txt";
        private SoundPlayer loseGame = new SoundPlayer(Properties.Resources.loseGame);
        private SoundPlayer winGame = new SoundPlayer(Properties.Resources.winGame);
        private List <int> highscores = new List<int> ();

        public int PlayerScore { get => playerScore; }
        public int AiScore { get => aiScore; }

        public Score(Color colour, Graphics graphics, Size clientSize, System.Windows.Forms.Timer timer) : base(graphics, clientSize, colour)
        {
            this.playerScore = 0;
            this.aiScore = 0;
            this.timer = timer;

            form3rds = new Size(clientSize.Width / 3, clientSize.Height / 3);
        }

        public void AiPoint()
        {
            aiScore++;
        }

        public void PlayerPoint()
        {
            playerScore++;
        }

        public override void Reset()
        {
            playerScore = 0;
            aiScore = 0;
        }

        public void CheckScores()
        {
            if ( playerScore >= 10)
            {
                EndGame();
                graphics.DrawString("      YOU WIN \n\nPress R to restart", font, Brushes.Green, new Point(form3rds.Width, form3rds.Height + 30));
                winGame.Play();
            }
            if ( aiScore >= 10)
            {
                EndGame();
                graphics.DrawString("     YOU LOSE \n\nPress R to restart", font, Brushes.DarkRed, new Point(form3rds.Width, form3rds.Height + 30));
                loseGame.Play();
            }
        }

        public override void Draw()
        {
            int scorePadding = 10;
            int yPosition = (clientSize.Height - (int)font.Size) / 2;
            graphics.DrawString(playerScore.ToString(), font, brush, new Point (clientSize.Width / 3 - (int)font.Size - scorePadding, yPosition));
            graphics.DrawString(aiScore.ToString(), font, brush, new Point(clientSize.Width * 2/3 + scorePadding, yPosition));
        }

        private void EndGame()
        {
            //basic gray background colouring
            timer.Enabled = false;
            SolidBrush grayTransparent = new SolidBrush(Color.FromArgb(150, 100, 100, 100));
            graphics.FillRectangle(grayTransparent, new Rectangle(0, 0, clientSize.Width, clientSize.Height)); 
            graphics.FillRectangle(grayTransparent, new Rectangle(form3rds.Width, form3rds.Height, form3rds.Width, form3rds.Height));

            //read all highscores, swap in players highscore, put into txt file
            ReadHighscoresFile();
            AddCurrentToHighscores();
            WriteHighscoresFile();

            //show top 5 highscores from file
            int fontSize = 10; //wanting to reduce font size to draw less attention

            graphics.DrawString(" HIGHSCORES:", font, Brushes.DarkBlue, new Point(form3rds.Width, form3rds.Height * 2));
            for (int i = 0; i < highscores.Count; i += 2)
            {
                //moving each line down with fontSize * i for point y value, and below title with font.Size
                graphics.DrawString($"                         player: {highscores[i]}         ai: {highscores[i + 1]}", new Font("Bazooka Regular", fontSize, FontStyle.Regular), Brushes.DarkBlue, new Point(form3rds.Width, form3rds.Height * 2 + (fontSize * (i + 2)) + (int)font.Size)); 
            }

            //empty highscores if user restarts at EndGame
            highscores.Clear();
        }

        private void ReadHighscoresFile()
        {
            StreamReader sr = new StreamReader(filepath);
            while (!sr.EndOfStream)
            {
                string fileLine = sr.ReadLine();
                string[] fileScores = fileLine.Split(',');
                try
                {
                    int.Parse(fileScores[0]);
                    int.Parse(fileScores[1]);
                }
                catch
                {
                    throw new Exception("ERROR: Highscores.txt has something other than numbers and commas. \nFormat should be {playerScore},{aiScore}[new line]");
                }

                foreach (string score in fileScores)
                {
                    highscores.Add(int.Parse(score));
                }
            }

            sr.Close();
        }

        private void WriteHighscoresFile()
        {
            StreamWriter sw = new StreamWriter(filepath);
            for (int i = 0; i < highscores.Count; i += 2) 
            {
                sw.WriteLine($"{highscores[i]},{highscores[i + 1]}");
            }
            sw.Close();
        }

        private void AddCurrentToHighscores()
        {
            if (highscores.Count >= 10) 
            {
                highscores.RemoveRange(highscores.Count - 2, 2); //remove last 2 scores, oldest session thru game
            }
            highscores.Insert(0, playerScore);
            highscores.Insert(1, aiScore);
        }
    }
}
