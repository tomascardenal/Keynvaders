using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keynvaders
{
    /// <summary>
    /// High score prompt for KeyNvaders game
    /// <author>Tomás Cardenal López</author>
    /// </summary>
    public partial class KnVHighScorePrompt : Form
    {
        /// <summary>
        /// The player name
        /// </summary>
        public string PlayerName;
        /// <summary>
        /// The score
        /// </summary>
        public int Score;

        /// <summary>
        /// Initializes a high score prompt 
        /// </summary>
        /// <param name="score">The high score</param>
        public KnVHighScorePrompt(int score)
        {
            this.PlayerName = "";
            this.Score = score;
            InitializeComponent();
            this.Text += " " + score;
        }

        
        /// <summary>
        /// Checks for the name to add and ends this dialog
        /// </summary>
        /// <param name="sender">The button sending the event</param>
        /// <param name="e">The event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.PlayerName = textBox1.Text;
            this.PlayerName = this.PlayerName == "" ? "NONAMER" : this.PlayerName;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Cancels the result if the name is left empty and close was invoked
        /// </summary>
        /// <param name="sender">The closing event</param>
        /// <param name="e">The event arguments</param>
        private void KnVHighScorePrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.PlayerName == "")
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
