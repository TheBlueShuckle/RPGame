using Newtonsoft.Json;
using RPGame.Scipts.Components;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RPGame.Scipts.Editing
{
    internal class MapSaver
    {
        List<Tile> tiles = new List<Tile>();
        StreamWriter writer;
        StreamReader reader;

        string fullPath;

        public MapSaver(string fileName)
        {
            fullPath = Path.GetFullPath(fileName);
        }

        public void SaveMap(List<Tile> tiles)
        {
            using (StreamWriter file = new StreamWriter(fullPath))
            {
                foreach (Tile tile in tiles)
                {
                    file.WriteLine(JsonConvert.SerializeObject(tile));
                }
            }

            writer.Close();
        }

        public List<int> LoadMap()
        {
            reader = new StreamReader(fullPath);
            List<int> result = new List<int>();

            while(!reader.EndOfStream)
            {
                result.Add(int.Parse(reader.ReadLine()));
            }

            reader.Close();
            return result;
        }
    }
}
