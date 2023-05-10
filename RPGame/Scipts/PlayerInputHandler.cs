using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class PlayerInputHandler
    {
        float speed;
        Vector2 playerPos;

        public Vector2 PlayerPos
        {
            get { return playerPos; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public PlayerInputHandler(Vector2 playerPos)
        {
            this.playerPos = playerPos;
        }

        public void Movement(double deltaTime, Rectangle hitBox, List<Tile> impassabeTiles)
        {
            //Move up doesnt work
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && !CollidingDown(hitBox, impassabeTiles))
            {
                playerPos.Y -= (int) Math.Round(speed * (float)deltaTime, 0);
            }

            //Move down
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && !CollidingUp(hitBox, impassabeTiles))
            {
                playerPos.Y += (int) Math.Round(speed * (float)deltaTime, 0);
            }

            //Move right
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && !CollidingRight(hitBox, impassabeTiles))
            {
                playerPos.X += (int) Math.Round(speed * (float)deltaTime, 0);
            }

            //Move left doesnt work
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && !CollidingLeft(hitBox, impassabeTiles))
            {
                playerPos.X -= (int) Math.Round(speed * (float)deltaTime);
            }
        }

        private bool CollidingDown(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchTop(hitBox, tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingUp(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchBottom(hitBox, tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingRight(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchToRight(hitBox, tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollidingLeft(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchToLeft(hitBox, tile.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}