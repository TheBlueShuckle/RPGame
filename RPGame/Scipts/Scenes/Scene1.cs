using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using RPGame.Scipts.Core;
using System.Collections.Generic;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using System.Linq;
using RPGame.Scipts.Sprites.Enemies;
using RPGame.Scipts.Editing;

namespace RPGame.Scipts.Scenes
{
    internal class Scene1 : Scene
    {
        const string FILE_NAME = "Scene1";

        Player player;
        Map map;
        MapSaver mapSaver;

        List<Tile> tiles, impassableTiles;
        List<Enemy> enemies = new List<Enemy>();
        List<Component> components;

        KeyboardState ks1, ks2;
        Texture2D texture;

        private Camera camera;


        public Scene1() : base()
        {

        }

        public override void LoadContent(GraphicsDevice GraphicsDevice)
        {
            LoadTextures(GraphicsDevice);

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
            if (Main.EditMode)
            {
                map.EditMap(player.GetCenter());
                AddNewTiles();
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
        }

        protected override void LoadTextures(GraphicsDevice GraphicsDevice)
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
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
