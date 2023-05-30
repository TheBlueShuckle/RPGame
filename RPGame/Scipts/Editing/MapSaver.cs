using Newtonsoft.Json;
using RPGame.Scipts.Components;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RPGame.Scipts.Editing
{
    internal class MapSaver
    {
        string fullPath;

        public MapSaver(string fileName)
        {
            fullPath = Path.GetFullPath(fileName);
        }

        public void SaveMap(List<Tile> tiles)
        {
            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(JsonConvert.SerializeObject(tiles));
            }
        }

        public List<Tile> LoadMap()
        {
            using (StreamReader file = new StreamReader(fullPath))
            {
                return JsonConvert.DeserializeObject<List<Tile>>(file.ReadToEnd());
            }
        }
    }
}
