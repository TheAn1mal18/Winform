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
        // Create all the variables used to control player turns, icons, and when the players have clicked
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
            // this randomizes the icons with the labels
            AssignRandomMatches();  
        }
        private void CheckForWinner()
        {
            foreach(Control control in MatchingGameLayout.Controls)
            {
                // this looks through every control in the table and checks if they have all been matched or not.
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    // if anyone label has not been matched it returns and stops checking.
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;        
                }
            }
            // this checks to see which player has won.
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
                //this checks through every variable and assigns each a random string from the icons list.
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
                // if the timer is still going this stops the player from clicking.
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
                    // if it is the players first click this will save that label string
                    firstClicked = clickLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                // this saves the players second click with the labels text
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
                // this decides if the two labels the player has clicked match.
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = firstClicked.BackColor;
            }
            else
            {
                // this changes the turn between the two players.
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
                // this changes the text of the players turn.
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
    }
}
