using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Player
    {
        MovementHandler movementHandler;

        int tileSize;

        public Player(Rectangle windowSize, int tileSize)
        {
            this.tileSize = tileSize;
            movementHandler = new MovementHandler(tileSize * 3.3f, new Vector2(0, 0), new Vector2(windowSize.Width / 128, (windowSize.Width / 96)));
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            movementHandler.Update(gameTime, impassableTiles);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font)
        {
            spriteBatch.Draw(texture, movementHandler.Hitbox, Color.Yellow);
            spriteBatch.DrawString(font, "" + movementHandler.Hitbox + " " + tileSize, new Vector2(0, 0), Color.White);
        }
    }
}
