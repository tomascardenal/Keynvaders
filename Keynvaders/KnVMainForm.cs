//#define SUICIDEMODE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keynvaders
{
    /// <summary>
    /// Main Form for KeyNvaders game
    /// <author>Tomás Cardenal López</author>
    /// </summary>
    public partial class KnVMainForm : Form
    {
        /// <summary>
        /// Random Generator
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// The String of valid characters (a-z+ñ)
        /// </summary>
        private readonly string validChars = Properties.Resources.VALID_CHARS;
        /// <summary>
        /// Constants
        /// </summary>
        private const int START_WIDTH = 550,//The start menu width
            START_HEIGHT = 140,//The start menu height
            GAME_WIDTH = 1000,//The ingame width
            GAME_HEIGHT = 500,//The ingame height
            LVLUP_EACH = 30,//Levels up each x points
            TMR_CREATOR_INI = 600,//Initial interval for timer creator
            TMR_MOVEMENT_INI = 50;//Initial interval for timer movement
        /// <summary>
        /// Game variables
        /// </summary>
        private int outOfGameControls,//The number of controls which aren't game labels
            currentScore,//The current game score
            currentLevel,//The current game level
            displayScore,//The score being displayed (on high scores)
            displayState;//The displaying state of the score being displayed
        private bool inGame;//Activated when playing

        /// <summary>
        /// Data handler pointer
        /// </summary>
        private KnVDataHandler dataHand;

        /// <summary>
        /// Constructor (default game)
        /// </summary>
        public KnVMainForm()
        {
            InitializeComponent();
            outOfGameControls = Controls.Count;
            dataHand = new KnVDataHandler();
            inGame = false;
            checkScoreDisplay();
        }

        /// <summary>
        /// Controls the high score display animation
        /// </summary>
        /// <param name="sender">The timer sending the tick</param>
        /// <param name="e">The event arguments</param>
        private void tmrDisplayScores_Tick(object sender, EventArgs e)
        {
            if (displayState < 0 || displayState > 29)//If the display state goes out of the wanted bounds, just make it zero
            {
                displayState = 0;
            }

            if (displayScore < 0 || displayScore > dataHand.HighScoreList.Count - 1)//If the displayed score goes out if the high score list's bounds, just make it zero
            {
                displayScore = 0;
            }

            if (displayState == 0 || displayState == 29)//For display states starting and ending
            {
                lbDisplay.ForeColor = Control.DefaultBackColor;//Same color as background
            }
            else if (displayState == 1 || displayState == 28)//Start appearing-dissappearing
            {
                lbDisplay.ForeColor = Color.LightGray;
            }
            else if (displayState == 2 || displayState == 27)
            {
                lbDisplay.ForeColor = Color.DarkGray;
            }
            else if (displayState == 3 || displayState == 26)
            {
                lbDisplay.ForeColor = Color.Gray;
            }
            else if (displayState >= 4 && displayState <= 25)//Fully displayed (staying there between states 4 and 25
            {
                lbDisplay.ForeColor = Color.Black;
            }

            KnVDataHandler.HighScore hs = dataHand.HighScoreList[displayScore];//Get the highscore
            lbDisplay.Text = String.Format(Properties.Resources.HIGHSCORE_DISPLAY, displayScore + 1, hs.Name, hs.Score);//Display it
            this.Update();
            displayState++;
            if (displayState == 30)//When display state is 30, increment the score to display
            {
                displayScore++;
            }
        }

        /// <summary>
        /// Controls the game movement
        /// </summary>
        /// <param name="sender">The timer sending the tick</param>
        /// <param name="e">The event arguments</param>
        private void tmrMovement_Tick(object sender, EventArgs e)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control c = Controls[i];
                if (c.Name == Properties.Resources.LBGAME || c.Name == Properties.Resources.LBGAME_QCK)//Move the labels
                {
                    c.Top += c.Name == Properties.Resources.LBGAME ? 1 : 5;//LBGAME_QCK goes five pixels

                    if (c.Top >= this.ClientSize.Height - c.Height)//If a label reaches the bottom of the formulary, the game ends
                    {
                        gameLost();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Controls the spawning of labels in game
        /// </summary>
        /// <param name="sender">The timer sending the tick</param>
        /// <param name="e">The event arguments</param>
        private void tmrCreator_Tick(object sender, EventArgs e)
        {
            //if (rand.Next(0, 101) < 99)
            //{
                Label lb = new Label();//Create a new label
                lb.AutoSize = true;//Set it to autosize
                lb.Name = currentLevel > 1 && rand.Next(0, 10) < 2 ? Properties.Resources.LBGAME_QCK : Properties.Resources.LBGAME;//Set the name (and mode therefore, 2 in 10 chance of a quick label)
                lb.ForeColor = lb.Name == Properties.Resources.LBGAME ? Color.Black : Color.Red;//Black color for normal labels, red for quick labels
                lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));//Set the font
                lb.Top = 0;//Starting point (height)
                lb.Left = rand.Next(0, this.ClientSize.Width - lb.Width);//Starting point (width) on a random basg 
                lb.Text = validChars[rand.Next(0, validChars.Length)].ToString();//Set a random char from the string of valid characters
                lb.Text = currentLevel > 7 && rand.Next(0, 10) < 1 ? lb.Text.ToUpper() : lb.Text;//1 in 10 chance if level is higher than 7 of an uppercase letter
                if (inGame)//Check if game is enabled (don't let it add controls after game has ended)
                {
                    Controls.Add(lb);//Add the label
                }
                //FIXME HateYou attack. It works, but i'm not sure about this idea
            /*}
            else
            {
                string h = "i hate you";
                int left = 0;
                for(int i = 0; i < h.Length; i++)
                {
                    Label lb = new Label();
                    lb.Name = Properties.Resources.LBGAME;//Set the name (and mode therefore, 2 in 10 chance of a quick label)
                    lb.ForeColor = Color.Red;//Black color for normal labels, red for quick labels
                    lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));//Set the font
                    lb.Top = 0;//Starting point (height)
                    lb.Left = left;//Starting point (width) on a random basis
                    left += lb.Width+5;
                    lb.Text = h[i].ToString();//Set a random char from the string of valid characters
                    if (inGame)
                    {
                        Controls.Add(lb);
                    }
                }
            }*/
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <param name="sender">The button sending the event</param>
        /// <param name="e">The event arguments</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            currentScore = 0;//Reset score

#if SUICIDEMODE//Suicide debug mode settings
            tmrCreator.Interval = 50;
            tmrMovement.Interval = 10;
            level = 10;
#else
            //Normal mode settings
            tmrCreator.Interval = TMR_CREATOR_INI;
            tmrMovement.Interval = TMR_MOVEMENT_INI;
            currentLevel = 1;
#endif
            //Hide the main controls
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Controls[i].Enabled = false;
                Controls[i].Hide();
            }

            this.Focus();//Set the focus on the form
            updateTitle(true);//Update the title with score and level
            inGame = true;//Go ingame
            tmrDisplayScores.Stop();//Stop displaying the scores
            //Start the game timers
            tmrMovement.Start();
            tmrCreator.Start();
            //Set the form to game size
            this.Size = new Size(GAME_WIDTH, GAME_HEIGHT);
        }

        /// <summary>
        /// Controls the key presses ingame
        /// </summary>
        /// <param name="sender">The key sending the event</param>
        /// <param name="e">The key press arguments</param>
        private void keynvaders_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (inGame)
            {
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    Control c = Controls[i];
                    if (c.Name == Properties.Resources.LBGAME || c.Name == Properties.Resources.LBGAME_QCK)//if the controls are game controls
                    {
                        if (e.KeyChar == char.Parse(c.Text))//Compare the character with the key inputted
                        {
                            currentScore += c.Name == Properties.Resources.LBGAME ? 1 : 5; //Normal labels increment the score by one, quick ones by 5
                            Controls.RemoveAt(i);//Remove and dispose the labels
                            c.Dispose();
                            checkLevelUp();//Check if leveled up
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if high scores should be displayed and launches it if so
        /// </summary>
        private void checkScoreDisplay()
        {
            if (dataHand.HighScoreList.Count > 0)//If the highscore list has data
            {
                displayState = 0;
                displayScore = 0;
                tmrDisplayScores.Start();
            }
            else//If it doesn't just display no high scores
            {
                lbDisplay.Text = Properties.Resources.HIGHSCORE_NOSCORES;
                this.Update();
            }
        }

        /// <summary>
        /// Routine for the end of a game
        /// </summary>
        private void gameLost()
        {
            //Stop the timers and ingame
            tmrMovement.Stop();
            tmrCreator.Stop();
            inGame = false;

            //Show losing prompt
            MessageBox.Show(String.Format(Properties.Resources.MSG_LOSE_TXT, currentScore), Properties.Resources.MSG_LOSE_TITLE, MessageBoxButtons.OK);

            //Check if it's a high score and prompt the user for a name if so
            if (dataHand.IsHighScore(currentScore))
            {
                using (KnVHighScorePrompt hsPrompt = new KnVHighScorePrompt(currentScore))
                {
                    DialogResult result = hsPrompt.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        dataHand.AddHighScore(hsPrompt.PlayerName, hsPrompt.Score);
                        if (!dataHand.SaveToXml())
                        {
                            MessageBox.Show(Properties.Resources.MSG_ERRORSAVE_TXT, Properties.Resources.MSG_ERRORSAVE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            //Removing game controls
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control c = Controls[i];
                if (c.Name == Properties.Resources.LBGAME || c.Name == Properties.Resources.LBGAME_QCK)
                {
                    Controls.RemoveAt(i);
                    c.Dispose();
                }
            }
            //Reset the size to the menu size
            this.Size = new Size(START_WIDTH, START_HEIGHT);

            //Enabling and showing menu controls
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Controls[i].Enabled = true;
                Controls[i].Show();
            }

            //Check for high scores, update title to menu title
            checkScoreDisplay();
            updateTitle(false);
            this.Update();
        }

        /// <summary>
        /// Updates the form title
        /// </summary>
        /// <param name="inGame">True for ingame variables, false for plain title</param>
        private void updateTitle(bool inGame)
        {
            if (inGame)
            {
                this.Text = Properties.Resources.TITLE_MAIN + Properties.Resources.TITLE_SCORE + currentScore + Properties.Resources.TITLE_LEVEL + currentLevel;
            }
            else
            {
                this.Text = Properties.Resources.TITLE_MAIN;
            }
        }

        /// <summary>
        /// Checks if the player leveled up
        /// </summary>
        private void checkLevelUp()
        {
            if (currentScore > 0 && currentScore > LVLUP_EACH * currentLevel)//If the score reached a new level
            {
                currentLevel++;
                if (currentLevel > 4)
                {
                    if (tmrCreator.Interval > 50)//If the interval is bigger than 50, decrease 50 (starts at 600 so this will happen for levels 5 to 15)
                    {
                        tmrCreator.Interval -= 50;
                    }
                    else if (tmrCreator.Interval > 5)//If the interval is bigger than 5 but less or equal than 50, decrease 5 (this will happen for levels 16 to 24)
                    {
                        tmrCreator.Interval -= 5;
                    }
                    else//On level 25, the interval is 1
                    {
                        tmrCreator.Interval = 1;
                    }
                }
                if (tmrMovement.Interval > 5)//For levels 1-8
                {
                    tmrMovement.Interval -= 5;
                }
                else
                {
                    tmrMovement.Interval = 1;
                }
            }
            updateTitle(true);
        }

        private void KeynvadersMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
