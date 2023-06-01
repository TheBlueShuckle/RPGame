using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Scenes;
using System.Collections.Generic;

namespace RPGame.Scipts.Handlers
{
    internal class SceneHandler
    {
        List<Scene> scenes = new List<Scene>();

        public List<Scene> GetScenes { get { return scenes; } }

        public SceneHandler()
        {
            scenes.Add(new Scene1());
            scenes.Add(new Scene2());
        }

        public Scene GetCurrentScene(int index)
        {
            return scenes[index];
        }
    }
}
