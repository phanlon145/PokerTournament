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

        public MainForm()
        {
            InitializeComponent();
        }

        string path = null;     

        private void newPlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        //Save+close button
        private void saveBtn_Click_1(object sender, EventArgs e)
        {

            if (path == null)
            {
                SpawnFileDialog();
            }

            List<Player> players = new List<Player>();

            players = GetPlayers();

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

            for (int q = 0; q < players.Count; ++q)
            {
                if (players[q].Equals(newPlayer))
                {
                    MessageBox.Show("That employee already exists\nThe existing employee has been updated", "Duplicate employee message");
                    players.Remove(players[q]);
                }
            }
            players.Add(newPlayer);

            SavePlayers(players);

            //Confirm that the data was saved, and close the window
            MessageBox.Show("The player has been saved, thank you!", "Save Confirmation");
            TabPage t = tabControl1.TabPages[1];
            tabControl1.SelectedTab = t; //go to tab
            firstNameBox.Clear();
            lastNameBox.Clear();
            ssnBox.Clear();

            }

        List<Player> GetPlayers()
        {
            // This method gets all of the saved players or creates a file to store them if it does not exist
            FileStream infile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            List<Player> players = new List<Player>();
            BinaryFormatter bformatter = new BinaryFormatter();
            while (infile.Position < infile.Length)
            {
                Player newPlayer = (Player)bformatter.Deserialize(infile);
                players.Add(newPlayer);
            }

            infile.Close();

            return players;
        }

        void SavePlayers(List<Player> players)
        {

            FileStream outfile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            // The file is deleted with this next statement
            outfile.SetLength(0);

            BinaryFormatter bformatter = new BinaryFormatter();

            // The updated list of employees is written to the file
            foreach (Player x in players)
            {
                bformatter.Serialize(outfile, x);
            }

            outfile.Close();
        }

        //Allows user to specify file path
        void SpawnFileDialog()
        {
            folderBrowserDialog.ShowDialog();
            path = folderBrowserDialog.SelectedPath + "\\Players.txt";
            currentPathBox.Text = "Current File Path: " + path;
        }
        
        
        
        
        //cancel button closes form
        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //check to see if file path specified, if not, prompt user
            if (path == null)
            {
                MessageBox.Show("Please specify player file location", "Select File");
                SpawnFileDialog();
            }

            List<Player> searchPlayers = new List<Player>();

            searchPlayers = GetPlayers();

            bool found = false;

            for (int i = 0; i < searchPlayers.Count; i++)
            {
                if (Convert.ToInt32(ssnSearchForTextBox.Text) == searchPlayers[i].ssn)
                {
                    selectPlayer.Text = searchPlayers[i].FirstName + " " + searchPlayers[i].LastName;
                    currentPlayer = searchPlayers[i];
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
            //check to see if file path specified, if not, prompt user
            if (path == null)
            {
                MessageBox.Show("Please specify player file location", "Select File");
                SpawnFileDialog();
                
            }
            
            // The update button uses the selected player index set with the search button to update winnings for a player
            List<Player> updatePlayers = new List<Player>();

            updatePlayers = GetPlayers();

            int currentWeek = Convert.ToInt16(weekBox.Text) - 1;

            currentPlayer.Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);
            updatePlayers[playerPos].Winnings.Weeks[currentWeek].Winning = Convert.ToInt32(winningsBox.Text);

            currentPlayer.Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);
            updatePlayers[playerPos].Winnings.Weeks[currentWeek].Location = new Location(casinoBox.Text, stateBox.Text);

            totalWinnings.Text = updatePlayers[playerPos].Winnings.CalculateTotalWinnings().ToString();

            SavePlayers(updatePlayers);
        }
        
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            displayResultsListBox.Items.Clear();

            if (path == null)
            {
                MessageBox.Show("Please specify player file location", "Select File");
                SpawnFileDialog();
            }

            List<Player> displayPlayers = new List<Player>();

            displayPlayers = GetPlayers();

            if (sortByWinningsRadioButton.Checked == true)
            {
                displayPlayers.Sort(delegate (Player x, Player y)
                {
                    return y.Winnings.CalculateTotalWinnings().CompareTo(x.Winnings.CalculateTotalWinnings());
                });
            }
            else
            {
                displayPlayers.Sort();
            }

            displayResultsListBox.Items.AddRange(displayPlayers.ToArray());
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            //Exit application
            Application.Exit();
        }

        //Allow user to select new default save/load location and display path
        private void filePathItem_Click(object sender, EventArgs e)
        {
            SpawnFileDialog();
        }

        private void weekBox_TextChanged(object sender, EventArgs e)
        {
            winningsBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Winning.ToString();
            stateBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Location.State;
            casinoBox.Text = currentPlayer.Winnings.Weeks[Convert.ToInt16(weekBox.Text) - 1].Location.Name;
        }

        private void currentPathBox_Click(object sender, EventArgs e)
        {

        }
    }
}
