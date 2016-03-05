using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTournament
{
    class DataUtility
    {
        // fields
        private FileStream inFile;
        private FileStream outFile;
        private BinaryFormatter bformatter;
        public List<Player> Players { get; set; }
        const string DEFAULT_FILEPATH = @"FilePath.txt";
        private string path;
        public string Path { get { return path; }
            set
            {
                path = value;

                // The value set to path is also saved to a file to prevent forcing the user to pick the file location every time the program is used
                try
                {
                    outFile = new FileStream(DEFAULT_FILEPATH, FileMode.OpenOrCreate, FileAccess.Write); // A FileStream is declared and set to write
                    StreamWriter writer = new StreamWriter(outFile);
                    writer.WriteLine(value); 
                    writer.Close();
                    outFile.Close();
                }

                catch (Exception)
                {
                    // If an exception is thrown, the program continues without handling it. Setting the user path to the saved
                    // value is not required for the program to function.
                }
            }
        }
        
        // constructor pre-instantiates binary formatter
        public DataUtility()
        {
            bformatter = new BinaryFormatter();

            // The Path is set to the saved value when an object is created
            try
            {
                inFile = new FileStream(DEFAULT_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                path = reader.ReadLine();
                reader.Close();
                inFile.Close();
            }
            catch (Exception)
            {
                // If an exception is thrown, the program continues without handling it. Setting the user path to the saved
                // value is not required for the program to function.
            }
        }

        // updates Players from file
        public void RefreshPlayerList()
        {
            Players = new List<Player>();

            try
            {
                inFile = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                while (inFile.Position < inFile.Length)
                {
                    Player newPlayer = (Player) bformatter.Deserialize(inFile);
                    Players.Add(newPlayer);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            finally
            {
                inFile.Close();
            }

            //return Players;
        }

        // saves Players to file
        public void SavePlayers()
        {
            try
            {
                outFile = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
                outFile.SetLength(0);
                foreach (Player x in Players)
                {
                    bformatter.Serialize(outFile, x);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Error");
            }
            finally
            {
                outFile.Close();
            }
        }

        // returns true if duplicate player is found
        // and removes that player from the players list
        public bool IsDupcliatePlayer(Player newPlayer)
        {
            foreach (Player existingPlayer in Players)
            {
                if (existingPlayer.Equals(newPlayer))
                {
                    Players.Remove(existingPlayer);
                    return true;
                }
            }

            return false;
        }

        // searches for player by ssn and returns
        // index if found, or -1 if not found
        public int SearchPlayersBySSN(int ssn)
        {
            foreach (Player player in Players)
            {
                if (ssn == player.ssn)
                {
                    return Players.IndexOf(player);
                }
            }

            return -1;
        }
    }
}
