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

namespace PokerTournament
{
    public partial class newPlayerForm : Form
    {
        public newPlayerForm()
        {
            InitializeComponent();
        }

        string path = @"C:\Poker Tournament\Players.txt";

        //cancel button closes form
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        //Save+close button
        private void saveBtn_Click(object sender, EventArgs e)
        {
            //create a new person object
            Person newPerson = new Person(firstNameBox.Text, lastNameBox.Text, Convert.ToInt16(ssnBox.Text));

            //if a players file doesn't exist, create one and write player to it
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(newPerson.ToString());
                }
            }
            //if the file exists, add the player on a new line
            else {               
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(newPerson.ToString());
                }

                //Confirm that the data was saved, and close the window
                MessageBox.Show("The player has been saved, thank you!", "Save Confirmation");
                Form.ActiveForm.Close();
            }
        }
    }
}
