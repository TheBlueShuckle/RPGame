using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGame.Scipts
{
    internal class Tile
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Texture2D texture;

        int tileLength;
        Color color;
        Vector2 pos;
        bool passable;

        public Tile(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture, int tileLength, Color color, Vector2 pos, bool passable)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            this.tileLength = tileLength;
            this.color = color;
            this.pos = pos;
            this.passable = passable;
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, new Rectangle(pos.ToPoint(), new Point(tileLength, tileLength)), color);
        }

        public bool GetPassability()
        {
            return passable;
        }
    }
}
