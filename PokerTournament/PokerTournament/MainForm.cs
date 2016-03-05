using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PokerTournament
{
    public partial class MainForm : Form
    {
        static Player currentPlayer;
        static int playerPos;
        private DataUtility util;

        // constructor instantiates new DataUtility 
        public MainForm()
        {
            InitializeComponent();
            util = new DataUtility();
        }

         //----------------------------//
        //// New Player Tab Methods ////
       //----------------------------//

        //Save+close button
        private void saveBtn_Click_1(object sender, EventArgs e)
        {
            // validate ssn input and assign converted int to variable
            int ssn;
            if(!int.TryParse(ssnBox.Text, out ssn) || ssnBox.Text.Length != 9)
            {
                // alert user if invalid entry by length or unable to parse
                MessageBox.Show("Please Enter a valid SSN!", "Invalid Input!");
                return;
            }
            // verify the path is set
            VerifyPathSet();
            // update existing player list
            util.RefreshPlayerList();
            // instantiate new player
            Player newPlayer = new Player(firstNameBox.Text, lastNameBox.Text, ssn);

            // check if new player is a duplicate,
            // and if so, alert user and remove
            // existing player (done in DataUtility class)
            if (util.IsDupcliatePlayer(newPlayer))
            {
                MessageBox.Show("That player already exists\n" +
                                "The existing player has been updated",
                                "Duplicate player message");
            }

            // add new player to players list
            util.Players.Add(newPlayer);
            // save players list
            util.SavePlayers();

            //Confirm that the data was saved, and close the window
            MessageBox.Show("The player has been saved, thank you!",
                "Save Confirmation");

            //move to winnings tab on save
            TabPage t = tabControl1.TabPages[1];
            tabControl1.SelectedTab = t; //go to tab

            // clear text boxes
            firstNameBox.Clear();
            lastNameBox.Clear();
            ssnBox.Clear();




        }
        
        //cancel button closes form
        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

         //--------------------------//
        //// Winnings Tab Methods ////
       //--------------------------//

        // search button searches players by ssn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // verify paths set
            VerifyPathSet();
            // update player list
            util.RefreshPlayerList();

            // convert and assign search input to variable
            int ssn = Convert.ToInt32(ssnSearchForTextBox.Text);
            // find players that match this ssn (returns -1 if not found)
            int i = util.SearchPlayersBySSN(ssn);

            // if not found, then alert user
            if (i == -1)
            {
                MessageBox.Show("No players with the SSN exist", "Player not found");
            }
            // otherwise, update form information
            else
            {
                selectPlayer.Text = util.Players[i].FirstName + " " + util.Players[i].LastName;
                currentPlayer = util.Players[i];
                playerPos = i;
                totalWinnings.Text = currentPlayer.Winnings.CalculateTotalWinnings().ToString();
            }

            // clear search text box
            ssnSearchForTextBox.Clear();
        }

        // update button updates winnings for player found in search
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // alerts user if no player is selected
            if (currentPlayer == null)
            {
                MessageBox.Show("Please search for a player before updating", "No Player Selected");
                return;
            }

            // verify save path is set
            VerifyPathSet();

            // assign selected week to variable
            int currentWeek = Convert.ToInt16(weekBox.Text) - 1;

            // update winning amount for selected week
            currentPlayer.Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);
            util.Players[playerPos].Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);
            // update location for selected week
            currentPlayer.Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);
            util.Players[playerPos].Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);
            // update display of total winnings
            totalWinnings.Text = util.Players[playerPos].Winnings.CalculateTotalWinnings().ToString();

            // save players list to file
            util.SavePlayers();

        }

        private void weekBox_TextChanged(object sender, EventArgs e)
        {
            // checks to make sure currentPlayer is not
            // null before attempting to update player
            if (currentPlayer != null)
            {
                winningsBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Winning.ToString();
                stateBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Location.State;
                casinoBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Location.Name;
            }
        }

         //-------------------------//
        //// Results Tab Methods ////
       //-------------------------//

        // retrieve button displays all players, sorted by either winnings or ssn
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            // verify path set
            VerifyPathSet();
            // refresh player list
            util.RefreshPlayerList();
            // clear list box of previous results
            displayResultsListBox.Items.Clear();

            // sorting logic
            if (sortByWinningsRadioButton.Checked)
            {
                // sorts players by winnings
                util.Players.Sort(delegate (Player x, Player y)
                {
                    return y.Winnings.CalculateTotalWinnings().CompareTo(x.Winnings.CalculateTotalWinnings());
                });
            }
            // sorts players by ssn (default)
            else
            {
                util.Players.Sort();
            }

            // displays players in list box
            displayResultsListBox.Items.AddRange(util.Players.ToArray());
        }

        // build failed without this method - I think listener can be removed
        private void currentPathBox_Click(Object sender, EventArgs e)
        {
            
        }

        //Allow user to select new default save/load location and display path
        private void filePathItem_Click(object sender, EventArgs e)
        {
            SpawnFileDialog();
        }

         //-------------------//
        //// Other Methods ////
       //-------------------//

        // checks save path is not null
        private void VerifyPathSet()
        {
            if (util.Path == null)
            {
                MessageBox.Show("Please specify player file location", "Select File");
                SpawnFileDialog();
            }
        }

        //Allows user to specify file path
        void SpawnFileDialog()
        {
            folderBrowserDialog.ShowDialog();
            util.Path = folderBrowserDialog.SelectedPath + "\\Players.txt";
            currentPathBox.Text = "Current File Path: " + util.Path;
        }

        // exits application
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            //Exit application
            Application.Exit();
        }

        // method allows user to double-click user in list box to load details in winnings tab
        private void displayResultsListBox_DoubleClick(object sender, EventArgs e)
        {
            // split selected list box item at space delimeter
            string[] fields = displayResultsListBox.SelectedItem.ToString().Split(' ');

            // find players that match this ssn (returns -1 if not found)
            int i = util.SearchPlayersBySSN(Convert.ToInt32(fields[2]));

            // if not found, then alert user
            if (i == -1)
            {
                MessageBox.Show("No players with the SSN exist", "Player not found");
            }
            // otherwise, update form information
            else
            {
                selectPlayer.Text = util.Players[i].FirstName + " " + util.Players[i].LastName;
                currentPlayer = util.Players[i];
                playerPos = i;
                totalWinnings.Text = currentPlayer.Winnings.CalculateTotalWinnings().ToString("C");
            }

            // change selected tab
            tabControl1.SelectedIndex = 1;
        }
    }
}
