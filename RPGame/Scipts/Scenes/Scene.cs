using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;

namespace RPGame.Scipts.Scenes
{
    internal abstract class Scene
    {
        public virtual void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            LoadTextures(GraphicsDevice, Content);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        protected abstract void LoadTextures(GraphicsDevice GraphicsDevice, ContentManager Content);
    }
}
