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
using System.Security.Cryptography;
using System.IO.Packaging;
using RPGame.Scipts.Handlers;

namespace RPGame.Scipts.Scenes
{
    internal class Scene1 : Scene
    {
        const string FILE_NAME = "TileMaps\\scene1.json";

        Player player;
        Map map;
        MapSaver mapSaver;
        TileRenderer tileRenderer;

        List<Enemy> enemies = new List<Enemy>();
        List<Component> components;

        KeyboardState ks1, ks2;
        Keys lastPressedKey;
        Texture2D texture, tileSet;
        SpriteFont font;

        int tilesOnScreen = 0;

        private Camera camera;


        public Scene1() : base()
        {

        }

        public override void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            LoadTextures(GraphicsDevice, Content);

            map = new Map(texture, new int[] { 128, 72 });
            mapSaver = new MapSaver(FILE_NAME);
            map.GenerateMap(mapSaver.LoadMap());

            tileRenderer = new TileRenderer(map.TileSize, map.MapSize);
            camera = new Camera(GraphicsDevice.Viewport, map.MapSize);

            player = new Player(map.TileSize, map.GetImpassableTiles(), Content.Load<Texture2D>("Sprites/Player"));
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
            if (Keyboard.GetState().GetPressedKeys().Count() != 0 &&
                !Keyboard.GetState().IsKeyDown(Keys.W) &&
                !Keyboard.GetState().IsKeyDown(Keys.A) &&
                !Keyboard.GetState().IsKeyDown(Keys.S) &&
                !Keyboard.GetState().IsKeyDown(Keys.D) &&
                !Keyboard.GetState().IsKeyDown(Keys.LeftShift) &&
                !Keyboard.GetState().IsKeyDown(Keys.Up) &&
                !Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                lastPressedKey = Keyboard.GetState().GetPressedKeys()[0];
            }

            if (Main.EditMode)
            {
                map.EditMap(lastPressedKey, camera.ScreenToWorldSpace(Mouse.GetState().Position.ToVector2()));

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    camera.Zoom += 0.01f;
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    camera.Zoom -= 0.01f;
                }
            }

            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && camera.Zoom > 0.8)
                {
                    camera.Zoom -= 0.01f;
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && camera.Zoom > 0.8)
                {
                    camera.Zoom -= 0.01f;

                    if (camera.Zoom < 0.8)
                    {
                        camera.Zoom = 0.8f;
                    }
                }

                else if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift) && camera.Zoom < 1)
                {
                    camera.Zoom += 0.01f;

                    if (camera.Zoom > 1)
                    {
                        camera.Zoom = 1;
                    }
                }
            }

            foreach (Component component in components)
            {
                component.Update(gameTime);
            }

            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.F1) && ks2.IsKeyUp(Keys.F1))
            {
                mapSaver.SaveMap(map.GetTiles());
            }

            ks2 = ks1;

            camera.Update(player.GetCenter().ToVector2());
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tilesOnScreen = 0;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            map.Draw(spriteBatch);

            foreach (Tile tile in map.GetTiles())
            {
                if (tileRenderer.IsTileOnScreen(tile, player.Position, player.GetCenter(), camera.Zoom))
                {
                    tile.Draw(gameTime, spriteBatch, tileSet);
                    tilesOnScreen++;
                }
            }

            foreach (Component component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            DrawHUD(spriteBatch);
        }

        private void DrawHUD(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "" + tilesOnScreen, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(font, "" + camera.Zoom, new Vector2(0, 100), Color.Black);

            spriteBatch.End();
        }

        protected override void LoadTextures(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            tileSet = Content.Load<Texture2D>("Sprites/Tileset");

            font = Content.Load<SpriteFont>("Font/Font");
        }
    }
}
