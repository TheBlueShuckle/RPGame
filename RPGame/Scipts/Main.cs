﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using RPGame.Scipts.Handlers;
using RPGame.Scipts.Scenes;
using System.CodeDom;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SceneHandler sceneHandler;

        public static int ScreenWidth;
        public static int ScreenHeight;
        public static float Pixel;

        public static SpriteFont Font { get; set; }
        public static bool EditMode { get; set; }

        Texture2D texture;

        KeyboardState ks1, ks2, sceneks1, sceneks2;
        int currentScene = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            sceneHandler = new SceneHandler();

            EditMode = false;

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            graphics.HardwareModeSwitch = false;

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            Pixel = (float)ScreenWidth / 512;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            Font = Content.Load<SpriteFont>("Font/Font");

            foreach (Scene scene in sceneHandler.GetScenes)
            {
                scene.LoadContent(GraphicsDevice, Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ToggleEditMode();

            ForceChangeScene();

            sceneHandler.GetCurrentScene(currentScene).Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            sceneHandler.GetCurrentScene(currentScene).Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            if (EditMode)
            {
                DrawCrosshair();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ToggleEditMode()
        {
            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.Space) && ks2.IsKeyUp(Keys.Space))
            {
                EditMode = EditMode ? false : true;
            }

            ks2 = ks1;
        }

        private void ForceChangeScene()
        {
            sceneks1 = Keyboard.GetState();

            if (sceneks1.IsKeyDown(Keys.F12) && sceneks2.IsKeyUp(Keys.F12))
            {
                currentScene += 1;

                if (currentScene > sceneHandler.GetScenes.Count - 1)
                {
                    currentScene = 0;
                }
            }

            sceneks2 = sceneks1;
        }

        private void DrawCrosshair()
        {
            spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 2, (ScreenHeight / 2) - 10, 4, 20), Color.Black);
            spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 10, (ScreenHeight / 2) - 2, 20, 4), Color.Black);
        }
    }
}