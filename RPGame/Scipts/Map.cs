using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Map : Main
    {
        static int tileLength { get; set; }

        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid = new Vector2[40, 30];

        public Map()
        {
            tileLength = Window.ClientBounds.Width / 20;

            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                for (int x = 0; x < tileGrid.GetLength(0); x++)
                {
                    tileGrid[x, y] = new Vector2(x * tileLength, y * tileLength);
                }
            }
        }

        public void GenerateMap(int[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int number = map[x, y];

                    if (number > 0)
                    {
                        tiles.Add(new Tile(number, new Rectangle(y * tileLength, x * tileLength, tileLength, tileLength)));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Vector2 tilePos in tileGrid)
            {
                spriteBatch.Draw(texture, new Rectangle(tilePos.ToPoint(), new Point(tileLength, tileLength)), Color.Black);
                spriteBatch.Draw(texture, new Rectangle(new Point((int)tilePos.X + 1, (int)tilePos.Y + 1), new Point(tileLength - 2, tileLength - 2)), Color.CornflowerBlue);
            }

            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch, texture);
            }
        }

        public List<Tile> GetImpassableTiles()
        {
            List<Tile> impassableTiles = new List<Tile>();

            foreach (Tile tile in tiles)
            {
                if (tile.GetPassability() == false)
                {
                    impassableTiles.Add(tile);
                }
            }

            return impassableTiles;
        }
    }
}
