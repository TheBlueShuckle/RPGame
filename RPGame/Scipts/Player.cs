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

        int tileSize;
        Vector2 pos, size;
        Rectangle hitBox;

        public Player(Rectangle windowSize, int tileSize)
        {
            pos = new Vector2(0, 0);
            size = new Vector2(windowSize.Width / 128, (windowSize.Width / 96));
            this.tileSize = tileSize;
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
            inputHandler = new PlayerInputHandler(tileSize * 3.3f);
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            inputHandler.Movement(gameTime, hitBox, impassableTiles);

            pos = new Vector2((float)Math.Round(pos.X + inputHandler.Velocity.X, 0), (float)Math.Round(pos.Y + inputHandler.Velocity.Y, 0));
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font)
        {
            spriteBatch.Draw(texture, hitBox, Color.Yellow);
            spriteBatch.DrawString(font, "" + hitBox + " " + tileSize, new Vector2(0, 0), Color.White);
        }
    }
}
