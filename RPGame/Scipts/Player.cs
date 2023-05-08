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
        PlayerInputHandler inputHandler;

        Vector2 pos;
        Point size;
        Rectangle hitBox;

        public Player()
        {
            pos = new Vector2(Window.ClientBounds.X / 2, Window.ClientBounds.Y / 2);
            size = new Point(20, 40);
            hitBox = new Rectangle(pos.ToPoint(), size);
            inputHandler = new PlayerInputHandler(pos);
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            inputHandler.Movement(gameTime.ElapsedGameTime.TotalSeconds, hitBox, impassableTiles);
            pos = inputHandler.PlayerPos;
            hitBox = new Rectangle(pos.ToPoint(), size);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, hitBox, Color.Red);
        }
    }
}
