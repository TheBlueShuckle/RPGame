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


        public Scene1(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture) : base(spriteBatch, graphics, texture)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            player = new Player(spriteBatch, graphics, texture);
            map = new Map(spriteBatch, graphics, texture);

            impassableTiles = map.GetImpassableTiles();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, impassableTiles);
        }

        public override void Draw()
        {
            map.Draw();
            player.Draw();
        }
    }
}
