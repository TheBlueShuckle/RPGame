using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts.Handlers
{
    internal class MovementHandler
    {
        float speed;
        Vector2 size, velocity;

        public Vector2 Pos { get; set; }

        public Rectangle Hitbox { get; set; }

        public MovementHandler(float speed, Vector2 pos, Vector2 size)
        {
            this.speed = speed;
            Pos = pos;
            this.size = size;
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            Movement(gameTime, impassableTiles);

            Pos = new Vector2((float)Math.Round(Pos.X + velocity.X, 0), (float)Math.Round(Pos.Y + velocity.Y, 0));
            Hitbox = new Rectangle(Pos.ToPoint(), size.ToPoint());
        }

        private void Movement(GameTime gameTime, List<Tile> impassableTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S) && !CollidingUp(Hitbox, impassableTiles))
            {
                velocity.Y = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W) && !CollidingDown(Hitbox, impassableTiles))
            {
                velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else
            {
                velocity.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D) && !CollidingLeft(Hitbox, impassableTiles))
            {
                velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && !CollidingRight(Hitbox, impassableTiles))
            {
                velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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