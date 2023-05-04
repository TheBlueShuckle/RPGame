using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace RPGame.Scipts
{
    internal class Player : Main
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphicsDeviceManager;
        PlayerInputHandler inputHandler;

        Vector2 pos;
        Point size;
        Rectangle hitBox;
        Texture2D texture;

        public Player(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDeviceManager = graphics;
            this.texture = texture;

            pos = new Vector2(Window.ClientBounds.X / 2, Window.ClientBounds.Y / 2);
            size = new Point(Window.ClientBounds.X / 30, Window.ClientBounds.Y / 20);
            inputHandler = new PlayerInputHandler(pos, hitBox);
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            inputHandler.Movement(gameTime.ElapsedGameTime.TotalSeconds, impassableTiles);
            pos = inputHandler.PlayerPos;
            hitBox = new Rectangle(pos.ToPoint(), size);
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, hitBox, Color.Red);
        }
    }
}
