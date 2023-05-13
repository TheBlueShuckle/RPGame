using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGame.Scipts.Components;
using System.Collections.Generic;

namespace RPGame.Scipts.Core
{
    internal class Map
    {
        public int TileSize { get; set; }

        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid = new Vector2[64, 36];
        Texture2D texture;

        public Map(Texture2D texture)
        {
            this.texture = texture;
            TileSize = Main.ScreenWidth / 64;

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
                        tiles.Add(new Tile(texture, number, new Rectangle(y * TileSize, x * TileSize, TileSize, TileSize)));
                    }
                }
            }
        }

        public List<Tile> GetTiles() { return tiles; }

        public List<Tile> GetImpassableTiles()
        {
            List<Tile> impassableTiles = new List<Tile>();

            foreach (Tile tile in tiles)
            {
                List<Tile> neighboringTiles = GetNeighboringTiles(tile);

                if (tile.GetPassability() == false && neighboringTiles.Count < 4)
                {
                    impassableTiles.Add(tile);
                    //tile.SetColor();
                }

                else if (tile.GetPassability() == false && AmountOfImpassableTiles(neighboringTiles) < 4)
                {
                    impassableTiles.Add(tile);
                    //tile.SetColor();
                }
            }

            return impassableTiles;
        }

        private List<Tile> GetNeighboringTiles(Tile tile)
        {
            List<Tile> neighboringTiles = new List<Tile>();

            foreach (Tile neighboringTile in tiles)
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

        private int AmountOfImpassableTiles(List<Tile> neighboringTiles)
        {
            int impassableNeighboringTiles = 0;

            foreach (Tile neighboringTile in neighboringTiles)
            {
                if (neighboringTile.GetPassability() == false)
                {
                    impassableNeighboringTiles++;
                }
            }

            return impassableNeighboringTiles;
        }
    }
}
