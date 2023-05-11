using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Player
    {
        PlayerInputHandler inputHandler;

        Vector2 pos, size;
        Rectangle hitBox;

        public Player(Rectangle windowSize, int tileSize)
        {
            pos = new Vector2(0, 0);
            size = new Vector2(windowSize.Width / 128, (windowSize.Width / 96));
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
            inputHandler = new PlayerInputHandler(tileSize * 3.3f);
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            inputHandler.Movement(gameTime, hitBox, impassableTiles);

            pos += inputHandler.Velocity;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, hitBox, Color.Yellow);
        }
    }
}
