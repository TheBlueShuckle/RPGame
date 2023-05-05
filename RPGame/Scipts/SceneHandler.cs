using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Scenes;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class SceneHandler
    {
        List<Scene> scenes = new List<Scene>();

        public SceneHandler()
        {
            scenes.Add(new Scene1());
        }

        public Scene GetCurrentScene()
        {
            return scenes[0];
        }
    }
}
