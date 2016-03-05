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

        public MainForm()
        {
            InitializeComponent();
            util = new DataUtility();
        }

        //string path = null;

        // checks save path is not null
        private void VerifyPathSet()
        {
            if (util.Path == null)
            {
                MessageBox.Show("Please specify player file location", "Select File");
                SpawnFileDialog();
            }
        }

        //Save+close button
        private void saveBtn_Click_1(object sender, EventArgs e)
        {

            VerifyPathSet();
            util.RefreshPlayerList();

            //List<Player> players = util.RefreshPlayerList();

            //create a new Player object
            try {
                Convert.ToInt32(ssnBox.Text);
            } catch (FormatException ex)
            {
                MessageBox.Show("Please Enter a valid SSN!", "Invalid Input!");
                return;
            }

            if (ssnBox.Text.Length != 9)
            {
                MessageBox.Show("Please Enter a valid SSN!", "Invalid Input!");
                return;
            }

            Player newPlayer = new Player(firstNameBox.Text, lastNameBox.Text, Convert.ToInt32(ssnBox.Text));

            for (int q = 0; q < util.Players.Count; ++q)
            {
                if (util.Players[q].Equals(newPlayer))
                {
                    MessageBox.Show("That employee already exists\nThe existing employee has been updated", "Duplicate employee message");
                    util.Players.Remove(util.Players[q]);
                }
            }
            util.Players.Add(newPlayer);
            util.SavePlayers();
            //SavePlayers(players);

            //Confirm that the data was saved, and close the window
            MessageBox.Show("The player has been saved, thank you!", "Save Confirmation");
            firstNameBox.Clear();
            lastNameBox.Clear();
            ssnBox.Clear();

        }

        //List<Player> RefreshPlayerList()
        //{
        //    // This method gets all of the saved players or creates a file to store them if it does not exist
        //    FileStream infile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //    List<Player> players = new List<Player>();
        //    BinaryFormatter bformatter = new BinaryFormatter();
        //    while (infile.Position < infile.Length)
        //    {
        //        Player newPlayer = (Player)bformatter.Deserialize(infile);
        //        players.Add(newPlayer);
        //    }

        //    infile.Close();

        //    return players;
        //}

        //void SavePlayers(List<Player> players)
        //{

        //    FileStream outfile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

        //    // The file is deleted with this next statement
        //    outfile.SetLength(0);

        //    BinaryFormatter bformatter = new BinaryFormatter();

        //    // The updated list of employees is written to the file
        //    foreach (Player x in players)
        //    {
        //        bformatter.Serialize(outfile, x);
        //    }

        //    outfile.Close();
        //}

        //Allows user to specify file path
        void SpawnFileDialog()
        {
            folderBrowserDialog.ShowDialog();
            util.Path = folderBrowserDialog.SelectedPath + "\\Players.txt";
            currentPathBox.Text = "Current File Path: " + util.Path;
        }
        
        //cancel button closes form
        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            VerifyPathSet();
            util.RefreshPlayerList();

            //List<Player> searchPlayers = util.RefreshPlayerList();

            bool found = false;

            for (int i = 0; i < util.Players.Count; i++)
            {
                if (Convert.ToInt32(ssnSearchForTextBox.Text) == util.Players[i].ssn)
                {
                    selectPlayer.Text = util.Players[i].FirstName + " " + util.Players[i].LastName;
                    currentPlayer = util.Players[i];
                    playerPos = i;
                    found = true;
                    totalWinnings.Text = currentPlayer.Winnings.CalculateTotalWinnings().ToString();
                }
            }

            if (found == false)
            {
                MessageBox.Show("No players with the SSN exist", "Player not found");
            }

            ssnSearchForTextBox.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // alerts user if no player is selected
            if (currentPlayer == null)
            {
                MessageBox.Show("Please search for a player before updating", "No Player Selected");
                return;
            }

            VerifyPathSet();
            util.RefreshPlayerList();
            // The update button uses the selected player index set with the search button to update winnings for a player
            //List<Player> updatePlayers = util.RefreshPlayerList();

            int currentWeek = Convert.ToInt16(weekBox.Text) - 1;

            currentPlayer.Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);
            util.Players[playerPos].Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);

            currentPlayer.Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);
            util.Players[playerPos].Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);

            totalWinnings.Text = util.Players[playerPos].Winnings.CalculateTotalWinnings().ToString();

            util.Players = util.Players;
            util.SavePlayers();

            //SavePlayers(updatePlayers);
        }
        
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            displayResultsListBox.Items.Clear();

            VerifyPathSet();
            util.RefreshPlayerList();

            //List<Player> displayPlayers = util.RefreshPlayerList();

            if (sortByWinningsRadioButton.Checked)
            {
                util.Players.Sort(delegate (Player x, Player y)
                {
                    return y.Winnings.CalculateTotalWinnings().CompareTo(x.Winnings.CalculateTotalWinnings());
                });
            }
            else
            {
                util.Players.Sort();
            }

            displayResultsListBox.Items.AddRange(util.Players.ToArray());
        }


        //Allow user to select new default save/load location and display path
        private void filePathItem_Click(object sender, EventArgs e)
        {
            SpawnFileDialog();
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

        // exits application
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            //Exit application
            Application.Exit();
        }


    }
}
