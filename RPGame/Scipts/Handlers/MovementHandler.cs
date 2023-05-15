using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Xml.Serialization;

namespace RPGame.Scipts.Handlers
{
    internal class MovementHandler
    {
        const float SPEED_MULTIPLIER = 5;

        float speed;
        Vector2 size, velocity, pos;

        public Vector2 Pos { get { return pos; } }

        public Rectangle Hitbox { get; set; }

        public MovementHandler(float speed, Vector2 pos, Vector2 size)
        {
            this.speed = speed;
            this.pos = pos;
            this.size = size;
        }

        public void Update(GameTime gameTime, List<Tile> impassableTiles)
        {
            Movement(gameTime, impassableTiles);
            pos = new Vector2((float)Math.Round(Pos.X + velocity.X, 0), (float)Math.Round(Pos.Y + velocity.Y, 0));
            Hitbox = new Rectangle(Pos.ToPoint(), size.ToPoint());
        }

        private void Movement(GameTime gameTime, List<Tile> impassableTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S) && (!CollidingUp(Hitbox, impassableTiles) || Main.EditMode))
            {
                velocity.Y = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == false)
                {
                    foreach (Tile tile in impassableTiles)
                    {
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesBottomOf(tile.GetRectangle()))
                        {
                            pos.Y = tile.GetRectangle().Bottom;
                            velocity.Y = 0;
                        }
                    }
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W) && (!CollidingDown(Hitbox, impassableTiles) || Main.EditMode))
            {
                velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == false)
                {
                    foreach (Tile tile in impassableTiles)
                    {
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesTopOf(tile.GetRectangle()))
                        {
                            pos.Y = tile.GetRectangle().Y - Hitbox.Height;
                            velocity.Y = 0;
                        }
                    }
                }
            }

            else
            {
                velocity.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D) && (!CollidingLeft(Hitbox, impassableTiles) || Main.EditMode))
            {
                velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X = SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == false)
                {
                    foreach (Tile tile in impassableTiles)
                    {
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesRightOf(tile.GetRectangle()))
                        {
                            pos.X = tile.GetRectangle().Right;
                            velocity.X = 0;
                        }
                    }
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && (!CollidingRight(Hitbox, impassableTiles) || Main.EditMode))
            {
                velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X = SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == false)
                {
                    foreach (Tile tile in impassableTiles)
                    {
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesLeftOf(tile.GetRectangle()))
                        {
                            pos.X = tile.GetRectangle().Left - Hitbox.Width;
                            velocity.X = 0;
                        }
                    }
                }
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
                if (hitbox.TouchesBottomOf(tile.GetRectangle()))
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
                if (hitbox.TouchesTopOf(tile.GetRectangle()))
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
                if (hitbox.TouchesLeftOf(tile.GetRectangle()))
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
                if (hitbox.TouchesRightOf(tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}