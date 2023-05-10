using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Player
    {
        PlayerInputHandler inputHandler;

        Vector2 pos, size;
        Rectangle hitBox;
        int tileSize;

        public Player(Rectangle windowSize, int tileSize)
        {
            pos = new Vector2(100, 100);
            size = new Vector2(windowSize.Width / 60, (windowSize.Width / 48));
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
            inputHandler = new PlayerInputHandler(pos);
            inputHandler.Speed = tileSize * 3.3f;

            this.tileSize = tileSize;
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            inputHandler.Movement(gameTime.ElapsedGameTime.TotalSeconds, hitBox, impassableTiles);
            pos = inputHandler.PlayerPos;
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, hitBox, Color.Yellow);
        }
    }
}
