using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Map : Main
    {
        int tileSize;
        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid = new Vector2[64, 36];

        public Map(Rectangle windowSize)
        {
            tileSize = windowSize.Width / 64;

            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                for (int x = 0; x < tileGrid.GetLength(0); x++)
                {
                    tileGrid[x, y] = new Vector2(x * tileSize, y * tileSize);
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
                        tiles.Add(new Tile(number, new Rectangle(y * tileSize, x * tileSize, tileSize, tileSize)));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Vector2 tilePos in tileGrid)
            {
                spriteBatch.Draw(texture, new Rectangle(tilePos.ToPoint(), new Point(tileSize, tileSize)), Color.Black);
                spriteBatch.Draw(texture, new Rectangle(new Point((int)tilePos.X + 1, (int)tilePos.Y + 1), new Point(tileSize - 2, tileSize - 2)), Color.CornflowerBlue);
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

        public int GetTileSize()
        {
            return tileSize;
        }
    }
}
