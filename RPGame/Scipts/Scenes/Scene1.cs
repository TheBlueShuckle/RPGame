﻿using Microsoft.Xna.Framework;
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
using System.ComponentModel;

namespace RPGame.Scipts.Scenes
{
    internal class Scene1 : Scene
    {
        const string FILE_NAME = "TileMaps\\Scene1.txt";

        Player player;
        Map map;
        MapSaver mapSaver;

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

            tilesStartCount = map.GetTiles().Count;

            player = new Player(map.TileSize, map.GetImpassableTiles(), texture);
            components = new List<Component>();

            enemies.Add(new Slime(map.TileSize, new Vector2(map.TileSize, map.TileSize), texture));

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
            int tilesOnScreen = 0;

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform);

            map.Draw(spriteBatch);

            foreach (Tile tile in map.GetTiles())
            {
                if (IsTileOnScreen(tile))
                {
                    tile.Draw(gameTime, spriteBatch);
                    tilesOnScreen++;
                }
            }

            foreach (Component component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();


            spriteBatch.Begin();

            spriteBatch.DrawString(font, "" + tilesOnScreen, Vector2.Zero, Color.Black);

            spriteBatch.End();
        }

        private bool IsTileOnScreen(Tile tile)
        {
            bool isOnScreen = false;

            if (
                tile.Rectangle.X + map.TileSize >= player.GetCenter().X - (Main.ScreenWidth / 2) &&
                tile.Rectangle.Right - map.TileSize <= player.GetCenter().X + (Main.ScreenWidth / 2) &&
                tile.Rectangle.Y + map.TileSize >= player.GetCenter().Y - (Main.ScreenHeight / 2) &&
                tile.Rectangle.Bottom - map.TileSize <= player.GetCenter().Y + (Main.ScreenHeight / 2))
            {
                isOnScreen = true;
            }

            if (
                // if component.X is in range 
                player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                tile.Rectangle.Right - map.TileSize < Main.ScreenWidth &&
                  // if camera sees top of screen and component.Y is in range
                ((player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  tile.Position.Y - map.TileSize < Main.ScreenHeight) ||
                 // else if camera sees bottom of screen and component.Y is in range
                 (player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  tile.Position.Y + map.TileSize > map.MapSize.Bottom - (Main.ScreenHeight)) ||
                 // else if camera sees neither the bottom or the top and component.Y is in range
                 (player.Position.Y > map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  player.Position.Y < map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  tile.Position.Y - map.TileSize < player.Position.Y + (Main.ScreenHeight / 2) &&
                  tile.Position.Y + map.TileSize > player.Position.Y - (Main.ScreenHeight / 2))))
            {
                isOnScreen = true;
            }

            if (
                player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                tile.Rectangle.X + map.TileSize > map.MapSize.Right - Main.ScreenWidth &&
                  // if camera sees top of screen and component.Y is in range
                ((player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  tile.Position.Y - map.TileSize < Main.ScreenHeight) ||
                  // else if camera sees bottom of screen and component.Y is in range
                 (player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  tile.Position.Y + map.TileSize > map.MapSize.Bottom - (Main.ScreenHeight)) ||
                  // else if camera sees neither the bottom or the top and component.Y is in range
                 (player.Position.Y > map.MapSize.Y + (Main.ScreenHeight / 2) &&
                  player.Position.Y < map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                  tile.Position.Y - map.TileSize < player.Position.Y + (Main.ScreenHeight / 2) &&
                  tile.Position.Y + map.TileSize > player.Position.Y - (Main.ScreenHeight / 2))))

            {
                isOnScreen = true;
            }
            
            if (
                player.Position.Y < map.MapSize.Y + (Main.ScreenHeight / 2) &&
                tile.Rectangle.Bottom - map.TileSize < Main.ScreenHeight &&                   
                  // if camera sees left of screen and component.X is in range
                ((player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                  tile.Position.X - map.TileSize < Main.ScreenWidth) ||
                 // else if camera sees right of screen and component.X is in range
                 (player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  tile.Position.X + map.TileSize > map.MapSize.Right - (Main.ScreenWidth)) ||
                 // else if camera sees neither the bottom or the top and component.X is in range
                 (player.Position.X > map.MapSize.X + (Main.ScreenWidth / 2) &&
                  player.Position.X < map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  tile.Position.X - map.TileSize < player.Position.X + (Main.ScreenWidth / 2) &&
                  tile.Position.X + map.TileSize > player.Position.X - (Main.ScreenWidth / 2))))
            {
                isOnScreen = true;
            }

            if (
                player.Position.Y > map.MapSize.Bottom - (Main.ScreenHeight / 2) &&
                tile.Rectangle.Y + map.TileSize > map.MapSize.Bottom - Main.ScreenHeight && 
                 // if camera sees left of screen and component.X is in range
                ((player.Position.X < map.MapSize.X + (Main.ScreenWidth / 2) &&
                  tile.Position.X - map.TileSize < Main.ScreenWidth) ||
                 // else if camera sees right of screen and component.X is in range
                 (player.Position.X > map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  tile.Position.X + map.TileSize > map.MapSize.Right - (Main.ScreenWidth)) ||
                 // else if camera sees neither the bottom or the top and component.X is in range
                 (player.Position.X > map.MapSize.X + (Main.ScreenWidth / 2) &&
                  player.Position.X < map.MapSize.Right - (Main.ScreenWidth / 2) &&
                  tile.Position.X - map.TileSize < player.Position.X + (Main.ScreenWidth / 2) &&
                  tile.Position.X + map.TileSize > player.Position.X - (Main.ScreenWidth / 2))))
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
    }
}
