using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using RPGame.Scipts.Handlers;
using RPGame.Scipts.Scenes;
using SharpDX.Direct2D1.Effects;
using SharpDX.MediaFoundation;
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
        public static SpriteFont Font { get; set; }
        public static bool EditMode { get; set; }

        Rectangle windowSize;
        Texture2D texture;
        Scene1 scene1;

        KeyboardState ks1, ks2;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            EditMode = false;

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            Font = Content.Load<SpriteFont>("Font/Font");

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

            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.Space) && ks2.IsKeyUp(Keys.Space))
            {
                EditMode = EditMode ? false : true;
            }

            ks2 = ks1;

            sceneHandler.GetCurrentScene().Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneHandler.GetCurrentScene().Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            if (EditMode)
            {
                spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 1, (ScreenHeight / 2) - 10, 2, 20), Color.Black);
                spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 10, (ScreenHeight / 2) - 1, 20, 2), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}