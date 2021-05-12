using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormProject
{
    public partial class Multiplayer : Form
    {
        bool Player = false;

        int ScoreOne = 0;
        int ScoreTwo = 0;

        Label firstClicked = null;

        Label secondClicked = null;

        Random random = new Random();

        List<string> Icons = new List<string>()
        {
            "M","M","L","L","I","I","U","U","A","A","Z","Z","W","W","D","D"
        };
        public List<string> IconCopy = new List<string>()
        {
            "M","M","L","L","I","I","U","U","A","A","Z","Z","W","W","D","D"
        };
        public Multiplayer()   
        {
            InitializeComponent();
            AssignRandomMatches();  
        }
        private void CheckForWinner()
        {
            foreach(Control control in MatchingGameLayout.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;        
                }
            }
            if (ScoreOne > ScoreTwo)
            {
                MessageBox.Show("Congratulations Player One you win!");
            }
            else if(ScoreTwo > ScoreOne)
            {
                MessageBox.Show("Congratulations Player Two you win!");
            }
            else
            {
                
                MessageBox.Show("You both are too good for each other. Tie Game.");
            }
            Close();
        }

        private void AssignRandomMatches()
        {
            foreach (Control control in MatchingGameLayout.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(Icons.Count);
                    iconLabel.Text = Icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    Icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                return;
            }

            Label clickLabel = sender as Label;

            if(clickLabel != null)
            {
                if (clickLabel.ForeColor == Color.Black)
                {
                    return;
                }
                if(firstClicked == null)
                {
                    firstClicked = clickLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                secondClicked = clickLabel;
                secondClicked.ForeColor = Color.Black;

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (firstClicked.Text != secondClicked.Text)
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = firstClicked.BackColor;
            }
            else
            {
                if (Turn.Text == "Player 1")
                {
                    ScoreOne += 1;
                }
                else
                {
                    ScoreTwo += 1;
                }
            }

            firstClicked = null;
            secondClicked = null;

            PlayerOneScore.Text = "Player 1: " + ScoreOne;
            PlayerTwoScore.Text = "Player 2: " + ScoreTwo;

            if (Player == true)
            {
                Turn.Text = "Player 1";
                Player = false;
            }
            else
            {
                Turn.Text = "Player 2";
                Player = true;
            }
            CheckForWinner();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            foreach (Control control in MatchingGameLayout.Controls)
            {
                Label iconLabel = control as Label;

                iconLabel.ForeColor = iconLabel.BackColor;
            }
            Icons = IconCopy;
            AssignRandomMatches();
            Player = false;
            timer1.Start();
            ScoreOne = 0;
            ScoreTwo = 0;
            CheckForWinner();
        }
    }
}
