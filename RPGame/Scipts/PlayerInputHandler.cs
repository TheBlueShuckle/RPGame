using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class PlayerInputHandler
    {
        const float SPEED = 200;

        public Vector2 playerPos;
        Rectangle hitBox;

        public Vector2 PlayerPos
        {
            get { return playerPos; }
        }

        public PlayerInputHandler(Vector2 playerPos, Rectangle hitBox)
        {
            this.playerPos = playerPos;
            this.hitBox = hitBox;
        }

        public void Movement(double deltaTime, List<Tile> impassabeTiles)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPos.Y -= SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPos.Y += SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerPos.X += SPEED * (float)deltaTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                playerPos.X -= SPEED * (float)deltaTime;
            }
        }
    }
}