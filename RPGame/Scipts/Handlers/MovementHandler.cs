using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System.Collections.Generic;

namespace RPGame.Scipts.Handlers
{
    internal class MovementHandler
    {
        const float RUNNING_SPEED_MULTIPLIER = 5;
        const bool OF = false;

        float speed;
        Vector2 size, velocity, pos;
        Keys lastKey;

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
            pos = pos + velocity;
            Hitbox = new Rectangle(Pos.ToPoint(), size.ToPoint());
        }

        private void Movement(GameTime gameTime, List<Tile> impassableTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S) && (!CollidingUp(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.W;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y += (RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds)) / 25;

                    if (velocity.Y <= RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                    {
                        velocity.Y = RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                }

                else
                {
                    if (velocity.Y < -speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.Y += -speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 25;
                    }

                    if (velocity.Y >= -speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.Y = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.LeftControl) && (!CollidingDown(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.S;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y += (RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds)) / 25;

                    if (velocity.Y >= RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                    {
                        velocity.Y = RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                else
                {
                    if (velocity.Y > speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 25;
                    }

                    if (velocity.Y <= speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else
            {
                if (velocity.Y > 0)
                {
                    velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 3;

                    if (velocity.Y < 0)
                    {
                        velocity.Y = 0;
                    }
                }

                if (velocity.Y < 0)
                {
                    velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 3;

                    if (velocity.Y > 0)
                    {
                        velocity.Y = 0;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D) && (!CollidingLeft(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.A;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X += (RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds)) / 25;

                    if (velocity.X <= RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                    {
                        velocity.X = RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                else
                {
                    if (velocity.X < -speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.X += -speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 25;
                    }

                    if (velocity.X >= -speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && (!CollidingRight(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.D;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X += (RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds)) / 25;

                    if (velocity.X >= RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
                    {
                        velocity.X = RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                else
                {
                    if (velocity.X > speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 25;
                    }

                    if (velocity.X <= speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else
            {
                if (velocity.X > 0)
                {
                    velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 3;

                    if (velocity.X < 0)
                    {
                        velocity.X = 0;
                    }
                }

                if (velocity.X < 0)
                {
                    velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 3;

                    if (velocity.X > 0)
                    {
                        velocity.X = 0;
                    }
                }
            }
        }

        private void UpdateCollision(List<Tile> impassableTiles)
        {
            foreach (Tile tile in impassableTiles)
            {
                switch (lastKey)
                {
                    case Keys.W:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesBottomOf(tile.ScaledRectangle()))
                        {
                            pos.Y = tile.ScaledRectangle().Bottom;
                            velocity.Y = 0;
                        }
                        break;

                    case Keys.S:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesTopOf(tile.ScaledRectangle()))
                        {
                            pos.Y = tile.ScaledRectangle().Y - Hitbox.Height;
                            velocity.Y = 0;
                        }
                        break;

                    case Keys.A:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesRightOf(tile.ScaledRectangle()))
                        {
                            pos.X = tile.ScaledRectangle().Right;
                            velocity.X = 0;
                        }
                        break;

                    case Keys.D:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesLeftOf(tile.ScaledRectangle()))
                        {
                            pos.X = tile.ScaledRectangle().Left - Hitbox.Width;
                            velocity.X = 0;
                        }
                        break;
                }
            }
        }

        private bool CollidingUp(Rectangle hitbox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (hitbox.TouchesBottomOf(tile.ScaledRectangle()))
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
                if (hitbox.TouchesTopOf(tile.ScaledRectangle()))
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
                if (hitbox.TouchesLeftOf(tile.ScaledRectangle()))
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
                if (hitbox.TouchesRightOf(tile.ScaledRectangle()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}