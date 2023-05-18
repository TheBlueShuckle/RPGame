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

        public override Vector2 Position { get; set; }

        public override Rectangle Rectangle { get; set; }

        public Player(int tileSize, List<Tile> impassableTiles, Texture2D texture)
        {
            this.impassableTiles = impassableTiles;
            this.texture = texture;

            movementHandler = new MovementHandler(tileSize * 3.3f, new Vector2(0, 0), new Vector2(Main.ScreenWidth / 128, Main.ScreenWidth / 96));

            Position = movementHandler.Pos;
            Rectangle = movementHandler.Hitbox;
        }

        public override void Update(GameTime gameTime)
        {
            movementHandler.Update(gameTime, impassableTiles);

            Position = movementHandler.Pos;
            Rectangle = movementHandler.Hitbox;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.Yellow);
        }

        public Point GetCenter()
        {
            return new Point((int)(Position.X + Rectangle.Width / 2), (int)(Position.Y + Rectangle.Height / 2));
        }
    }
}
