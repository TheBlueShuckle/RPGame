using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace RPGame.Scipts.Scenes
{
    internal abstract class Scene
    {
        public virtual void LoadContent(GraphicsDevice GraphicsDevice)
        {
            LoadTextures(GraphicsDevice);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        protected abstract void LoadTextures(GraphicsDevice GraphicsDevice);
    }
}
