using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class PlayerInputHandler
    {
        const float SPEED = 200;

        public Vector2 playerPos;

        public Vector2 PlayerPos
        {
            get { return playerPos; }
        }

        public PlayerInputHandler(Vector2 playerPos)
        {
            this.playerPos = playerPos;
        }

        public void Movement(double deltaTime, Rectangle hitBox, List<Tile> impassabeTiles)
        {
            //Move up doesnt work
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !CollidingDown(hitBox, impassabeTiles))
            {
                playerPos.Y -= SPEED * (float)deltaTime;
                CorrectTop(hitBox, impassabeTiles);
            }

            //Move down
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !CollidingUp(hitBox, impassabeTiles))
            {
                playerPos.Y += SPEED * (float)deltaTime;
                CorrectBottom(hitBox, impassabeTiles);
            }

            //Move right
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !CollidingRight(hitBox, impassabeTiles))
            {
                playerPos.X += SPEED * (float)deltaTime;
                CorrectRight(hitBox, impassabeTiles);
            }

            //Move left doesnt work
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !CollidingLeft(hitBox, impassabeTiles))
            {
                playerPos.X -= SPEED * (float)deltaTime;
                CorrectLeft(hitBox, impassabeTiles);
            }
        }

        private void CorrectTop(Rectangle hitBox, List<Tile> impassabeTiles) //*
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchTop(hitBox, tile.GetRectangle()))
                {
                    playerPos.Y = tile.GetRectangle().Bottom;
                }
            }
        }

        private void CorrectBottom(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if(RectangleHelper.TouchBottom(hitBox, tile.GetRectangle()))
                {
                    playerPos.Y = tile.GetRectangle().Top - hitBox.Height;
                }
            }
        }

        private void CorrectRight(Rectangle hitBox, List<Tile> impassabeTiles)
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchToRight(hitBox, tile.GetRectangle()))
                {
                    playerPos.X = tile.GetRectangle().Left - hitBox.Width;
                }
            }
        }

        private void CorrectLeft(Rectangle hitBox, List<Tile> impassabeTiles) //*
        {
            foreach (Tile tile in impassabeTiles)
            {
                if (RectangleHelper.TouchToLeft(hitBox, tile.GetRectangle()))
                {
                    playerPos.X = tile.GetRectangle().Right;
                }
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