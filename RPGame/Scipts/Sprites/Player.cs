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

        int tileSize;

        int spriteWidth, spriteHeight;

        public override Vector2 Position { get; set; }

        public override Rectangle Hitbox { get; set; }

        public Player(int tileSize, List<Tile> impassableTiles, Texture2D texture)
        {
            this.impassableTiles = impassableTiles;
            this.texture = texture;
            this.tileSize = tileSize;

            spriteWidth = texture.Width * tileSize / 16;
            spriteHeight = texture.Height * tileSize / 16;

            movementHandler = new MovementHandler(tileSize * 3.3f, new Vector2(0, 0), new Vector2(tileSize, tileSize));

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
