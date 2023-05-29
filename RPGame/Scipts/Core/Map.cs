using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGame.Scipts.Core
{
    internal class Map
    {
        public int TileSize { get; set; }

        public Rectangle MapSize { get; set; }

        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid;
        Texture2D tileSet;
        Tile lastChangedTile, duplicateTile;

        public Map(Texture2D texture, int[] tileGridSize)
        {
            this.tileSet = texture;
            tileGrid = new Vector2[tileGridSize[0], tileGridSize[1]];

            TileSize = Main.ScreenWidth / 32;
            MapSize = new Rectangle(0, 0, tileGridSize[0] * TileSize, tileGridSize[1] * TileSize);

            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                for (int x = 0; x < tileGrid.GetLength(0); x++)
                {
                    tileGrid[x, y] = new Vector2(x * TileSize, y * TileSize);
                }
            }
        }

        public void GenerateMap(List<int> savedTiles)
        {
            for (int y = 0; y < 72; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    if (savedTiles.Count > 0)
                    {
                        int number = savedTiles.ElementAt(0);

                        if (number > 0)
                        {
                            tiles.Add(new Tile(tileSet, number, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        }

                        savedTiles.RemoveAt(0);
                    }
                }
            }
        }

        public void EditMap(Vector2 playerPos, Keys lastPressedKey, Vector2 mousePosition)
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (
                        mousePosition.X > x * TileSize &&
                        mousePosition.X < (x + 1) * TileSize &&
                        mousePosition.Y > y * TileSize &&
                        mousePosition.Y < (y + 1) * TileSize &&
                        (lastChangedTile == null || lastChangedTile.Rectangle != new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))  &&
                        Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        AddTile(lastPressedKey, x, y);
                    }

                    else if (
                        mousePosition.X > x * TileSize &&
                        mousePosition.X < (x + 1) * TileSize &&
                        mousePosition.Y > y * TileSize &&
                        mousePosition.Y < (y + 1) * TileSize &&
                        (lastChangedTile == null || lastChangedTile.Rectangle != new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)) &&
                        Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        RemoveTile(x, y);
                    }
                }
            }
        }

        private void AddTile(Keys lastPressedKey, int x, int y)
        {
            switch (lastPressedKey)
            {
                case Keys.V:
                    if (!IsDuplicate(new Tile(tileSet, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                    {
                        tiles.Add(new Tile(tileSet, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        lastChangedTile = new Tile(tileSet, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                    }

                    else
                    {
                        ReplaceTile(new Tile(tileSet, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                    }

                    break;

                case Keys.B:
                    if (!IsDuplicate(new Tile(tileSet, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                    {
                        tiles.Add(new Tile(tileSet, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        lastChangedTile = new Tile(tileSet, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                    }

                    else
                    {
                        ReplaceTile(new Tile(tileSet, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                    }

                    break;

                case Keys.N:
                    if (!IsDuplicate(new Tile(tileSet, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                    {
                        tiles.Add(new Tile(tileSet, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        lastChangedTile = new Tile(tileSet, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                    }

                    else
                    {
                        ReplaceTile(new Tile(tileSet, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                    }

                    break;

                case Keys.M:
                    if (!IsDuplicate(new Tile(tileSet, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                    {
                        tiles.Add(new Tile(tileSet, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        lastChangedTile = new Tile(tileSet, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                    }

                    else
                    {
                        ReplaceTile(new Tile(tileSet, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                    }

                    break;
            }
        }

        private void RemoveTile(int x, int y)
        {
            foreach (Tile tile in tiles.ToList())
            {
                if (tile.Position == new Vector2(x * TileSize, y * TileSize))
                {
                    tiles.Remove(tile);
                }
            }
        }

        private bool IsDuplicate(Tile newTile)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.Position == newTile.Position)
                {
                    duplicateTile = tile;
                    return true;
                }
            }

            return false;
        }

        private void ReplaceTile(Tile newTile)
        {
            tiles.Insert(tiles.IndexOf(duplicateTile), newTile);
            tiles.Remove(duplicateTile);
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
                if (neighboringTile.Position == tile.Position - new Vector2(TileSize, 0) ||
                    neighboringTile.Position == tile.Position + new Vector2(TileSize, 0) ||
                    neighboringTile.Position == tile.Position - new Vector2(0, TileSize) ||
                    neighboringTile.Position == tile.Position + new Vector2(0, TileSize))
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
                    spriteBatch.Draw(tileSet, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), Color.Black);
                    spriteBatch.Draw(tileSet, new Rectangle(x * TileSize + 1, y * TileSize + 1, TileSize - 2, TileSize - 2), Color.CornflowerBlue);
                }
            }
        }
    }
}
