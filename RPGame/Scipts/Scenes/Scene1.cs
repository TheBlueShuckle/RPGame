using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RPGame.Scipts.Scenes
{
    internal class Scene1 : Scene
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Texture2D texture;

        Player player;
        Map map;
        List<Tile> impassableTiles;


        public Scene1() : base()
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            player = new Player();
            map = new Map();

            map.GenerateMap(new int[,]
            {
                {2, 2, 2, 2, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3},
                {4, 2, 2, 2, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3},
                {4, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3},
                {4, 4, 2, 2, 2, 1, 2, 2, 2, 3, 3, 3, 4, 4, 3, 3, 3, 1, 3, 3},
                {4, 4, 2, 2, 2, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 3, 3, 1, 3, 3},
                {4, 4, 2, 2, 2, 1, 1, 2, 3, 3, 3, 4, 4, 4, 4, 3, 1, 1, 3, 3},
                {4, 4, 4, 2, 2, 2, 1, 2, 3, 3, 3, 4, 4, 4, 4, 3, 1, 3, 3, 3},
                {4, 4, 4, 2, 2, 2, 1, 2, 2, 3, 3, 3, 4, 3, 3, 3, 1, 3, 3, 3},
                {4, 4, 2, 2, 2, 2, 1, 1, 2, 3, 3, 3, 3, 3, 3, 1, 1, 1, 3, 3},
                {4, 4, 2, 2, 2, 2, 2, 1, 1, 1, 3, 3, 3, 3, 1, 1, 3, 1, 1, 3},
                {4, 4, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 3},
                {4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3}
            }
            );

            impassableTiles = map.GetImpassableTiles();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, impassableTiles);
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.Draw(spriteBatch, texture);
            player.Draw(spriteBatch, texture);
        }
    }
}
