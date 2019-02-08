using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Keynvaders
{
    /// <summary>
    /// Data handler for KeyNvaders game
    /// <author>Tomás Cardenal López</author>
    /// </summary>
    public class KnVDataHandler
    {
        /// <summary>
        /// Represents a high score
        /// </summary>
        public struct HighScore
        {
            /// <summary>
            /// The high score name
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// The high score score
            /// </summary>
            public int Score { get; set; }

            /// <summary>
            /// Initializes the fields to it's parameters
            /// </summary>
            /// <param name="name">The name</param>
            /// <param name="score">The score</param>
            public HighScore(string name, int score)
            {
                this.Name = name;
                this.Score = score;
            }
        }

        /// <summary>
        /// The high score list
        /// </summary>
        public List<HighScore> HighScoreList;

        /// <summary>
        /// Main directory path
        /// </summary>
        private string dir;

        /// <summary>
        /// Initializes a default data handler for KeyNvaders game
        /// </summary>
        public KnVDataHandler()
        {
            CreateDataDirectory();
            if (!LoadFromXml())
            {
                HighScoreList = new List<HighScore>();
            }
        }

        /// <summary>
        /// Checks if a score is a high score on the top 10
        /// </summary>
        /// <param name="score">The score to check</param>
        /// <returns>True if it's a high score, false if not</returns>
        public bool IsHighScore(int score)
        {
            return checkHighScorePosition(score) != -1 ? true : false;
        }

        /// <summary>
        /// Checks the position to put the high score into
        /// </summary>
        /// <param name="score">The score to check</param>
        /// <returns>The position to put the high score, -1 if it shouldn't be added</returns>
        private int checkHighScorePosition(int score)
        {
            int position = -1;
            for (int i = HighScoreList.Count - 1; i >= 0; i--)
            {
                if (HighScoreList[i].Score < score)
                {
                    position = i;
                }
            }
            if (position == -1 && HighScoreList.Count < 10)
            {
                return HighScoreList.Count;
            }
            else
            {
                return position;
            }
        }


        /// <summary>
        /// Adds (if possible) the high score to the list
        /// </summary>
        /// <param name="name">The high score name</param>
        /// <param name="score">The high score score</param>
        /// <returns>True if high score was added, false if it wasn't</returns>
        public bool AddHighScore(string name, int score)
        {
            int index = checkHighScorePosition(score);
            if (index != -1 || index <= HighScoreList.Count)
            {
                HighScoreList.Insert(index, new HighScore(name, score));
                if (HighScoreList.Count > 10)
                {
                    HighScoreList.RemoveRange(10, HighScoreList.Count - 10);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates (if necessary) a new data directory 
        /// </summary>
        /// <returns>True if a directory was created, false if it wasnt</returns>
        public bool CreateDataDirectory()
        {
            string path = Environment.GetEnvironmentVariable("HOMEPATH");
            path = Path.Combine(path, "AppData");
            path = Path.Combine(path, "Keynvaders");
            this.dir = Path.Combine(path, "hsKnv");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.WriteLine("Directory created on " + path);
                return true;
            }
            Debug.WriteLine("Directory on " + path + " already exists");
            return false;
        }

        /// <summary>
        /// Saves the list to an xml file
        /// </summary>
        /// <returns>True if added correctly, false if InvalidOperationException was caught</returns>
        public bool SaveToXml()
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(dir))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
                    serializer.Serialize(writer, HighScoreList);
                    //writer.Flush();
                    //writer.Close();
                    return true;
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message + " on SaveToXML");
                return false;
            }
        }

        /// <summary>
        /// Loads the XML into the high score list
        /// </summary>
        /// <returns>True if loaded correctly, false an exception was caught</returns>
        public bool LoadFromXml()
        {
            try
            {
                using (StreamReader reader = new StreamReader(dir))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
                    List<HighScore> highScores = (List<HighScore>)serializer.Deserialize(reader);
                    //reader.Close();
                    this.HighScoreList = highScores;
                    return true;
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message + " on LoadFromXML");
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.WriteLine(e.Message + " on LoadFromXML");
                return false;
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(e.Message + " on LoadFromXML");
                return false;
            }
        }
    }
}
