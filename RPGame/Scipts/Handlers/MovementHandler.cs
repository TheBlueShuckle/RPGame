﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPGame.Scipts.Components;
using System.Collections.Generic;
using System.Dynamic;

namespace RPGame.Scipts.Handlers
{
    internal class MovementHandler
    {
        const float RUNNING_SPEED_MULTIPLIER = 2.5f;
        const bool OF = false;
        const int UP = 1, DOWN = 2, LEFT = 3, RIGHT = 4;

        float speed;
        Vector2 size, velocity, pos;
        Keys lastKey;

        public Vector2 Pos { get { return pos; } }

        public Rectangle Hitbox { get; set; }

        public int LookingDirection = DOWN;

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
                LookingDirection = UP;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * RUNNING_SPEED_MULTIPLIER;
                }

                else
                {
                    velocity.Y = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.LeftControl) && (!CollidingDown(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.S;
                LookingDirection = DOWN;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * RUNNING_SPEED_MULTIPLIER;
                }

                else
                {
                    velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else
            {
                velocity.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D) && (!CollidingLeft(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.A;
                LookingDirection = LEFT;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X = RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                else
                {
                    velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && (!CollidingRight(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.D;
                LookingDirection = RIGHT;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X =  (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * RUNNING_SPEED_MULTIPLIER;
                }

                else
                {
                    velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else
            {
                velocity.X = 0;
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