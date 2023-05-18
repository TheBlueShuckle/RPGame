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

        private Camera camera;


        public Scene1() : base()
        {

        }

        public override void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            LoadTextures(GraphicsDevice, Content);

            map = new Map(texture, new int[] { 128, 72 });
            camera = new Camera(new Rectangle(new Point(0, 0), new Point(128 * map.TileSize, 72 * map.TileSize)));
            mapSaver = new MapSaver();

            map.GenerateMap(mapSaver.LoadMap(FILE_NAME));

            tiles = map.GetTiles();
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
                map.EditMap(player.Pos, lastPressedKey);
                AddNewTiles();
                //RemoveDeletedTiles();
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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform);

            map.Draw(spriteBatch);

            foreach (Component component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.DrawString(font, ("" + (Mouse.GetState().Position.X - (Main.ScreenWidth / 2 - player.Pos.X - 10) + ", " + (Mouse.GetState().Position.Y - (Main.ScreenHeight / 2 - player.Pos.Y -25 )))), Vector2.Zero, Color.Black);

            spriteBatch.End();
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
                if (oldTile.GetPosition() == tile.GetPosition() && oldTile != tile)
                {
                    tiles.Remove(oldTile);
                }
            }
        }
    }
}
