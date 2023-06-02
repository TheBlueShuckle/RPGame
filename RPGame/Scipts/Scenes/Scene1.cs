using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using RPGame.Scipts.Core;
using RPGame.Scipts.Editing;
using RPGame.Scipts.Handlers;
using RPGame.Scipts.Sprites.Enemies;
using System.Collections.Generic;
using System.Linq;

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

        Camera camera;


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
            SetLastPressedKey();

            if (!Main.EditMode)
            {
                ZoomOutOnRun();
            }

            else
            {
                EditMode();
            }

            foreach (Component component in components)
            {
                component.Update(gameTime);
            }

            SaveMap();

            camera.Update(player.GetCenter().ToVector2());
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tilesOnScreen = 0;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            map.Draw(spriteBatch);
            DrawTiles(gameTime, spriteBatch);
            DrawComponents(gameTime, spriteBatch);
            spriteBatch.Draw(texture, new Rectangle(camera.ScreenToWorldSpace(Mouse.GetState().Position.ToVector2()).ToPoint(), new Point((int)Main.Pixel, (int)Main.Pixel)), Color.White);

            spriteBatch.End();

            DrawHUD(spriteBatch);
        }

        protected override void LoadTextures(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            tileSet = Content.Load<Texture2D>("Sprites/Tileset");

            font = Content.Load<SpriteFont>("Font/Font");
        }

        private void SetLastPressedKey()
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
        }

        private void EditMode()
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

        private void ZoomOutOnRun()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && camera.Zoom > 0.8 &&
                (Keyboard.GetState().IsKeyDown(Keys.W) ||
                 Keyboard.GetState().IsKeyDown(Keys.A) ||
                 Keyboard.GetState().IsKeyDown(Keys.S) ||
                 Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                camera.Zoom -= 0.01f;

                if (camera.Zoom < 0.8)
                {
                    camera.Zoom = 0.8f;
                }
            }

            else if ((!Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                      (!Keyboard.GetState().IsKeyDown(Keys.W) &&
                       !Keyboard.GetState().IsKeyDown(Keys.A) &&
                       !Keyboard.GetState().IsKeyDown(Keys.S) &&
                       !Keyboard.GetState().IsKeyDown(Keys.D))) &&
                      camera.Zoom < 1)
            {
                camera.Zoom += 0.01f;

                if (camera.Zoom > 1)
                {
                    camera.Zoom = 1;
                }
            }

            else if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift) && camera.Zoom > 1)
            {
                camera.Zoom -= 0.01f;

                if (camera.Zoom < 1)
                {
                    camera.Zoom = 1;
                }
            }
        }

        private void SaveMap()
        {
            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.F1) && ks2.IsKeyUp(Keys.F1))
            {
                mapSaver.SaveMap(map.GetTiles());
            }

            ks2 = ks1;
        }

        private void DrawTiles(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in map.GetTiles())
            {
                if (tileRenderer.IsTileOnScreen(tile, player.Position, player.GetCenter(), camera.Zoom))
                {
                    tile.Draw(gameTime, spriteBatch, tileSet);
                    tilesOnScreen++;
                }
            }
        }

        private void DrawComponents(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        private void DrawHUD(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "" + tilesOnScreen, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(font, "" + camera.Zoom, new Vector2(0, 100), Color.Black);

            spriteBatch.End();
        }

    }
}
