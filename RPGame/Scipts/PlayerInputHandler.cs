using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class PlayerInputHandler
    {
        float speed;
        Vector2 velocity;

        public Vector2 Velocity { get { return velocity; } }

        public PlayerInputHandler(float speed)
        {
            this.speed = speed;
        }

        public void Movement(GameTime gameTime, Rectangle hitbox, List<Tile> impassableTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y = 0;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.W) && !CollidingUp(hitbox, impassableTiles))
            {
                velocity.Y = -(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !CollidingDown(hitbox, impassableTiles))
            {
                velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else
            {
                velocity.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = 0;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !CollidingRight(hitbox, impassableTiles))
            {
                velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !CollidingLeft(hitbox, impassableTiles))
            {
                velocity.X = -(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            else
            {
                velocity.X = 0;
            }
        }

        private bool CollidingUp(Rectangle hitbox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (hitbox.TouchTop(tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingDown(Rectangle hitbox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (hitbox.TouchBottom(tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingRight(Rectangle hitbox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (hitbox.TouchToRight(tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingLeft(Rectangle hitbox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (hitbox.TouchToLeft(tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}