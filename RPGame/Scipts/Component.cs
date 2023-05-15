using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame.Scipts
{
    public interface Component
    {
        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
