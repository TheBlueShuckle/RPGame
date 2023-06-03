using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RPGame.Scipts.Handlers;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts.Components
{
    internal class Player : Component
    {
        const int UP = 1, DOWN = 2, LEFT = 3, RIGHT = 4;

        MovementHandler movementHandler;

        Texture2D texture;
        List<Tile> impassableTiles;
        Rectangle spriteSize;

        int spriteWidth, spriteHeight;

        double meleeCooldown;

        public override Vector2 Position { get; set; }

        public override Rectangle Hitbox { get; set; }

        public float Damage { get; set; }

        public Rectangle MeleeRange { get; set; }

        public int LookingDirection { get; private set; }

        public Player(float tileSize, List<Tile> impassableTiles, Texture2D texture)
        {
            this.impassableTiles = impassableTiles;
            this.texture = texture;

            spriteWidth = (int)(texture.Width * Main.Pixel);
            spriteHeight = (int)(texture.Height * Main.Pixel);

            movementHandler = new MovementHandler(tileSize * 3.3f, new Vector2(0, 0), new Vector2(16 * Main.Pixel, 16 * Main.Pixel));

            Position = movementHandler.Pos;
            Hitbox = movementHandler.Hitbox;
            Damage = 10;
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds > meleeCooldown)
            {
                movementHandler.Update(gameTime, impassableTiles);
            }

            Position = movementHandler.Pos;
            Hitbox = movementHandler.Hitbox;
            LookingDirection = movementHandler.LookingDirection;
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

        public void CheckMeleeAttack(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && gameTime.TotalGameTime.TotalMilliseconds > meleeCooldown)
            {
                switch (LookingDirection)
                {
                    case UP:
                        MeleeRange = new Rectangle((int)Position.X, (int)(Position.Y - (16 * Main.Pixel)), (int)(16 * Main.Pixel), (int)(16 * Main.Pixel));
                        break;
                    case DOWN:
                        MeleeRange = new Rectangle((int)Position.X, (int)spriteSize.Bottom, (int)(16 * Main.Pixel), (int)(16 * Main.Pixel));
                        break;
                    case LEFT:
                        MeleeRange = new Rectangle((int)(Position.X - 16 * Main.Pixel), (int)Position.Y, (int)(16 * Main.Pixel), (int)(16 * Main.Pixel));
                        break;
                    case RIGHT:
                        MeleeRange = new Rectangle((int)spriteSize.Right, (int)Position.Y, (int)(16 * Main.Pixel), (int)(16 * Main.Pixel));
                        break;
                }

                meleeCooldown = gameTime.TotalGameTime.TotalMilliseconds + 500;
            }
        }
    }
}