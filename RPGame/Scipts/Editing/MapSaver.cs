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

        public MapSaver()
        {

        }

        public void SaveMap(string fileName, List<Tile> tiles)
        {
            FileStream overwriteFile = File.Open("C:\\Users\\ville\\source\\repos\\RPGame\\RPGame\\TileMaps\\" + fileName, FileMode.Create);

            writer = new StreamWriter(overwriteFile);

            foreach(Tile tile in tiles)
            {
                writer.WriteLine(tile.Material);
            }

            writer.Close();
        }

        public List<int> LoadMap(string fileName)
        {
            reader = new StreamReader("C:\\Users\\ville\\source\\repos\\RPGame\\RPGame\\TileMaps\\" + fileName);
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
