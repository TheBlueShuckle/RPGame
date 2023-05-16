using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace RPGame.Scipts.Core
{
    internal class Map
    {
        public int TileSize { get; set; }

        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid;
        Texture2D texture;

        public Map(Texture2D texture, int[] tileGridSize)
        {
            this.texture = texture;
            tileGrid = new Vector2[tileGridSize[0], tileGridSize[1]];

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

        public void EditMap(Vector2 playerPos)
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (playerPos.X > x * TileSize && playerPos.X < (x + 1) * TileSize && playerPos.Y > y * TileSize && playerPos.Y < (y + 1) * TileSize && Keyboard.GetState().GetPressedKeys().Count() != 0)
                    {
                        switch (Keyboard.GetState().GetPressedKeys()[0])
                        {
                            case Keys.V:
                                tiles.Add(new Tile(texture, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                break;
                            case Keys.B:
                                tiles.Add(new Tile(texture, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                break;
                            case Keys.N:
                                tiles.Add(new Tile(texture, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                break;
                            case Keys.M:
                                tiles.Add(new Tile(texture, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                break;
                        }
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
                    tile.SetColor();
                }

                else if (tile.GetPassability() == false && AmountOfImpassableTiles(neighboringTiles) < 4)
                {
                    impassableTiles.Add(tile);
                    tile.SetColor();
                }
            }

            return impassableTiles;
        }

        public bool SeeIfNewTileHasCollision(Tile tile)
        {
            if (tile.GetPassability() == false && GetNeighboringTiles(tile).Count < 4)
            {
                tile.SetColor();
                return true;
            }

            else if (tile.GetPassability() == false && AmountOfImpassableTiles(GetNeighboringTiles(tile)) < 4)
            {
                tile.SetColor();
                return true;
            }

            return false;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    spriteBatch.Draw(texture, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), Color.Black);
                    spriteBatch.Draw(texture, new Rectangle(x * TileSize + 1, y * TileSize + 1, TileSize - 2, TileSize - 2), Color.CornflowerBlue);
                }
            }
        }
    }
}
