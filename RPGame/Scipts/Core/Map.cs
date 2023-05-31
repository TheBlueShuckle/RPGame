using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System.Collections.Generic;
using System.Linq;

namespace RPGame.Scipts.Core
{
    internal class Map
    {
        const int TILE_PIXEL_COUNT = 16;

        public float TileSize { get; private set; }

        public Rectangle MapSize { get; set; }

        List<Tile> tiles = new List<Tile>();
        Vector2[,] tileGrid;
        Texture2D texture;
        Tile lastChangedTile, duplicateTile;

        public Map(Texture2D texture, int[] tileGridSize)
        {
            tileGrid = new Vector2[tileGridSize[0], tileGridSize[1]];
            this.texture = texture;
            TileSize = 16 * Main.Pixel;
            MapSize = new Rectangle(0, 0, (int)(tileGridSize[0] * TileSize), (int)(tileGridSize[1] * TileSize));

            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                for (int x = 0; x < tileGrid.GetLength(0); x++)
                {
                    tileGrid[x, y] = new Vector2(x * TileSize, y * TileSize);
                }
            }
        }

        public void GenerateMap(List<Tile> savedTiles)
        {
            tiles = savedTiles;
        }

        public void EditMap(Keys lastPressedKey, Vector2 mousePosition)
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (
                        new Rectangle((int)(x * TileSize), (int)(y * TileSize), (int)TileSize, (int)TileSize).Contains(mousePosition.X, mousePosition.Y) &&
                        (lastChangedTile == null || lastChangedTile.Rectangle != new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)) &&
                        Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        AddTile(lastPressedKey, x, y);
                    }

                    else if (
                        new Rectangle((int)(x * TileSize), (int)(y * TileSize), (int)TileSize, (int)TileSize).Contains(mousePosition.X, mousePosition.Y) &&
                        (lastChangedTile == null || lastChangedTile.Rectangle != new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)) &&
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
                case Keys.D1:
                    if (!IsDuplicate(new Tile(1, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT))))
                    {
                        tiles.Add(lastChangedTile = new Tile(1, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    else
                    {
                        ReplaceTile(lastChangedTile = new Tile(1, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    break;

                case Keys.D2:
                    if (!IsDuplicate(new Tile(2, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT))))
                    {
                        tiles.Add(lastChangedTile = new Tile(2, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    else
                    {
                        ReplaceTile(lastChangedTile = new Tile(2, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    break;

                case Keys.D3:
                    if (!IsDuplicate(new Tile(3, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT))))
                    {
                        tiles.Add(lastChangedTile = new Tile(3, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    else
                    {
                        ReplaceTile(lastChangedTile = new Tile(3, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    break;

                case Keys.D4:
                    if (!IsDuplicate(new Tile(4, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT))))
                    {
                         tiles.Add(lastChangedTile = new Tile(4, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    else
                    {
                        ReplaceTile(lastChangedTile = new Tile(4, new Rectangle(x * TILE_PIXEL_COUNT, y * TILE_PIXEL_COUNT, TILE_PIXEL_COUNT, TILE_PIXEL_COUNT)));
                    }

                    break;
            }
        }

        private void RemoveTile(int x, int y)
        {
            foreach (Tile tile in tiles.ToList())
            {
                if (tile.Position() == new Vector2(x * TILE_PIXEL_COUNT * Main.Pixel, y * TILE_PIXEL_COUNT * Main.Pixel))
                {
                    tiles.Remove(tile);
                }
            }
        }

        private bool IsDuplicate(Tile newTile)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.Position() == newTile.Position())
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
                if (neighboringTile.Position() == tile.Position() - new Vector2(TILE_PIXEL_COUNT, 0) ||
                    neighboringTile.Position() == tile.Position() + new Vector2(TILE_PIXEL_COUNT, 0) ||
                    neighboringTile.Position() == tile.Position() - new Vector2(0, TILE_PIXEL_COUNT) ||
                    neighboringTile.Position() == tile.Position() + new Vector2(0, TILE_PIXEL_COUNT))
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
                    spriteBatch.Draw(texture, new Rectangle((int)(x * TileSize), (int)(y * TileSize), (int)TileSize, (int)TileSize), Color.Black);
                    spriteBatch.Draw(texture, new Rectangle((int)(x * TileSize + 1), (int)(y * TileSize + 1), (int)TileSize - 2, (int)TileSize - 2), Color.CornflowerBlue);
                }
            }
        }
    }
}
