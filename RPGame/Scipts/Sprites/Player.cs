using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Handlers;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts.Components
{
    internal class Player : Component
    {
        MovementHandler movementHandler;

        Texture2D texture;
        List<Tile> impassableTiles;
        Rectangle spriteSize;

        float tileSize;

        int spriteWidth, spriteHeight;

        public override Vector2 Position { get; set; }

        public override Rectangle Hitbox { get; set; }

        public Player(float tileSize, List<Tile> impassableTiles, Texture2D texture)
        {
            this.impassableTiles = impassableTiles;
            this.texture = texture;
            this.tileSize = tileSize;

            spriteWidth = (int)(texture.Width * Main.Pixel);
            spriteHeight = (int)(texture.Height * Main.Pixel);

            movementHandler = new MovementHandler(tileSize * 3.3f, new Vector2(0, 0), new Vector2(16 * Main.Pixel, 16 * Main.Pixel));

            Position = movementHandler.Pos;
            Hitbox = movementHandler.Hitbox;
        }

        public override void Update(GameTime gameTime)
        {
            movementHandler.Update(gameTime, impassableTiles);

            Position = movementHandler.Pos;
            Hitbox = movementHandler.Hitbox;

            spriteSize = new Rectangle(Hitbox.Center.X - spriteWidth / 2, Hitbox.Bottom - spriteHeight, spriteWidth, spriteHeight);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, spriteSize, Color.White);
        }

        public Point GetCenter()
        {
            return new Point((int)(Position.X + Hitbox.Width / 2), (int)(Position.Y + Hitbox.Height / 2));
        }
    }
}
