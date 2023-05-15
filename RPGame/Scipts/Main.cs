using Microsoft.Xna.Framework;
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

        Rectangle windowSize;
        Texture2D texture;
        SpriteFont font;
        Scene1 scene1;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            font = Content.Load<SpriteFont>("Font/Font");

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            sceneHandler = new SceneHandler();

            foreach (Scene1 scene in sceneHandler.GetScenes)
            {
                scene.LoadContent(GraphicsDevice, texture);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneHandler.GetCurrentScene().Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneHandler.GetCurrentScene().Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}