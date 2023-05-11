using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace RPGame.Scipts
{
    internal class Map : Main
    {
        public int TileSize { get; set; }
        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid = new Vector2[64, 36];

        public Map(Rectangle windowSize)
        {
            TileSize = windowSize.Width / 64;

            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                for (int x = 0; x < tileGrid.GetLength(0); x++)
                {
                    tileGrid[x, y] = new Vector2(x * TileSize, y * TileSize);
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
                        tiles.Add(new Tile(number, new Rectangle(y * TileSize, x * TileSize, TileSize, TileSize)));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Vector2 tilePos in tileGrid)
            {
                spriteBatch.Draw(texture, new Rectangle(tilePos.ToPoint(), new Point(TileSize, TileSize)), Color.Black);
                spriteBatch.Draw(texture, new Rectangle(new Point((int)tilePos.X + 1, (int)tilePos.Y + 1), new Point(TileSize - 2, TileSize - 2)), Color.CornflowerBlue);
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
                List<Tile> neighboringTiles = GetNeighboringTiles(tile);

                if (neighboringTiles.Count < 4)
                {
                    if (tile.GetPassability() == false)
                    {
                        impassableTiles.Add(tile);
                        //tile.SetColor();
                    }
                }

                else
                {
                    int impassableNeighboringTiles = 0;

                    foreach(Tile neighboringTile in neighboringTiles)
                    {
                        if (neighboringTile.GetPassability() == false)
                        {
                            impassableNeighboringTiles++;
                        }
                    }

                    if (tile.GetPassability() == false && impassableNeighboringTiles < 4)
                    {
                        impassableTiles.Add(tile);
                        //tile.SetColor();
                    }
                }
            }

            return impassableTiles;
        }

        private List<Tile> GetNeighboringTiles(Tile tile)
        {
            List<Tile> neighboringTiles = new List<Tile>();

            foreach(Tile neighboringTile in tiles)
            {
                if (neighboringTile.GetPosition() == tile.GetPosition() - new Vector2(TileSize, 0) ||
                    neighboringTile.GetPosition() == tile.GetPosition() + new Vector2(TileSize, 0) ||
                    neighboringTile.GetPosition() == tile.GetPosition() - new Vector2(0, TileSize) ||
                    neighboringTile.GetPosition() == tile.GetPosition() + new Vector2(0, TileSize))
                {
                    neighboringTiles.Add(neighboringTile);
                }
            }

            return neighboringTiles;
        }
    }
}
