using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public string Path { get; set; }
        

        public DataUtility()
        {
            bformatter = new BinaryFormatter(); 
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
    }
}
