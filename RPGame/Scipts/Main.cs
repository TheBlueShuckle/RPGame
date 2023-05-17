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

        Texture2D texture;

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
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Font = Content.Load<SpriteFont>("Font/Font");

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            sceneHandler = new SceneHandler();

            foreach (Scene1 scene in sceneHandler.GetScenes)
            {
                scene.LoadContent(GraphicsDevice);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ToggleEditMode();

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

        private void DrawCrosshair()
        {
            spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 2, (ScreenHeight / 2) - 10, 4, 20), Color.Black);
            spriteBatch.Draw(texture, new Rectangle((ScreenWidth / 2) - 10, (ScreenHeight / 2) - 2, 20, 4), Color.Black);
        }
    }
}