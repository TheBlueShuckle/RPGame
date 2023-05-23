using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using SharpDX.Multimedia;
using System;

namespace RPGame.Scipts.Handlers
{
    internal class EnemyMovementHandler
    {
        float speed;
        Vector2 velocity, size;

        public Vector2 Position { get; set; }

        public Rectangle Rectangle { get; set; }

        public EnemyMovementHandler(float speed, Vector2 pos, Vector2 size)
        {
            this.speed = speed;
            Position = pos;
            this.size = size;
        }

        public void Update(GameTime gameTime, Point center)
        {
            Movement(gameTime, center);
            Position = Position + velocity;
            Rectangle = new Rectangle(Position.ToPoint(), size.ToPoint());
        }

        private void Movement(GameTime gameTime, Point center)
        {
            if (center.X < 0 || velocity.X == 0)
            {
                velocity.X = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (center.X > Main.ScreenWidth)
            {
                velocity.X = -speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
