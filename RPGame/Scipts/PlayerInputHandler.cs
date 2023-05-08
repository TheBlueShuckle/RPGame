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
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !CheckTop(hitBox, impassabeTiles))
            {
                playerPos.Y -= SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && !CheckBottom(hitBox, impassabeTiles))
            {
                playerPos.Y += SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !CheckLeft(hitBox, impassabeTiles))
            {
                playerPos.X += SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && !CheckRight(hitBox, impassabeTiles))
            {
                playerPos.X -= SPEED * (float)deltaTime;
            }
        }

        private bool CheckBottom(Rectangle hitBox, List<Tile> impassabeTiles)
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

        private bool CheckTop(Rectangle hitBox, List<Tile> impassabeTiles)
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

        private bool CheckLeft(Rectangle hitBox, List<Tile> impassabeTiles)
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

        private bool CheckRight(Rectangle hitBox, List<Tile> impassabeTiles)
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
    }
}