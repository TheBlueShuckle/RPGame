using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame.Scipts
{
    public abstract class Component
    {
        public abstract Vector2 Position { get; set; }

        public abstract Rectangle Hitbox { get; set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
