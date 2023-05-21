using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using RPGame.Scipts.Components;
using RPGame.Scipts.Core;
using RPGame.Scipts.Sprites.Enemies;
using RPGame.Scipts.Editing;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Metadata;

namespace RPGame.Scipts.Scenes
{
    internal class Scene1 : Scene
    {
        const string FILE_NAME = "Scene1.txt";

        Player player;
        Map map;
        MapSaver mapSaver;

        List<Tile> tiles, impassableTiles;
        List<Enemy> enemies = new List<Enemy>();
        List<Component> components;

        KeyboardState ks1, ks2;
        Keys lastPressedKey;
        Texture2D texture;
        SpriteFont font;
        int tilesStartCount;

        private Camera camera;


        public Scene1() : base()
        {

        }

        public override void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            LoadTextures(GraphicsDevice, Content);

            map = new Map(texture, new int[] { 128, 72 });
            mapSaver = new MapSaver();
            map.GenerateMap(mapSaver.LoadMap(FILE_NAME));

            camera = new Camera(map.MapSize);

            tiles = map.GetTiles();
            tilesStartCount = map.GetTiles().Count;
            impassableTiles = map.GetImpassableTiles();

            player = new Player(map.TileSize, impassableTiles, texture);
            components = new List<Component>();

            foreach (Tile tile in tiles)
            {
                components.Add(tile);
            }

            foreach (Enemy enemy in enemies)
            {
                components.Add(enemy);
            }

            components.Add(player);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Count() != 0)
            {
                lastPressedKey = Keyboard.GetState().GetPressedKeys()[0];
            }

            if (Main.EditMode)
            {
                map.EditMap(player.Position, lastPressedKey);

                if (tilesStartCount != map.GetTiles().Count)
                {
                    AddNewTiles();
                    RemoveDeletedTiles();
                    tilesStartCount = tiles.Count;
                }
            }

            foreach (Component component in components)
            {
                component.Update(gameTime);
            }

            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.F1) && ks2.IsKeyUp(Keys.F1))
            {
                mapSaver.SaveMap(FILE_NAME, map.GetTiles());
            }

            ks2 = ks1;

            camera.Follow(player);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int elementsOnScreen = 0;

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform);

            map.Draw(spriteBatch);

            foreach (Component component in components)
            {
                if (IsComponentOnScreen(component))
                {
                    component.Draw(gameTime, spriteBatch);
                    elementsOnScreen++;
                }
            }

            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "" + elementsOnScreen, Vector2.Zero, Color.Black);

            spriteBatch.End();
        }

        private bool IsComponentOnScreen(Component component)
        {
            bool isOnScreen = false;

            if (
                component.Rectangle.X >= player.GetCenter().X - (Main.ScreenWidth / 2) &&
                component.Rectangle.Right <= player.GetCenter().X + (Main.ScreenWidth / 2) &&
                component.Rectangle.Y >= player.GetCenter().Y - (Main.ScreenHeight / 2) &&
                component.Rectangle.Bottom <= player.GetCenter().Y + (Main.ScreenHeight / 2))
            {
                isOnScreen = true;
            }

            if (
                // if component.X is in range 
                player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                component.Rectangle.Right < Main.ScreenWidth &&
                  // if camera sees top of screen and component.Y is in range
                ((player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  component.Position.Y < Main.ScreenHeight) ||
                 // else if camera sees bottom of screen and component.Y is in range
                 (player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  component.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight)) ||
                 // else if camera sees neither the bottom or the top and component.Y is in range
                 (player.Position.Y > map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  player.Position.Y < map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  component.Position.Y < player.Position.Y + (Main.ScreenHeight / 2) &&
                  component.Position.Y > player.Position.Y - (Main.ScreenHeight / 2))))
            {
                isOnScreen = true;
            }

            if (
                player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                component.Rectangle.X > map.MapSize.Right - Main.ScreenWidth &&
                  // if camera sees top of screen and component.Y is in range
                ((player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  component.Position.Y < Main.ScreenHeight) ||
                  // else if camera sees bottom of screen and component.Y is in range
                 (player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  component.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight)) ||
                  // else if camera sees neither the bottom or the top and component.Y is in range
                 (player.Position.Y > map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  player.Position.Y < map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  component.Position.Y < player.Position.Y + (Main.ScreenHeight / 2) &&
                  component.Position.Y > player.Position.Y - (Main.ScreenHeight / 2))))

            {
                isOnScreen = true;
            }
            
            if (
                player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                component.Rectangle.Bottom < Main.ScreenHeight &&                   
                  // if camera sees left of screen and component.X is in range
                ((player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                  component.Position.X < Main.ScreenWidth) ||
                 // else if camera sees right of screen and component.X is in range
                 (player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  component.Position.X > map.MapSize.Right - (Main.ScreenWidth)) ||
                 // else if camera sees neither the bottom or the top and component.X is in range
                 (player.Position.X > map.MapSize.X + (Main.ScreenWidth / 2) &&
                  player.Position.X < map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  component.Position.X < player.Position.X + (Main.ScreenWidth / 2) &&
                  component.Position.X > player.Position.X - (Main.ScreenWidth / 2))))
            {
                isOnScreen = true;
            }

            if (
                player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                component.Rectangle.Y > map.MapSize.Bottom - Main.ScreenHeight && 
                 // if camera sees left of screen and component.X is in range
                ((player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                  component.Position.X < Main.ScreenWidth) ||
                 // else if camera sees right of screen and component.X is in range
                 (player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  component.Position.X > map.MapSize.Right - (Main.ScreenWidth)) ||
                 // else if camera sees neither the bottom or the top and component.X is in range
                 (player.Position.X > map.MapSize.X + (Main.ScreenWidth / 2) &&
                  player.Position.X < map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  component.Position.X < player.Position.X + (Main.ScreenWidth / 2) &&
                  component.Position.X > player.Position.X - (Main.ScreenWidth / 2))))
            {
                isOnScreen = true;
            }

            return isOnScreen;
        }

        protected override void LoadTextures(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            font = Content.Load<SpriteFont>("Font/Font");
        }

        private void AddNewTiles()
        {
            foreach (Tile tile in map.GetTiles().ToList())
            {
                if (!components.Contains(tile))
                {
                    components.Add(tile);
                    RemoveDuplicates(tile);
                    map.SeeIfNewTileHasCollision(tile);
                }
            }

            components.Remove(player);
            components.Insert(components.Count, player);
        }

        private void RemoveDeletedTiles()
        {
            foreach (Tile tile in tiles.ToList())
            {
                if (!map.GetTiles().Contains(tile))
                {
                    tiles.Remove(tile);
                }
            }
        }


        private void RemoveDuplicates(Tile tile)
        {
            foreach (Tile oldTile in tiles.ToList())
            {
                if (oldTile.Position == tile.Position && oldTile != tile)
                {
                    tiles.Remove(oldTile);
                }
            }
        }
    }
}
