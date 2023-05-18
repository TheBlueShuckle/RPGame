﻿using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System;
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

        public void GenerateMap(List<int> savedTiles)
        {
            for (int y = 0; y < 39; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    if (savedTiles.Count > 0)
                    {
                        int number = savedTiles.ElementAt(0);

                        if (number > 0)
                        {
                            tiles.Add(new Tile(texture, number, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                        }

                        savedTiles.RemoveAt(0);
                    }
                }
            }
        }

        public void EditMap(Vector2 playerPos, Keys lastPressedKey)
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (
                        Mouse.GetState().Position.X - (Main.ScreenWidth / 2 - playerPos.X - 10) > x * TileSize && 
                        Mouse.GetState().Position.X - (Main.ScreenWidth / 2 - playerPos.X - 10) < (x + 1) * TileSize && 
                        Mouse.GetState().Position.Y - (Main.ScreenHeight / 2 - playerPos.Y - 25) > y * TileSize && 
                        Mouse.GetState().Position.Y - (Main.ScreenHeight / 2 - playerPos.Y - 25) < (y + 1) * TileSize && 
                        Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        switch (lastPressedKey)
                        {
                            case Keys.V:
                                if (!IsDuplicate(new Tile(texture, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                                {
                                    tiles.Add(new Tile(texture, 1, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                }
                                break;

                            case Keys.B:
                                if(!IsDuplicate(new Tile(texture, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                                {
                                    tiles.Add(new Tile(texture, 2, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                }
                                break;

                            case Keys.N:
                                if (!IsDuplicate(new Tile(texture, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                                {
                                    tiles.Add(new Tile(texture, 3, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                }
                                break;

                            case Keys.M:
                                if (!IsDuplicate(new Tile(texture, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize))))
                                {
                                    tiles.Add(new Tile(texture, 4, new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize)));
                                }
                                break;
                        }
                    }

                    else if (
                        Mouse.GetState().Position.X - (Main.ScreenWidth / 2 - playerPos.X - 10) > x * TileSize &&
                        Mouse.GetState().Position.X - (Main.ScreenWidth / 2 - playerPos.X - 10) < (x + 1) * TileSize &&
                        Mouse.GetState().Position.Y - (Main.ScreenHeight / 2 - playerPos.Y - 25) > y * TileSize &&
                        Mouse.GetState().Position.Y - (Main.ScreenHeight / 2 - playerPos.Y - 25) < (y + 1) * TileSize &&
                        Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        foreach(Tile tile in tiles.ToList())
                        {
                            if (tile.GetPosition() == new Vector2(x * TileSize, y * TileSize))
                            {
                                tiles.Remove(tile);
                            }
                        }
                    }
                }
            }
        }

        private bool IsDuplicate(Tile newTile)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.GetPosition() == newTile.GetPosition())
                {
                    tiles.Insert(tiles.IndexOf(tile), newTile);
                    return true;
                }
            }

            return false;
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

        public bool SeeIfNewTileHasCollision(Tile tile)
        {
            if (tile.GetPassability() == false && GetNeighboringTiles(tile).Count < 4)
            {
                //tile.SetColor();
                return true;
            }

            else if (tile.GetPassability() == false && AmountOfImpassableTiles(GetNeighboringTiles(tile)) < 4)
            {
                //tile.SetColor();
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
