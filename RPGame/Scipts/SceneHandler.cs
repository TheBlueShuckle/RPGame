﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Scenes;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class SceneHandler
    {
        List<Scene> scenes = new List<Scene>();

        public SceneHandler(Rectangle windowSize)
        {
            scenes.Add(new Scene1(windowSize));
        }

        public Scene GetCurrentScene()
        {
            return scenes[0];
        }
    }
}
