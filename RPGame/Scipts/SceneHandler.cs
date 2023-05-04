using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Scenes;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class SceneHandler
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Texture2D texture;

        List<Scene> scenes = new List<Scene>();

        public SceneHandler(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            scenes.Add(new Scene1(spriteBatch, graphics, texture));
        }

        public Scene GetCurrentScene()
        {
            return scenes[0];
        }
    }
}
