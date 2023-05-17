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
            pos = new Vector2((float)Math.Round(Pos.X + velocity.X, 0), (float)Math.Round(Pos.Y + velocity.Y, 0));
            Hitbox = new Rectangle(Pos.ToPoint(), size.ToPoint());
        }

        private void Movement(GameTime gameTime, List<Tile> impassableTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S) && (!CollidingUp(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.W;
                velocity.Y = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.LeftControl) && (!CollidingDown(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.S;
                velocity.Y = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.Y = RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
                velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X = RUNNING_SPEED_MULTIPLIER * (-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Main.EditMode == OF)
                {
                    UpdateCollision(impassableTiles);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A) && (!CollidingRight(Hitbox, impassableTiles) || Main.EditMode))
            {
                lastKey = Keys.D;
                velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    velocity.X = RUNNING_SPEED_MULTIPLIER * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesBottomOf(tile.GetRectangle()))
                        {
                            pos.Y = tile.GetRectangle().Bottom;
                            velocity.Y = 0;
                        }
                        break;

                    case Keys.S:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesTopOf(tile.GetRectangle()))
                        {
                            pos.Y = tile.GetRectangle().Y - Hitbox.Height;
                            velocity.Y = 0;
                        }
                        break;

                    case Keys.A:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesRightOf(tile.GetRectangle()))
                        {
                            pos.X = tile.GetRectangle().Right;
                            velocity.X = 0;
                        }
                        break;

                    case Keys.D:
                        if (new Rectangle(Hitbox.X + (int)velocity.X, Hitbox.Y + (int)velocity.Y, (int)size.X, (int)size.Y).TouchesLeftOf(tile.GetRectangle()))
                        {
                            pos.X = tile.GetRectangle().Left - Hitbox.Width;
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